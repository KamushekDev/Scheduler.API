using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using Contracts.Parser;
using System.Collections.Generic;
using Contracts.Models;

namespace Parser
{
    public class LetiTimetableParser : ITimetableParser
    {
        public Task<ITimetable> ParseTimetable(Stream pathToFile, IProgress<IParserProgress> progressReporter = default, CancellationToken ct = default)
        {
            using var workbook = new XLWorkbook(pathToFile);
            var worksheets = workbook.Worksheets;
            ITimetable timetable = new Timetable();
            foreach (IXLWorksheet ws in worksheets)
                Parsing(ws,timetable);

            return Task.FromResult(timetable);

            //Ну а дальше развлекайся с Excel файлом
            //https://github.com/closedxml/closedxml
            //будет полезно тут глянуть на вкладочку Wiki

            //Чтобы понять что за прогресс и отмена можешь глянуть сюда:
            //https://devblogs.microsoft.com/dotnet/async-in-4-5-enabling-progress-and-cancellation-in-async-apis/
        }

        private void Parsing(IXLWorksheet worksheet, ITimetable timetable)
        {
            //нужно получить список групп
            //из них поймем длину таблицы (*кол-во столбцов)
            const int firstGroup = 4; //где неачинается полезная информация
            IXLRow currentRow = FindRaw(worksheet, "№ гр.", 1);
            var index = firstGroup; //начало групп
            while (!currentRow.Cell(index).IsEmpty()) //ищем конец групп
                index++;
            var lastGroup = index - 1;
            var groupRow = currentRow.Row(firstGroup, lastGroup); //строка групп
            var pastDaysBuffer = 0; //сдвиг строк относительно предыдущих дней

            //берем range n-го интервала времени для всех групп
            var groupInterval = GetGroupInterval(worksheet, groupRow.RowNumber()); //кол-во строк для номера группы

            for (var dayIndex = 0; dayIndex < 6; dayIndex++) //+совместные пары для групп
            {
                //определяем, сколько пар в этом дне (может еще в 19:05 и еще одна быть)
                var lessonsCount = 1;
                var dayColumn = worksheet.FirstColumnUsed();
                while (dayColumn.Cell(groupRow.FirstCell().Address.RowNumber + groupInterval - 1
                    + lessonsCount * 4 + pastDaysBuffer).Style.Border.BottomBorder != XLBorderStyleValues.Medium)
                    lessonsCount++;

                //начинаем проверять каждое время на пары
                for (var timeIndex = 0; timeIndex < lessonsCount; timeIndex++)
                {
                    index = groupRow.FirstCell().Address.RowNumber + groupInterval + 4 * timeIndex + pastDaysBuffer; //индекс обрабатываемой строки
                    var currentRange = worksheet.Range(index, firstGroup, index + 3, lastGroup);
                    //идти по ней, проверяя на жирный текст
                    //при нахождении искать первую границу справа, потом снизу
                    for (var i = 1; i <= lastGroup - firstGroup + 1; i++)
                    {
                        for (var k = 1; k < 4; k += 2)
                        {
                            var currentCell = currentRange.Cell(k, i);
                            if (currentCell.IsMerged() && (currentCell.MergedRange().FirstCell() != currentCell))
                                continue;
                            if (currentCell.Style.Font.Bold && (currentCell.Value.ToString() != ""))
                                timetable.Lessons.Add(GetLessonData(worksheet, groupRow, currentRange, i, dayIndex, timeIndex, k));
                            if (currentCell.IsMerged())
                            {
                                var mergedRange = currentCell.MergedRange();
                                for (var j = 1; j <= mergedRange.ColumnCount(); j++)
                                {
                                    string group = worksheet.Cell(groupRow.FirstCell().Address.RowNumber, mergedRange.Cell(1, j).Address.ColumnNumber).Value.ToString();
                                    Lesson l = timetable.Lessons[timetable.Lessons.Count - 1] as Lesson; //я бегло не могу сообразить, что здесь может пойти не так, а глубже я уже не в состояни вникать
                                    timetable.Lessons.Add(new Lesson(l.Time, l.Type, group, l.Room, l.WeekType, l.Day, l.Teacher, l.Subject)); //работает - не трожь
                                }
                            }
                        }
                    }
                }
                pastDaysBuffer += lessonsCount * 4 + 1;
            }

            //return timetable;
        }

