using Owin;

namespace SSW.WebApiHelper.Test
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
