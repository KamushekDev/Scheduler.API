using System.Threading.Tasks;
using System.IO;
using Xunit;

namespace Parser.Tests
{
    public class FktiTest
    {
        [Fact]
        public async Task Fkti_4_1_Test()
        {
            var parser = new LetiTimetableParser();

            var file = File.OpenRead(@"../../../TestFiles/fkti-4-1.xlsx");
            
            var result = await parser.ParseTimetable(file);
            
            Assert.Equal(358, result.Lessons.Count);
        }
        
        [Fact]
        public async Task Fkti_4_2_Test()
        {
            var parser = new LetiTimetableParser();

            var file = File.OpenRead(@"../../../TestFiles/fkti-4-2.xlsx");
            
            var result = await parser.ParseTimetable(file);
            
            Assert.Equal(377, result.Lessons.Count);
        }
    }
}