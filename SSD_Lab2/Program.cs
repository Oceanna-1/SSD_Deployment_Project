using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SSD_Lab2.Data;
using Azure.Identity;

namespace SSD_Lab2 {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();


            builder.Services.AddControllersWithViews();
            var kvuri = new Uri(builder.Configuration.GetSection("KVURI").Value);
            var azcred = new DefaultAzureCredential();

            builder.Configuration.AddAzureKeyVault(kvuri, azcred);
            DbInitializer.employeePassword = 
                builder.Configuration.GetSection("secret-employee-cred").Value;
            DbInitializer.supervisorPassword =
                builder.Configuration.GetSection("secret-supervisor-cred").Value;

            var app = builder.Build();

            using ( var scope = app.Services.CreateScope() ) {
                DbInitializer.SeedUsersAndRoles(scope.ServiceProvider).Wait();
            }

            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment() ) {
                app.UseMigrationsEndPoint();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
