namespace Contracts.Models
{
    public interface IUser
    {
        int Id { get; }
        string Name { get; }
        string Surname { get; }
        string Patronymic { get; }
        string Phone { get; }
        string Email { get; }
    }
}