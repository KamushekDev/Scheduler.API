using System;
using System.IO;
using System.Threading.Tasks;
using Contracts.Parser;
using Xunit;

namespace Parser.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task TrialTest()
        {
            ITimetableParser parser = new LetiTimetableParser();

            var file = File.OpenRead("pathToFile");
            
            var result = await parser.ParseTimetable(file); //ну прогресс и токен тут ещё с:
            
            Assert.True(false);
            //Ну думаю ты понял как это работает. если нет. то можешь глянуть тут:
            //https://xunit.net/#documentation
            //Но тебе очевидно не нужно писать тесты на парсер. томуша пошёл он нахуй.
            //Это просто чтобы ты мог дебагать парсер без воздействия на остальной проект
            //Но если в итоге пока будешь теститься напишешь тест, то норм)
        }
    }
}