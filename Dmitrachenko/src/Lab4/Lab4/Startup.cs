using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab4.Startup))]
namespace Lab4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
