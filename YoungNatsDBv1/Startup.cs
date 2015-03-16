using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YoungNatsDBv1.Startup))]
namespace YoungNatsDBv1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
