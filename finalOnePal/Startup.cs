using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(finalOnePal.Startup))]
namespace finalOnePal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
