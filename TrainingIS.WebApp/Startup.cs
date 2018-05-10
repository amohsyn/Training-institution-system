using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrainingIS.WebApp.Startup))]
namespace TrainingIS.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
