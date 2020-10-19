using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TMS_Project.Startup))]
namespace TMS_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
