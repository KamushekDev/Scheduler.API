using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Repositories;
using Dapper;
using Data.Dapper.Models;
using Npgsql;

namespace Data.Dapper.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DatabaseAccess _databaseAccess;

        public GroupRepository(DatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<IGroup> GetById(int groupId)
        {
            const string query =
                @"select id as groupId,
                         name as groupName,
                         invite_tag as inviteLink
                  from groups
                  where id = @groupId;";
            var response = await _databaseAccess.ExecuteQueryFirstOrDefaultAsync<GroupDto>(query,
                new
                {
                    groupId = groupId
                });

            return response.ToModel();
        }

        public async Task<IGroup> GetByInviteLink(string inviteLink)
        {
            const string query =
                @"select id as groupId,
                         name as groupName,
                         invite_tag as inviteLink
                  from groups
                  where invite_tag = @inviteLink;";
            var response = await _databaseAccess.ExecuteQueryFirstOrDefaultAsync<GroupDto>(query,
                new
                {
                    inviteLink = inviteLink
                });

            return response.ToModel();
        }

        public async Task<IEnumerable<IGroup>> GetUserGroups(int userId)
        {
            const string query =
                @"select id as groupId,
                         name as groupName,
                         role as userRole,
                         date_entry as entryDate,
                         invite_tag as inviteLink
                  from groups
                  left join users_to_groups utg on groups.id = utg.group_id
                  where date_exit is null and user_id = @userId;";
            var response = await _databaseAccess.ExecuteQueryAsync<GroupDto>(query,
                new
                {
                    userId = userId
                });

            return response.Select(x => x.ToModel()).ToArray();
        }

        public async Task<bool> MakeGroupPublic(int groupId)
        {
            const string query =
                @"update groups set access= @access where id=@groupId;";

            var result = await _databaseAccess.ExecuteAsync(query, new NpgsqlParameter("access", AccessModifier.Public),
                new NpgsqlParameter("groupId", groupId));

            return result == 1;
        }

        public async Task<int> CreateGroup(int userId, string name, string description = null)
        {
            var tag = Math.Abs($"{name}_{description}_{DateTime.Now}".GetHashCode()).ToString();

            const string query =
                @"insert into groups (name, invite_tag, description) values (@name, @tag, @description) returning id;";

            await using var connection = await _databaseAccess.GetConnectionAsync();

            var transaction = await connection.BeginTransactionAsync();
            
            try
            {
                var groupId = await connection.ExecuteScalarAsync<int>(query, new
                {
                    name = name,
                    description = description,
                    tag = tag
                });
                const string query2 =
                    @"insert into users_to_groups (user_id, group_id, role, date_entry) values (@userId, @groupId, 'Headman', NOW());";

                var result = await connection.ExecuteAsync(query2, new {userId = userId, groupId = groupId});

                if (result == 1)
                    await transaction.CommitAsync();
                else
                    await transaction.RollbackAsync();

                return groupId;
            }
            catch
            {
                await transaction.DisposeAsync();
            }

            return -1;
        }

        public async Task<IEnumerable<IGroup>> GetPublicGroupsWithoutUser(int userId)
        {
            const string query =
                @"select id as groupId,
                         name as groupName,
                         role as userRole,
                         date_entry as entryDate,
                         invite_tag as inviteLink
                  from groups
                  left join users_to_groups utg on groups.id = utg.group_id
                  where date_exit is null and access = 'Public' and (user_id is null or user_id != @userId);";
            var response = await _databaseAccess.ExecuteQueryAsync<GroupDto>(query,
                new
                {
                    userId = userId
                });

            return response.Select(x => x.ToModel()).ToArray();
        }
    }
}