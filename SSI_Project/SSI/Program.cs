using Microsoft.EntityFrameworkCore;
using SSI.Models;
using SSI.Data;
using SSI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using SSI.Ultils.Mapper;
using Microsoft.Extensions.Configuration;
using SSI.Ultils;
using SSI.Services.Service;
using Microsoft.Data.SqlClient;
using SSI.Services.IService;
using SSI.Data.Repository;
using SSI.Data.IRepository;
namespace SSI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IInvestmentRequestRepository, InvestmentRequestRepository>();
            builder.Services.AddScoped<IIdeaRepository, IdeaRepository>();
            builder.Services.AddScoped<IIdeaService, IdeaService>();
            builder.Services.AddScoped<IInvestmentRequestService, InvestmentRequestService>();

            builder.Services.AddDbContext<SSIV2Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                       .EnableSensitiveDataLogging()
                       .LogTo(Console.WriteLine);
            });
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
            builder.Services.AddAutoMapperConfig();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddTransient<EmailService>();
            builder.Services.AddTransient<CloudinaryService>();
            builder.Services.AddRepository();
            builder.Services.AddService();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "";
                options.SlidingExpiration = true;
                options.Cookie.Name = "SSI";

            });
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