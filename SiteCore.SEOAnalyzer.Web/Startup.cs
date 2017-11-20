using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiteCore.SEOAnalyzer.Web.Startup))]
namespace SiteCore.SEOAnalyzer.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
