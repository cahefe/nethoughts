using System;
using Hangfire;
using Hangfire.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebAPIHangFire
{
    //  https://code.4noobz.net/hangfire-quickstart-simple-tutorial/
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
            var SQLiteOptions = new SQLiteStorageOptions()
            {
                SchemaName = "HangFireSchema",
            };

            services.AddHangfire(config => config
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSQLiteStorage("Filename=./Hangfire.db;", SQLiteOptions));
            services.AddHangfireServer();

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHangfireDashboard();
            //  Testando jobs...
            BackgroundJob.Enqueue(() => Console.WriteLine("Hello backgroundjobs."));
            RecurringJob.AddOrUpdate("Hello", () => Console.WriteLine($"RecurringJob => {DateTime.Now}"), Cron.Minutely);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
