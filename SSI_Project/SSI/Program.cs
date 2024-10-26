using Microsoft.EntityFrameworkCore;
using SSI.Models;
using SSI.Data;
using SSI.Services;
using SSI.Data.Repository;
using SSI.Data.IRepository;
using SSI.Services.Service;
using SSI.Services.IService;
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
            builder.Services.AddRepository();
            builder.Services.AddService();
            builder.Services.AddScoped<IInvestmentRequestRepository, InvestmentRequestRepository>();
            builder.Services.AddScoped<IInvestmentRequestService, InvestmentRequestService>();
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}