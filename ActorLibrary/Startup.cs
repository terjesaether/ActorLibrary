using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ActorLibrary.Startup))]
namespace ActorLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
