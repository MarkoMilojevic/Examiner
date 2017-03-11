using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Examiner.Web.Startup))]
namespace Examiner.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
