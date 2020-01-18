using System.Collections.Generic;

namespace Contracts.Models
{
    public interface IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<ICredential> Credentials { get; set; }
        public ICollection<IGroup> Groups { get; set; }
    }
}