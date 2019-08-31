using basic_auth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using NSwag.Generation.Processors.Security;

namespace basic_auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

            services.AddOpenApiDocument(document => {
                document.Title = "Sample API with Basic Authentication";
                document.Description = "This is a sample OpenApi3 API with Basic Authentication. Try it with test/testpass";
                document.AddSecurity("basic", new NSwag.OpenApiSecurityScheme() {
                    Type = NSwag.OpenApiSecuritySchemeType.Http,
                    Scheme = "basic"
                });
                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("basic"));

                document.PostProcess = d => d.Info.Contact = new NSwag.OpenApiContact() { 
                    Name = "XlApi Demo Api Developers",
                    Url = "https://xlapi.app",
                    Email = "apiteam@xlapi.app" 
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseOpenApi(); // serve OpenAPI/Swagger documents
            app.UseSwaggerUi3(); // serve Swagger UI

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
