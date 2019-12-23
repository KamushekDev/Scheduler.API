using System;
using System.Collections.Generic;
using System.Linq;

namespace Contracts.Parser
{
    public interface ITimetable
    {
        
        //Тут делай как тебе будет удобно, потому что преобразовывать это всё равно придётся в наш формат
        //Единственное требование к этому в том, чтобы я мог легко и быстро достать все названия предметов и поменять предмет внутри расписания

        IList<ILesson> Lessons { get; }

        IEnumerable<ISubject> Subjects => Lessons.Select(x => x.Subject);
    }

    public interface ISubject
    {
        string Name { get; }
    }
    
    public interface ILesson
    {
        ISubject Subject { get; }

        /// <summary>
        /// Заполняй тут только время
        /// </summary>
        DateTime Time { get; }

        LessonType Type { get; }

        string Group { get; }
        
        string Room { get; }
        
        WeekType WeekType { get; }
        
        ITeacher Teacher { get; }
    }

    public enum WeekType
    {
        Even,
        Odd,
        Both
    }
    
    public interface ITeacher
    {
        //Иванов П. К. => Name="П" Surname="Иванов" Patronymic="К"

        string Name { get; }
        string Surname { get; }
        string Patronymic { get; }
    }

    public enum LessonType
    {
        Practice,
        Lection,
        Laba,
        Shared
        //Ну и чо там ещё есть
        //напиши тут лучше нормально, я от балды ебанул
    }
}