        private Lesson GetLessonData(IXLWorksheet worksheet, IXLRangeRow groupRow, IXLRange currentRange, int currentColumn, int day, int time, int checkedWeek)
        {
            var lessonRange = SearchForLessonRange(worksheet, currentRange, checkedWeek, currentColumn);
            var name = lessonRange.Cell(1, 1).Value.ToString();
            string type;

            bool isLecture;
            try
            {
                isLecture = lessonRange.Cell(1, 1).Style.Fill.BackgroundColor.Color != XLColor.FromArgb(0, 255, 255, 255).Color;
            }
            catch
            {
                isLecture = true;
            }

            if (isLecture)
                type = "Lecture";
            else
            {
                Regex regex = new Regex(@"лаб[.]*", RegexOptions.IgnoreCase);
                if (regex.IsMatch(name))
                {
                    name = regex.Replace(name, "");
                    type = "Laba";
                }
                else
                {
                    regex = new Regex(@"пр[.]*", RegexOptions.IgnoreCase);
                    name = regex.Replace(name, "");
                    type = "Practice";
                }
            }
            name = name.Trim();

            var weekType = (lessonRange.RowCount() == 4) ? "Both" : (checkedWeek == 1) ? "Odd" : "Even"; //надо унифицировать для четных недель все

            var group = worksheet.Cell(groupRow.FirstCell().Address.RowNumber, currentRange.Cell(1, currentColumn).Address.ColumnNumber).Value.ToString();

            var lessonInfo = "";
            for (var i = 2; i <= lessonRange.RowCount(); i++)
                for (var j = 1; j <= lessonRange.ColumnCount(); j++)
                    if (!lessonRange.Cell(i, j).IsEmpty())
                        lessonInfo += lessonRange.Cell(i, j).Value.ToString() + " ";
            var rangeOfLessonInfo = lessonInfo.Split(' ');
            var room = "";
            var teacher = "";
            var teacherIO = "";
            foreach (string info in rangeOfLessonInfo)
            {
                if (info == "")
                    continue;
                if ((info.ToLower().Contains("уит")) || (new Regex("[0-9]+").IsMatch(info))) //Доразобраться с регулярками
                    room = info;
                else
                {
                    if ((info.Contains('.')) && (info != "пр.") && (info != "лаб."))
                    {
                        var ioRange = info.Split('.');
                        foreach (string io in ioRange)
                            if (io != "")
                                teacherIO += io + " ";
                        continue;
                    }
                    teacher = info;
                }
            }

            return new Lesson(((Time)time).ToString(), type, group, room, weekType, (DayOfWeek)(day + 1), teacher, name);
        }

        /// <summary>
        /// Возвращает адресс нижней правой ячейки пары
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="range"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private IXLRange SearchForLessonRange(IXLWorksheet worksheet, IXLRange range, int startRow, int startColumn)
        {
            var column = startColumn;
            var startCell = range.Cell(startRow, column).Address;
            var row = startRow;
            var lessonPrimaryBorderStyle = XLBorderStyleValues.Medium;
            var lessonSecondaryBorderStyle = XLBorderStyleValues.Thin;

            while ((range.Cell(row, column).Style.Border.RightBorder != lessonPrimaryBorderStyle) &&
            (range.Cell(row, column + 1).Style.Border.RightBorder != lessonPrimaryBorderStyle) &&
            (column < range.ColumnCount()))
                column++;

            while ((range.Cell(row, column).Style.Border.BottomBorder != lessonPrimaryBorderStyle) &&
            (range.Cell(row, column).Style.Border.BottomBorder != lessonSecondaryBorderStyle) &&
            (range.Cell(row + 1, column).Style.Border.TopBorder != lessonPrimaryBorderStyle) &&
            (range.Cell(row + 1, column).Style.Border.TopBorder != lessonSecondaryBorderStyle) &&
            (row < range.RowCount()))
                row++;

            return worksheet.Range(startCell, range.Cell(row, column).Address); //возвращает на 1 столбец меньше военки
        }

