using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalFestival2015.Startup))]
namespace SalFestival2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
