using Contracts.Models;

namespace Data.Dapper.Models
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public User ToModel()
        {
            return new User(Name, Surname, Patronymic, Phone, Email);
        }

        public class User : IUser
        {
            public string Name { get; }
            public string Surname { get; }
            public string Patronymic { get; }
            public string Phone { get; }
            public string Email { get; }

            public User(string name, string surname, string patronymic, string phone, string email)
            {
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Phone = phone;
                Email = email;
            }
        }
    }
}