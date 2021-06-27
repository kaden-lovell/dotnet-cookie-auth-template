using System.IO;
using Server.Services;
using Server.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace source {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var keysDirectoryName = "Keys";
            var keysDirectoryPath = Path.Combine(@"C:\Users\kaden\Desktop\local_cookies", keysDirectoryName);
            if (!Directory.Exists(keysDirectoryPath)) {
                Directory.CreateDirectory(keysDirectoryPath);
            };

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keysDirectoryPath))
                .SetApplicationName("CustomCookieAuthentication");

            services.AddCors();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddAuthorization(options => {
                options.AddPolicy("TeacherRole", policy => policy.RequireRole("Teacher"));
                options.AddPolicy("StudentRole", policy => policy.RequireRole("Student"));
            });

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "source", Version = "v1" }); });

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevelopSqlServer")));
            services.AddScoped<SpeechAceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "source v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // NO don't use Strict!
            // Had issues with iPhone Chrome not working
            //app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
