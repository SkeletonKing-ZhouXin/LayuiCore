using LC.Core.Infrastructure.Engine;
using LC.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LC.Web.Framework.Infrastructure.Startup
{
    public class MvcStartup : ISelfStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(); //动态编译，添加html代码不需要重新编译

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultChallengeScheme = "Authentication";
            //    options.DefaultScheme = "Authentication";
            //    options.DefaultSignInScheme = "ExternalAuthentication";
            //});

            services.AddSession(); //注入session
        }

        public void Configure(IApplicationBuilder app)
        {
            var webHostEnvironment = EngineContext.Current.Resolve<IWebHostEnvironment>();

            if (webHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller}/{action}/{id?}",
                defaults: new { controller = "Account", action = "Login" });
            });
            //pattern: "{controller=Account}/{action=Login}/{id?}");
        }

        public int Order { get; } = 2;
    }
}
