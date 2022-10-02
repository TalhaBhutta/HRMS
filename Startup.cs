using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMS.Models;
//using HRMS.Email;
using HRMS.Data;
using Microsoft.AspNetCore.Identity.UI.Services;


//using Microsoft.AspNetCore.Identity.UI;


namespace HRMS
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
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationDBContext")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; //To be changed to true
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            //services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,AdditionalUserClaimsPrincipalFactory>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            //.AddDefaultUI()

            /*Session Timeout*/
            services.AddSession(options => {
                //options.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            //IdentityRegistrar.Register(services);                  // No change
            //AuthConfigurer.Configure(services, _appConfiguration); // No change

            services.ConfigureApplicationCookie(o =>
            {
                //o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                //o.SlidingExpiration = true;
            });
            /*               */

            //services.AddMvc();

            //services.AddTransient<IEmailSender, YourEmailSender>();
            //services.AddTransient<IEmailSender, YourSmsSender>();
            //services.AddMvc().AddRazorPagesOptions(o => o.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model =>
            //{
            //    foreach (var selector in model.Selectors)
            //    {
            //        var attributeRouteModel = selector.AttributeRouteModel;
            //        attributeRouteModel.Order = -1;
            //        attributeRouteModel.Template = attributeRouteModel.Template.Remove(0, "Identity".Length);
            //    }
            //})
            //    ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //.AddDefaultTokenProviders(); /*?*/

            //services.AddMvc().WithRazorPagesRoot("/Areas/Identity");
            //services.AddMvc().AddRazorPagesOptions(options => {
            //    options.Conventions.AddAreaPageRoute("Identity", "/Account/Manage/Index", "");
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddControllersWithViews();

            //services.AddDbContext<EmployeeDBContext>(options =>
            //   options.UseSqlServer(Configuration.GetConnectionString("EmployeeDBContext")));
            //services.AddRazorPages();
            //services.AddRazorPages()
            //        .AddRazorPagesOptions(options =>
            //        {
            //            options.RootDirectory = "/Areas/Identity";
            //        });

            //services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddTransient<IEmailSender, AuthoMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            //app.UseMvc();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            CreateRoles(serviceProvider);
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
    {
        //initializing custom roles 
        var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //string[] roleNames = { "Admin", "Manager", "Member" };
        string[] roleNames = {  "Employee","Recruter","Customer","Employee And Recruter", "Customer Manager", "Customer Owner/Director", "Customer Accountant",
                                "Customer HR", "System Admin", "CS", "Lower Customer Service","System User", "Developer", "Owner"};
        Task<IdentityResult> roleResult;


        foreach (var roleName in roleNames)
        {
            Task<bool> roleExist = RoleManager.RoleExistsAsync(roleName);
            roleExist.Wait();
            
            if (!roleExist.Result)
            {
                //create the roles and seed them to the database: Question 2
                roleResult = RoleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }
        }


        //Here you could create a super user who will maintain the web app
        var poweruser = new ApplicationUser
        {

            UserName = Configuration["AppSettings:UserName"],
            Email = Configuration["AppSettings:UserEmail"],
        };
        //Ensure you have these values in your appsettings.json file
        string userPWD = Configuration["AppSettings:UserPassword"];
        var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]);

       if(_user == null)
       {
            var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
            if (createPowerUser.Succeeded)
            {
                //here we tie the new user to the role
                await UserManager.AddToRoleAsync(poweruser, "Admin");

            }
       }
    }
    }
}
