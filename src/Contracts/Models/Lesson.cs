namespace Contracts.Models
{
    public class Lesson
    {
        public string Start_Time { get; set; }
        public string End_Time { get; set;  }
        public int Day_Number { get; set; }
        public string Lesson_Description { get; set; }
        public string Lesson_Name { get; set; }
        public string Room { get; set; }
        public string Teacher_Name { get; set; }
        public string Teacher_Surname { get; set; }
        public string Teacher_Patronymic { get; set; }
        public string Lesson_Type { get; set; }
    }
}