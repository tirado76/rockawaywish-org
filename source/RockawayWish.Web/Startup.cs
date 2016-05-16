using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RockawayWish.Web.Startup))]
namespace RockawayWish.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
