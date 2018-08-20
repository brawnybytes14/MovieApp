using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeltaX.Startup))]
namespace DeltaX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
