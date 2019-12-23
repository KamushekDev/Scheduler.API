using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Contracts.Parser;
using System.Collections.Generic;

namespace Parser
{
    public class LetiTimetableParser : ITimetableParser
    {
        public Task<ITimetable> ParseTimetable(Stream pathToFile, IProgress<IParserProgress> progressReporter = default, CancellationToken ct = default)
        {
            using var workbook = new XLWorkbook(pathToFile);
            var worksheet = workbook.Worksheet("Data");

            Test(worksheet);

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
            //парсить 1-ю и 3-ю строки каждого времени (не, миша, ерунда)
            //считать 1-ю строку
            do
            //идти по ней, проверяя на жирный текст
            //"пр.", "пр", "" = практика (лишние отсечь), "лаб", "лаб." = лаба
            //при нахождении искать первую границу справа, потом снизу
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
    }

    class Timetable : ITimetable
    {

    }
}