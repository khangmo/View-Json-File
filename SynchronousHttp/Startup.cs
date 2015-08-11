using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SynchronousHttp.Startup))]
namespace SynchronousHttp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
