namespace ItNews.Data.Contracts
{
    public interface IDbScopeFactory
    {
        IDbScope GetNewScope();

        IDbScope GetSharedScope();
    }
}
