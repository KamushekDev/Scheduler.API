namespace Contracts.Models
{
    public interface IGroup
    {
        public int Id { get; }
        public string Name { get; }
        public string InviteLink { get; }
    }
}