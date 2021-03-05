using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BustadirForm.Startup))]
namespace BustadirForm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}