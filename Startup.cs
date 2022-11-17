using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DisasterAllevitionFoundationproject.Startup))]
namespace DisasterAllevitionFoundationproject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
