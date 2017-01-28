using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocRepositoryWeb.Startup))]
namespace DocRepositoryWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
