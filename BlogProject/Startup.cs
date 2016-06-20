using BlogProject.Web;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace BlogProject.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}