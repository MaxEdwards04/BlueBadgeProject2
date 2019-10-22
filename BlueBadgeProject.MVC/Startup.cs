using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlueBadgeProject.MVC.Startup))]
namespace BlueBadgeProject.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
