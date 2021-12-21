using MaintenanceWebsite.Data;
using MaintenanceWebsite.Models;
using MaintenanceWebsite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using MaintenanceLibrary.DataAccess;
using Serilog;
using MaintenanceWebsite.OtherMethods;

namespace MaintenanceWebsite
{
    public class Startup
    {
        IServiceProvider ServiceProvider;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            
            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"]
                )
            );

            
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <param name="env"><see cref="IWebHostEnvironment"/></param>
        /// <param name="configuration"><see cref="IConfiguration"/></param>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/></param>
        /// <param name="backgroundJobs"><see cref="IBackgroundJobClient"/></param>
        /// <param name="emailSender"><see cref="IEmailSender"/></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration
            , IServiceProvider serviceProvider
            , IBackgroundJobClient backgroundJobs, IEmailSender emailSender)
        {
            ServiceProvider = serviceProvider;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            
            app.UseHangfireDashboard();

            // Wires MaintenanceLibrary up so it can be used to run queries.
            SQLDataAccess.DefaultConnection = configuration.GetConnectionString("DefaultConnection");

            var manager = new RecurringJobManager();

            manager.AddOrUpdate("downtimeIssue12hour12", () => EmailHelpers.SendEmailAboutDowntimeIssue12hours((EmailSender)emailSender), "30 0 * * *");
            manager.AddOrUpdate("downtimeIssue12hour0", () => EmailHelpers.SendEmailAboutDowntimeIssue12hours((EmailSender)emailSender), "30 12 * * *");
            manager.AddOrUpdate("MROPastPromiseDate", () => EmailHelpers.SendEmailAboutRepairPartsMonday((EmailSender)emailSender), "0 2 * * 1");

            // Uncomment this if you are doing an initial install, that is setting up a new database.
            CreateRoles(ServiceProvider).Wait();
        }

        /// <summary>
        /// Create a initial set of roles that work with this application.
        /// Also, create is a power user.  You can change that information in the appsettings.json
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // Makes default roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            string[] roleNames = { "Admin", "Supervisor", "Mechanic", "MRO Supervisor", "MRO" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 2
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

           // Makes a Power User if new install.
           var poweruser = new AppUser
           {
               UserName = Configuration["AppSettings:UserName"],
               Email = Configuration["AppSettings:UserEmail"],
           };

            string userPWD = Configuration["AppSettings:UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role : Question 3
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }
    }
}

