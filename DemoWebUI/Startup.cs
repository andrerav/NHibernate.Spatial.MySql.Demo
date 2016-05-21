using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoWebUI.Startup))]
namespace DemoWebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
