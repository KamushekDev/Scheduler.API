namespace Contracts.Models
{
    public interface IUser
    {
        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }
        public string Phone { get; }
        public string Email { get; }
    }
}