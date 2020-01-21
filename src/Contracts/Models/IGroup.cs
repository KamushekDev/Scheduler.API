namespace Contracts.Models
{
    public interface IGroup
    {
        public int Id { get; }
        public string GroupName { get; }
        public string InviteLink { get; }
    }
}