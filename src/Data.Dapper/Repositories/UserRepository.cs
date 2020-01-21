using System;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Repositories;
using Data.Dapper.Models;

namespace Data.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseAccess _databaseAccess;

        public UserRepository(DatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<IUser> GetById(int userId)
        {
            const string query =
                @"select * from users where id=@userId;";
            var response = await _databaseAccess.ExecuteQueryFirstOrDefaultAsync<UserDto>(query,
                new
                {
                    userId = userId
                });

            return response.ToModel();
        }

        public async Task<bool> UpdateUser(int userId, string name, string surname)
        {
            const string query =
                @"update users set name=@name, surname=@surname where id=@userId";
            var response =
                await _databaseAccess.ExecuteAsync(query, new {userId = userId, name = name, surname = surname});

            return response == 1;
        }

        public async Task<int> AddUser(string name, string surname, string patronymic = null, string phone = null,
            string email = null)
        {
            const string query =
                @"insert into users (name, surname, patronymic, phone, email) 
                         values (@name, @surname, @patronymic, @phone, @email) RETURNING id";
            var response = await _databaseAccess.ExecuteQueryFirstOrDefaultAsync<int>(query,
                new
                {
                    name = name,
                    surname = surname,
                    patronymic = patronymic,
                    phone = phone,
                    email = email
                });

            return response;
        }

        public async Task<bool> JoinGroup(int userId, int groupId)
        {
            const string query =
                @"insert into users_to_groups (user_id, group_id, date_entry) values (@userId, @groupId, @dateEntry);";

            var response = await _databaseAccess.ExecuteAsync(query,
                new {userId = userId, groupId = groupId, dateEntry = DateTime.Now});

            return response == 1;
        }

        public async Task<bool> LeaveGroup(int userId, int groupId)
        {
            const string query =
                @"delete from users_to_groups where group_id=@groupId and user_id=@userId;";

            var response = await _databaseAccess.ExecuteAsync(query, new {userId = userId, groupId = groupId});

            return response == 1;
        }
    }
}