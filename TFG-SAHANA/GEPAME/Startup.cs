using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GEPAME.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            services.AddMvc();

            #region auth
            // Add authentication services
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect("Auth0", options => {
                // Set the authority to your Auth0 domain
                options.Authority = $"https://{Configuration["Auth0:Domain"]}";

                // Configure the Auth0 Client ID and Client Secret
                options.ClientId = Configuration["Auth0:ClientId"];
                options.ClientSecret = Configuration["Auth0:ClientSecret"];

                // Set response type to code
                options.ResponseType = "code";

                // Configure the scope
                options.Scope.Clear();
                options.Scope.Add("openid");

                // Set the callback path, so Auth0 will call back to http://localhost:5000/signin-auth0 
                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
                options.CallbackPath = new PathString("/signin-auth0");

                // Configure the Claims Issuer to be Auth0
                options.ClaimsIssuer = "Auth0";

                // Saves tokens to the AuthenticationProperties
                options.SaveTokens = true;

                options.Events = new OpenIdConnectEvents
                {
                    // handle the logout redirection 
                    OnRedirectToIdentityProviderForSignOut = (context) =>
                    {
                        var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

                        var postLogoutUri = context.Properties.RedirectUri;
                        if (!string.IsNullOrEmpty(postLogoutUri))
                        {
                            if (postLogoutUri.StartsWith("/"))
                            {
                                // transform to absolute
                                var request = context.Request;
                                postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                            }
                            logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                        }

                        context.Response.Redirect(logoutUri);
                        context.HandleResponse();

                        return Task.CompletedTask;
                    }
                };
            });
            #endregion

            //#if DEBUG
            //var connection = Configuration.GetConnectionString("DefaultConnection");
            var connection = Configuration.GetConnectionString("AzureConnection");
            //            var connection = Configuration.GetConnectionString("MysqlConnection");
            //            services.AddDbContext<GEPAMEContext>(options => options.UseMySQL(connection));
            //#endif
            services.AddDbContext<GEPAMEContext>(options => options.UseSqlServer(connection));

            //services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
            //services.AddIdentity<IdentityUser, IdentityRole>()
            //  .AddEntityFrameworkStores<GEPAMEContext>();

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
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Init}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "GEPAME API V1.1");
            });
        }
    }
}
