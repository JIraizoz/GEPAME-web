using System;
using System.Collections.Generic;
using System.Data.Common;
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
using MySql.Data.MySqlClient;
using Swashbuckle.AspNetCore.Swagger;

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
            //#if DEBUG
            //var connection = Configuration.GetConnectionString("DefaultConnection");
            var connection = Configuration.GetConnectionString("AzureConnection");
            //            var connection = Configuration.GetConnectionString("MysqlConnection");
            //            services.AddDbContext<GEPAMEContext>(options => options.UseMySQL(connection));
            //#endif
            services.AddDbContext<GEPAMEContext>(options => options.UseSqlServer(connection));
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));

            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<GEPAMEContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.1", new Info
                {
                    Version = "v1.1",
                    Title = "GEPAME API REST",
                    Description = "Descripcion of the API REST",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "J. Iráizoz", Email = "jesusiraizoz@gmail.com", Url = "https://www.jiraizoz.es" }
                });
            });
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "GEPAME API V1.1");
            });
        }
    }
}
