namespace Lab4.Domain.Contracts
{
    public interface IUserService
    {
        void AddUser(string id);
        bool FindUser(string id);
    }
}
