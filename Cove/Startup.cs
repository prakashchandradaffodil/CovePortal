using Cove.Application.Interfaces;
using Cove.Application.Services;
using Cove.ClassLibrary.Data;
using Cove.ClassLibrary.Interfaces;
using Cove.ClassLibrary.Model;
using Cove.ClassLibrary.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cove
{
    public class Startup
    {
        private string FacebookAppId { get; set; }
        private string FacebookAppSecret { get; set; }
        private string GoogleAppId { get; set; }
        private string GoogleAppSecret { get; set; }

        public IConfiguration Configuration { get; }
        private readonly IHostEnvironment _hostingEnvironment;
        public Startup(IHostEnvironment env)
        {
            _hostingEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            FacebookAppId = Configuration.GetSection("ExternalLoginCredentials").GetSection("Authentication:Facebook:AppId").Value;
            FacebookAppSecret = Configuration.GetSection("ExternalLoginCredentials").GetSection("Authentication:Facebook:AppSecret").Value;
            GoogleAppId = Configuration.GetSection("ExternalLoginCredentials").GetSection("Authentication:Google:AppId").Value;
            GoogleAppSecret = Configuration.GetSection("ExternalLoginCredentials").GetSection("Authentication:Google:AppSecret").Value;

            services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(
           Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUserIdentity, ApplicationRole>(options =>
            {
                //options.Password.RequiredLength = 10;
                //options.Password.RequiredUniqueChars = 3;
            })
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

            services.AddAuthentication()
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = GoogleAppId;
                googleOptions.ClientSecret = GoogleAppSecret;
                googleOptions.SignInScheme = IdentityConstants.ExternalScheme;
            })
              .AddFacebook(facebookOptions =>
              {
                  facebookOptions.AppId = FacebookAppId;
                  facebookOptions.AppSecret = FacebookAppSecret;
                  facebookOptions.SignInScheme = IdentityConstants.ExternalScheme;
              });

            services.AddControllersWithViews();

            services.AddScoped<IAccountRepo, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepo, UserRepository>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetRepo, AssetRepository>();
            services.AddScoped<IUploadComicRepo, UploadComicRepo>();
            services.AddScoped<IUploadComicService, UploadComicService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                  //pattern: "{controller=ManageAccount}/{action=ManageAccountOptions}");
        pattern: "{controller=account}/{action=welcome}");
        });
        }
    }
}
