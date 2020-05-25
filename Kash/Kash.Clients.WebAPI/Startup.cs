using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kash.Clients.WebAPI
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
            services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                // options.SuppressConsumesConstraintForFormFileParameters = true;
                // options.SuppressInferBindingSourcesForParameters = true;
                // options.SuppressModelStateInvalidFilter = true;
                // options.SuppressMapClientErrors = true;
                // options.ClientErrorMapping[404].Link = "https://httpstatuses.com/404";
                // options.InvalidModelStateResponseFactory = context =>
                // {
                //     var result = new BadRequestObjectResult(context.ModelState);
                //     // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
                //     result.ContentTypes.Add(MediaTypeNames.Application.Json);
                //     result.ContentTypes.Add(MediaTypeNames.Application.Xml);
                //     return result;
                // };
                options.InvalidModelStateResponseFactory = context =>
                {
                    var traceId = Activity.Current?.Id ?? context.HttpContext?.TraceIdentifier;
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        // Detail = ApiConstants.Messages.ModelStateValidation
                    };
                    // Find out which status code to use
                    var actionExecutingContext = context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    // If there are modelstate errors & all keys were correctly found/parsed we're dealing with validation errors
                    if (context.ModelState.ErrorCount > 0 && actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count)
                    {
                        problemDetails.Type = $"https://httpstatuses.com/422";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Extensions["traceId"] = traceId;
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    }

                    // If one of the keys wasn't correctly found / couldn't be parsed we're dealing with null/unparsable input
                    problemDetails.Type = $"https://httpstatuses.com/400";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Extensions["traceId"] = traceId;
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            })
            ;
            services.AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
