using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Parser;

namespace Parser
{
    public class LetiTimetableParser : ITimetableParser
    {
        public Task<ITimetable> ParseTimetable(string pathToFile, IProgress<IParserProgress> progressReporter = default, CancellationToken ct = default)
        {
            
            throw new NotImplementedException();
        }
    }
}