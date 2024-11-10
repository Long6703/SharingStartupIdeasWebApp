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
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using SSI.Hubs;
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
            builder.Services.AddSignalR();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/accessdenied";
                options.Cookie.Name = "SSI";
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                options.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = async context =>
                    {
                        var userPrincipal = context.Principal;
                        if (userPrincipal == null || !userPrincipal.Identity.IsAuthenticated)
                        {
                            Console.WriteLine("User has an invalid cookie but is not authenticated.");

                            context.RejectPrincipal();
                            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        }
                    }
                };


            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });
            builder.Services.AddTransient<AdminEmailService>();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthorization();

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
            app.MapHub<SignalRServer>("/hub");
            app.Run();
        }
    }
}