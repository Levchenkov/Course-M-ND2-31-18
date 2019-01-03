using Lab4.Data.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data.Repositories
{

    public class ContextFactory : IContextFactory
    {
        public IContext GetContext()
        {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(@"Server = (LocalDb)\MSSQLLocalDB; Initial Catalog = aspnet-Lab4.Web-301A5A0A-42C0-4059-8385-DAED2C2EA178; Trusted_Connection= True");
                return new ApplicationDbContext(optionsBuilder.Options);
        }

    }
}
