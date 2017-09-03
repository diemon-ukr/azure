using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITStepWebApp1.Startup))]
namespace ITStepWebApp1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
