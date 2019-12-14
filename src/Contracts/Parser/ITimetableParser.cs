using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Parser
{
    public interface ITimetableParser
    {
        public Task<ITimetable> ParseTimetable(Stream file, IProgress<IParserProgress> progressReporter = default, CancellationToken ct = default);
    }
}