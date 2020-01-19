using System.Collections.Generic;

namespace Contracts.Interfaces.Models
{
    public interface IUser
    {
        public int Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }
        public string Phone { get; }
        public string Email { get; }
        public ICollection<ICredential> Credentials { get; }
        public ICollection<IGroup> Groups { get; }
    }
}