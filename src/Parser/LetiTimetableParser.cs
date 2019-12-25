using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using ClosedXML.Excel;
using Contracts.Parser;
using System.Collections.Generic;

namespace Parser
{
    public class LetiTimetableParser : ITimetableParser
    {
        public Task<ITimetable> ParseTimetable(/*Stream*/ string pathToFile, IProgress<IParserProgress> progressReporter = default, CancellationToken ct = default)
        {
            //using var workbook = new XLWorkbook(pathToFile);
            //var worksheet = workbook.Worksheet("Data");

            //Test(worksheet);
            Parsing(pathToFile);

            //Ну а дальше развлекайся с Excel файлом
            //https://github.com/closedxml/closedxml
            //будет полезно тут глянуть на вкладочку Wiki

            //Чтобы понять что за прогресс и отмена можешь глянуть сюда:
            //https://devblogs.microsoft.com/dotnet/async-in-4-5-enabling-progress-and-cancellation-in-async-apis/

            throw new NotImplementedException();
        }

        private void Test(IXLWorksheet worksheet)
        {
            /*const int groupRowIndex = 5;
            const int groupTab = 4;
            
            var groupRow = worksheet.Row(groupRowIndex); //что ты пытался сделать?
            var groupCount = 0;
            while (!groupRow.Cell(groupCount + groupTab).IsEmpty())
                groupCount++;*/

            //нужно получить список групп
            //из них поймем длину таблицы (*кол-во столбцов)
            const int firstGroup = 4; //где неачинается полезная информация
            var index = 0;
            IXLRow currentRow = FindRaw(worksheet, "№ гр.", 0);
            index = firstGroup; //начало групп
            while (!currentRow.Cell(index).IsEmpty()) //ищем конец групп
                index++;
            var groupRow = currentRow.Row(4, index - 1); //строка групп
            //считать 1-ю строку
            currentRow = FindRaw(worksheet, "", index); //time to string
            var cellsAddress = new int[4];
            cellsAddress[0] = currentRow.FirstCellUsed().Address.RowNumber;
            cellsAddress[1] = firstGroup;
            cellsAddress[2] = groupRow.LastCellUsed().Address.ColumnNumber;
            cellsAddress[3] = cellsAddress[1] + 3;

            var currentRange = worksheet.Range(cellsAddress[0], cellsAddress[1], cellsAddress[2], cellsAddress[3]).RangeUsed();
            //идти по ней, проверяя на жирный текст
            //при нахождении искать первую границу справа, потом снизу
            for (int i = cellsAddress[1]; i <= cellsAddress[4]; i++)
            {
                //if (currentRange.Cell(0, i).Style.Font.Bold)

            }
            //"пр.", "пр", "" = практика (лишние отсечь), "лаб", "лаб." = лаба
            //ага, а про считать еще не подумал
            //если высота 4 - обе недели, 2 - нечетная
            //все это в range
            //лекция ли? проверяем по бэкграунду
            //если предущие выкладки верны, то в range чекаем ячейки
            //каждую непустую ячейку разбиваем по пробелам
            //... уже 3 стиля заполнения препода насчитал
            //"пр <фамилия>", "<фамилия>", "<фамилия> <И>. <О>."
            //"пр" отсекаем, одну букву + ""/"." отсекаем (пока что)
            //хмм... а могут ли они И.О. перед фамилией сунуть? в нашем нету, но зная лэти
            //последнее слово - аудитория (миша, все фигня)
            //у лекций дважды аудитория написана...
            //тогда нет цифр - преподом будешь, исключение "уит"
            //до меня только щас доперло, "пр" у преподов это метка практика, котороя промазала
            //хммм.... если ты будешь отсекать И.О. преподов, то... ну как бы же однофамильцы есть
            //окей, вместо выбрасывания запихиваем, остальным нули
            //снова почекал, только у савосина стоит "пр" и только на практиках
            //думаю имеет смысл и "пр.", "лаб", "лаб." тоже отсекать
            //а еще где то аудитории через слэш (не знаю, как это разносить, поэтому так суем)
            //еще интересно, в "УИТ3" они везде пишут "3" как цифру?, ладно, это щас не суть так важно
            //только щас дошло, что эксельку без фокуса листать могу, раньше выпендривалась
            //на каком чеке я остановился?
            //ага, аудитория
            //как только досматриваем весь range пушим в пары
            //мы знаем какое время и день чекаем, группу либо по сдвигу, либо из листа берем
            //если пара у нескольких групп?
            //у нас есть из range есть длина и стартовая точка, см. выше следовательно
            //вроде все
            //повторяем для 3-ей строки
            //повторяем для каждого времени в дне
            //повторяем для каждого дня (не забываем про пустую строку между ними)
            //кол-во дней мы знаем
            //что делать с вмп?
            //и тут все, что я выше писал, ломается
            //3 часа вникуда... достаем костыли
            //стоп, стоп, стоп
            //мы когда чекаем нижнию границу, то смотрим, сколько строк
            //если >4, то делим на 4 и в конце пушим столько пар со сдвигом времени
            //не очень красиво, но должно работать, если не начинается/заканчивается на опред неделе
            //но последнего, по идее, не должно быть
            //теперь все это надо в код перенести... времени нет

        }

