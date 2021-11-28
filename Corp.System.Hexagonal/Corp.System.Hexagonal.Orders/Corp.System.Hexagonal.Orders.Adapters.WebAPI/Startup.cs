using AutoMapper;
using Corp.System.Hexagonal.Orders.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Corp.System.Hexagonal.Orders.Adapters.WebAPI
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

            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Corp.System.Hexagonal.Orders.Adapters.WebAPI", Version = "v1" });
            });

            //  Application
            // services.AddOrdersModuleDependency(Configuration);
            services.AddOrdersModuleDependency();

            // services.AddOrdersModuleDependency(opt => opt.UseInMemoryDatabase(databaseName: "TestCustomerContext"));

            //  AutoMapper
            var mapConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoXModelProfile>();
            });
            mapConfig.AssertConfigurationIsValid();
            services.AddSingleton<IMapper>(mapConfig.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Corp.System.Hexagonal.Orders.Adapters.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
