using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Salvanimal.Startup))]
namespace Salvanimal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
