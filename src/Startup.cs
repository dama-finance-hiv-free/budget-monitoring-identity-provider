using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using IdentityModel;
using IdentityProvider.Core;
using IdentityProvider.Data;
using IdentityProvider.Data.Entity;
using IdentityProvider.Models;
using IdentityProvider.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;

namespace IdentityProvider
{
    public class Startup
    {
        private const string AllowedOrigins = "AllowedOrigins";

        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var connectionSettings = new ConnectionSettings();
            Configuration.Bind("ConnectionStrings", connectionSettings);

            const string emailConfirmationToken = "emailConf";

            services.AddDbContext<ApplicationDbContext>(builder =>
                builder.UseNpgsql(connectionSettings.IdentityData,
                    sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            var identitySettings = new IdentitySettings();

            Configuration.Bind("IdentitySettings", identitySettings);


            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Tokens.EmailConfirmationTokenProvider = emailConfirmationToken;

                    options.Password.RequiredUniqueChars = 4;

                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

                    options.Password.RequiredLength = identitySettings.PasswordRequiredLength;
                    options.Password.RequireNonAlphanumeric = identitySettings.PasswordRequireNonAlphanumeric;
                    options.Password.RequireDigit = identitySettings.PasswordRequireDigit;
                    options.Password.RequireUppercase = identitySettings.PasswordRequireUppercase;
                    options.Password.RequireLowercase = identitySettings.PasswordRequireLowercase;

                    options.SignIn.RequireConfirmedEmail = identitySettings.SignInRequireConfirmedEmail;
                    options.SignIn.RequireConfirmedPhoneNumber = identitySettings.SignInRequireConfirmedPhoneNumber;
                    options.User.RequireUniqueEmail = identitySettings.UserRequireUniqueEmail;

                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>(emailConfirmationToken)
                .AddPasswordValidator<DoesNotContainPasswordValidator<ApplicationUser>>();

            services.AddRepositoryServices();

            services.AddApplicationServices();

            //var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? Array.Empty<string>();

            services.AddCors(options =>
            {
                options.AddPolicy(Startup.AllowedOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromHours(3));

            services.Configure<EmailConfirmationTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromDays(2));

            var keyFilePath = Configuration["SigninKeyCredentials:KeyFilePath"];
            var keyFilePassword = Configuration["SigninKeyCredentials:KeyFilePassword"];

            var cert = new X509Certificate2(keyFilePath, keyFilePassword);

            var identityServerBuilder = services.AddIdentityServer()
                .AddSigningCredential(cert);

            var authorizationEndpoint = Configuration["AppSettings:AuthorizationServer"];

            services.AddAuthentication()
                .AddIdentityServerAuthentication("token", options =>
                {
                    options.Authority = authorizationEndpoint;
                    options.ApiName = "damaapi";
                });

            identityServerBuilder.AddOperationalStore(options =>
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(connectionSettings.IdentityData,
                            sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
                .AddConfigurationStore(options =>
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(connectionSettings.IdentityData,
                            sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));
            
            identityServerBuilder.AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            
            if (!env.IsDevelopment()) app.UseHsts();

            app.UseAuthentication();

            InitializeDatabase(app);

            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(AllowedOrigins);

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            serviceScope?.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            serviceScope?.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
            serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

            var context = serviceScope?.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            if (context != null && !context.Clients.Any())
            {
                foreach (var client in Clients.Get())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (context != null && !context.IdentityResources.Any())
            {
                foreach (var resource in Resources.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (context != null && !context.ApiScopes.Any())
            {
                foreach (var scope in Resources.GetApiScopes())
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
                context.SaveChanges();
            }

            if (context != null && !context.ApiResources.Any())
            {
                foreach (var resource in Resources.GetApiResources())
                {
                    var entity = resource.ToEntity();
                    context.ApiResources.Add(entity);
                }
                context.SaveChanges();
            }

            var userManager = serviceScope?.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (userManager != null && userManager.Users.Any()) return;


            AddTestUsers(userManager);
        }

        private static void AddTestUsers(UserManager<ApplicationUser> userManager)
        {
            //doctor
            var doctor = new ApplicationUser("doctor@cbchs.cm")
            {
                Id = Guid.NewGuid().ToString(),
                Email = "doctor@cbchs.cm",
                FullName = "LINDA ASANJI",
                Locale = "en",
                PhoneNumber = "625225142",
                Organization = "101",
                UserCode = "0010",
                ImageUrl = ""
            };
            var doctorClaims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Email, "doctor@cbchs.cm"),
                new Claim(JwtClaimTypes.Role, "doctor")
            };

        }

    }
}