        //private IXLAddress FindYourWay(IXLWorksheet worksheet, IXLRange range, int index)
        //{

        //}

        private void Parsing(string name)
        {
            Timetable data = new Timetable();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"../../../../Parser/Timetable.xml");
            XmlElement root = xDoc.DocumentElement;
            XmlElement timetable = root[name];
            foreach (XmlNode lists in timetable.ChildNodes)
                foreach (XmlNode para in lists.ChildNodes)
                {
                    Lesson lesson = new Lesson(para.Attributes["time"].Value,
                        para.Attributes["type"].Value, para.Attributes["group"].Value,
                        para.Attributes["room"].Value, para.Attributes["week"].Value,
                        para.Attributes["day"].Value, para.Attributes["teacher"].Value,
                        para.Attributes["name"].Value);

                    data.Lessons.Add(lesson);
                }
        }

        /// <summary>
        /// Искать строку с заданным значением
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="value">требуемое значение</param>
        /// <returns></returns>
        private IXLRow FindRaw(IXLWorksheet worksheet, string value, int startIndex)
        {
            IXLRow currentRow;
            do
            {
                currentRow = worksheet.Row(startIndex);
                startIndex++;
            }
            while (currentRow.Search(value, System.Globalization.CompareOptions.IgnoreCase, false) != null); //привести к адекватному виду
            return currentRow;
        }

        class Timetable : ITimetable
        {
            public IList<ILesson> Lessons { get; }

            public Timetable()
            {
                Lessons = new List<ILesson>();
            }
        }

        class Lesson : ILesson
        {
            public ISubject Subject { get; }
            public DateTime Time { get; }
            public LessonType Type { get; }
            public string Group { get; }
            public string Room { get; }
            public WeekType WeekType { get; }
            //public Day Day { get; }
            public ITeacher Teacher { get; }

            public Lesson(string time, string type, string group, string room,
                string weekType, string day, string teacher, string name)
            {
                string[] t = time.Split(':');
                Time = new DateTime(2019, 1, int.Parse(day), int.Parse(t[0]), (t[1][0]!='0')?int.Parse(t[1]):0, 0);
                switch (type)
                {
                    case "Laba": Type = LessonType.Laba; break;
                    case "Practice": Type = LessonType.Practice; break;
                    case "Lection": Type = LessonType.Lection; break;
                    case "Shared": Type = LessonType.Shared; break;
                }
                Group = group;
                Room = room;
                switch (weekType)
                {
                    case "0": WeekType = WeekType.Both; break;
                    case "1": WeekType = WeekType.Odd; break;
                    case "2": WeekType = WeekType.Even; break;
                }
                Teacher = new Teacher(teacher);
                Subject = new Subject(name);
            }
        }

        class Subject : ISubject
        {
            public string Name { get; }

            public Subject(string name)
            {
                Name = name;
            }
        }

        public class Teacher : ITeacher
        {
            public string Name { get; }
            public string Surname { get; }
            public string Patronymic { get; }

            public Teacher(string teacher)
            {
                Name = teacher;
                Surname = "";
                Patronymic = "";
            }
        }
    }
}