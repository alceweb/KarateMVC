using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KarateMVC.Startup))]
namespace KarateMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
