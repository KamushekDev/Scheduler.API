using Contracts.Models;

namespace Data.Dapper.Models
{
    public class UserDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string phone { get; set; }
        public string email { get; set; }


        public User ToModel()
        {
            return new User(id, name, surname, patronymic, phone, email);
        }

        public class User : IUser
        {
            public int Id { get; }
            public string Name { get; }
            public string Surname { get; }
            public string Patronymic { get; }
            public string Phone { get; }
            public string Email { get; }

            public User(int id, string name, string surname, string patronymic, string phone, string email)
            {
                Id = id;
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Phone = phone;
                Email = email;
            }
        }
    }
}