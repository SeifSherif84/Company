using Company.BLL;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Data.DBContexts;
using Company.DAL.Models;
using Company.PL.Dependency_Injection;
using Company.PL.ProfilesMapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#region MyRegion
// https://chatgpt.com/share/68cdd6c3-1f14-8005-96d4-5db596ba34d7 
// https://chatgpt.com/share/68db9306-062c-800f-ae25-eccb225a7ae0
#endregion

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
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IScoped, Scoped>();
            builder.Services.AddTransient<ITransient, Transient>();
            builder.Services.AddSingleton<ISingleton, Singleton>();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>();


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
