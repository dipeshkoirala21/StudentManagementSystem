using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalFinalCoursework.Startup))]
namespace FinalFinalCoursework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
