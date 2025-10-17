using Company.BLL;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Data.DBContexts;
using Company.DAL.Models;
using Company.PL.Dependency_Injection;
using Company.PL.Helper.MailKitFeature;
using Company.PL.Helper.TwilioFeature;
using Company.PL.ProfilesMapping;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace Company.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
                //options.UseSqlServer(builder.Configuration["Default"]);
            });

            builder.Services.AddAutoMapper(M =>
            {
                M.AddProfile(new EmployeeProfile());
                M.AddProfile(new DepartmentProfile());
                M.AddProfile(new UserProfile());
                M.AddProfile(new RoleProfile());
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IScoped, Scoped>();
            builder.Services.AddTransient<ITransient, Transient>();
            builder.Services.AddSingleton<ISingleton, Singleton>();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.Configure<MailKitSetting>(builder.Configuration.GetSection("MailKitSetting"));
            builder.Services.AddScoped<IMailService, MailService>();

            builder.Services.Configure<TwilioSetting>(builder.Configuration.GetSection("TwilioSetting"));
            builder.Services.AddScoped<ITwilioService, TwilioService>();



            //builder.Services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            //}).AddGoogle(o =>
            //{
            //    o.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            //    o.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            //});



            //builder.Services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = FacebookDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
            //}).AddFacebook(o =>
            //{
            //    o.AppId = builder.Configuration["Authentication:Facebook:AppId"];
            //    o.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
            //});



            // ============= GOOGLE AUTH =============
            var googleConfig = builder.Configuration.GetSection("Authentication:Google");
            if (!string.IsNullOrEmpty(googleConfig["ClientId"]) &&
                !string.IsNullOrEmpty(googleConfig["ClientSecret"]))
            {
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = googleConfig["ClientId"];
                    options.ClientSecret = googleConfig["ClientSecret"];
                });
            }


            // ============= FACEBOOK AUTH =============
            var facebookConfig = builder.Configuration.GetSection("Authentication:Facebook");
            if (!string.IsNullOrEmpty(facebookConfig["AppId"]) &&
                !string.IsNullOrEmpty(facebookConfig["AppSecret"]))
            {
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = FacebookDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
                })
                .AddFacebook(options =>
                {
                    options.AppId = facebookConfig["AppId"];
                    options.AppSecret = facebookConfig["AppSecret"];
                });
            }





            builder.Services.ConfigureApplicationCookie(Config =>
            {
                Config.LoginPath = "/Account/Login";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
