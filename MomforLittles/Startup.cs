using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MomforLittles.Startup))]
namespace MomforLittles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
