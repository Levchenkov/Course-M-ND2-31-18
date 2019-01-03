namespace Lab4.Data.Contracts.Repositories
{
    public interface IContextFactory
    {
        IContext GetContext();
    }
}
