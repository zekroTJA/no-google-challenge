using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ToDoList.Database;
using ToDoList.Modules;

namespace ToDoList
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
            var jwtSigningKey = Configuration.GetValue<string>("Authorization:JWTSigningKey");

            services
                .AddDbContext<Context>(ctx =>
                    ctx.UseNpgsql(Configuration.GetConnectionString("postgres")))
                .AddSingleton<IPasswordHasher, Argon2idHasher>()
                .AddSingleton<IAuthorization>((services) => jwtSigningKey == null
                    ? new JWTAuthorization(services.GetService<ILogger<JWTAuthorization>>())
                    : new JWTAuthorization(services.GetService<ILogger<JWTAuthorization>>(), jwtSigningKey))
                ;
            
            services.AddControllers();
            services.AddSwaggerGen(c => 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoList", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            using (var db = scope.ServiceProvider.GetService<Context>())
            {
                db.Database.Migrate();
            }
        }
    }
}
