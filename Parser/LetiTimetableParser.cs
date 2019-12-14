using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Contracts.Parser;

namespace Parser
{
    public class LetiTimetableParser : ITimetableParser
    {
        public Task<ITimetable> ParseTimetable(Stream pathToFile, IProgress<IParserProgress> progressReporter = default, CancellationToken ct = default)
        {
            using var workbook = new XLWorkbook(pathToFile);
            
            //Ну а дальше развлекайся с Excel файлом
            //https://github.com/closedxml/closedxml
            //будет полезно тут глянуть на вкладочку Wiki
            
            //Чтобы понять что за прогресс и отмена можешь глянуть сюда:
            //https://devblogs.microsoft.com/dotnet/async-in-4-5-enabling-progress-and-cancellation-in-async-apis/
            
            throw new NotImplementedException();
        }
    }
}