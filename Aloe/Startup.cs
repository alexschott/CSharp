using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aloe.Startup))]
namespace Aloe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
