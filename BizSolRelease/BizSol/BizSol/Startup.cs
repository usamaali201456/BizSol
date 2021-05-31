using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BizSol.Startup))]
namespace BizSol
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
