using Microsoft.EntityFrameworkCore;
using SSI.Models;
using SSI.Data;
using SSI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using SSI.Ultils.Mapper;
namespace SSI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<SSIV2Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "";
                options.AccessDeniedPath = "";
                options.SlidingExpiration = true;
                options.Cookie.Name = "SSI";

            });
            builder.Services.AddRepository();
            builder.Services.AddService();
            builder.Services.AddAutoMapperConfig();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            // Apply authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}