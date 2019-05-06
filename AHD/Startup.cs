using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AHD.Startup))]
namespace AHD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
