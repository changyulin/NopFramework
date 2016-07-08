using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nop.Admin.Startup))]
namespace Nop.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
