using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Parser
{
    public interface ITimetableParser
    {
        public Task<ITimetable> ParseTimetable(string pathToFile, IProgress<IParserProgress> progressReporter = default, CancellationToken ct = default);
    }
}