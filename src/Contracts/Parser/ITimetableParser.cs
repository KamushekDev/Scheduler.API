using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Contracts.Parser
{
    public interface ITimetableParser
    {
        public Task<List<ITimetable>> ParseTimetable(Stream file, IProgress<IParserProgress> progressReporter = default, CancellationToken ct = default);
    }
}