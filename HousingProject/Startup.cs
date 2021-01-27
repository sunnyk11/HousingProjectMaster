using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HousingProject.Startup))]
namespace HousingProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
