using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmpManaement.Startup))]
namespace EmpManaement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
