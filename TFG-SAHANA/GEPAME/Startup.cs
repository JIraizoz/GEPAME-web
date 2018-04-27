using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GEPAME.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GEPAME
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<IdentityOptions>(options =>
            //{
            //    // avoid redirecting REST clients on 401
            //    options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
            //    {
            //        OnRedirectToLogin = ctx =>
            //        {
            //            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //            return Task.FromResult(0);
            //        }
            //    };
            //});

            services.AddMvc();
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<GEPAMEContext>(options => options.UseSqlServer(connection));
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));

            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<GEPAMEContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //app.UseIdentity();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
