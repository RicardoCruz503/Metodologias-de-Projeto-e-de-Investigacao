using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Salvanimal_Auth.Startup))]
namespace Salvanimal_Auth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
