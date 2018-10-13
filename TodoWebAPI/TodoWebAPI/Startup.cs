using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoWebAPI.Core.Interfaces;
using TodoWebAPI.Infra.Repo.EFTodo;
using TodoWebAPI.Service;
using TodoWebAPI.ServicesInterfaces;

namespace TodoWebAPI
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
            var RepoEF = new TodoWebAPIContext(Configuration);
            services
            .AddLogging()
            .AddDbContext<TodoWebAPIContext>()
            .AddSingleton<ITodoService, TodoService>()
            .AddSingleton<IPersonService, PersonService>()
            .AddSingleton<ITodoItemRepo, TodoWebAPIContext>()
            //.AddSingleton<IPersonRepo, PersonRepo>()
            .AddScoped<IPersonRepo, PersonRepo>()
            //.AddSingleton<IPersonRepo, TodoWebAPIContext>()
            //.AddScoped<ITodoItemRepo, TodoWebAPIRepoEF>()
            //.AddScoped<IPersonRepo, TodoWebAPIRepoEF>()
            .BuildServiceProvider();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