        /// <summary>
        /// Искать строку с заданным значением
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="targetString">требуемое значение</param>
        /// <returns></returns>
        private IXLRow FindRaw(IXLWorksheet worksheet, string targetString, int startIndex) //на самом деле можно возвращать адрес вхождения и по нему строить строку + ухнаем столбец начала групп
        {
            IXLRow currentRow;
            while (true)
            {
                currentRow = worksheet.Row(startIndex);
                var result = currentRow.Search(targetString, System.Globalization.CompareOptions.IgnoreCase, false); //69 for tea
                foreach (IXLCell a in result)
                    return currentRow;
                startIndex++;
            }
        }

        private int GetGroupInterval(IXLWorksheet ws, int groupRowNumber)
        {
            var i = groupRowNumber + 1;
            while (ws.Cell(i, ws.FirstColumnUsed().ColumnNumber()).Value.ToString().ToLower() != "понедельник")
                i++;

            return i - groupRowNumber;
        }

        private ITimetable Pars()
        {
            ITimetable data = new Timetable();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"../../../../Parser/G6374.xml");
            XmlElement root = xDoc.DocumentElement;
            XmlElement timetable = root["g6374"];
            foreach (XmlNode para in timetable.ChildNodes)
            {
                Lesson lesson = new Lesson(para.Attributes["time"].Value,
                    para.Attributes["type"].Value, para.Attributes["group"].Value,
                    para.Attributes["room"].Value, para.Attributes["week"].Value,
                    (DayOfWeek)int.Parse(para.Attributes["day"].Value), para.Attributes["teacher"].Value,
                    para.Attributes["name"].Value);

                data.Lessons.Add(lesson);
            }
            return data;
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
            public DayOfWeek Day { get; }
            public ITeacher Teacher { get; }

            public Lesson(string time, string type, string group, string room,
                string weekType, DayOfWeek day, string teacher, string name)
            {
                string[] t = time.Split('t', '_');
                Day = day;
                Time = new DateTime(2019, 1, 1, int.Parse(t[1]), (t[2][0] != '0') ? int.Parse(t[2]) : 0, 0);
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
                    case "Both": WeekType = WeekType.Both; break;
                    case "Odd": WeekType = WeekType.Odd; break;
                    case "Even": WeekType = WeekType.Even; break;
                }
                Teacher = new Teacher(teacher);
                Subject = new Subject(name);
            }

            public Lesson(DateTime time, LessonType type, string group, string room,
                WeekType weekType, DayOfWeek day, ITeacher teacher, ISubject name)
            {
                Time = time;
                Type = type;
                Group = group;
                Room = room;
                WeekType = weekType;
                Day = day;
                Teacher = teacher;
                Subject = name;
            }

            public override string ToString()
            {
                var result = Day.ToString() + " " + Time.Hour.ToString() + ":" + Time.Minute.ToString() + " " + Group.ToString() + " " + Subject.ToString() + " " + Type.ToString() +
                    " " + WeekType.ToString() + " " + Room.ToString() + " " + Teacher.ToString();

                return result;
            }
        }

        class Subject : ISubject
        {
            public string Name { get; }

            public Subject(string name)
            {
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        class Teacher : ITeacher
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

            public override string ToString()
            {
                return Name;
            }
        }
    }
}