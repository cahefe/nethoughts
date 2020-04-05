using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Interfaces;
using TodoApi.Infrastructure;
using TodoApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace TodoApi
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
            services.AddSingleton<IProducer, PushStreamFlow>();
            services.AddSingleton<IConsumer, PushStreamFlow>();
            services.AddRazorPages();
            //  Utilizando um contexto exclusivamente em memória
            services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase(databaseName: "TestCustomerContext"));
            //services.AddDbContext<CustomerContext>();
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
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            //app.UseMvc(routes => routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}"));
            using (var serviceScope = app.ApplicationServices.CreateScope())
                PopulateCustomerAsync(serviceScope.ServiceProvider.GetService<CustomerContext>());
        }

        public async void PopulateCustomerAsync(CustomerContext context)
        {
            var customer1 = new Customer()
            {
                ID = 1,
                FirstName = "Jhonny",
                LastName = "Englisn",
                Address = "Avenue 1",
                Invoices = new List<Invoice>() {
                    new Invoice() {
                        ID = 10,
                        Items = new List<InvoiceItem>() {
                            new InvoiceItem() {
                                ID = 100,
                                Code= "Code100"
                                },
                            new InvoiceItem() {
                                ID = 101,
                                Code = "Code101"
                                }
                            }
                        },
                    new Invoice() {
                        ID = 11,
                        Items = new List<InvoiceItem>() {
                            new InvoiceItem() {
                                ID = 102,
                                Code= "Code102"
                                },
                            new InvoiceItem() {
                                ID = 103,
                                Code = "Code103"
                                }
                            }
                        }
                    }
            };
            var client1 = new Client()
            {
                ID = 1,
                Name = "First Client",
                BornDate = new DateTime(2002, 10, 6),
                ClientConditions = EnumClientConditions.Children | EnumClientConditions.Employee | EnumClientConditions.Marryied,
                Documents = new List<Document> {
                    new Document() {
                        ID = 10,
                        Type = EnumDocumentType.CPF,
                        Number = 123456789,
                        ExpirationDate = DateTime.Now.AddYears(10),
                    },
                    new Document() {
                        ID = 11,
                        Type = EnumDocumentType.RG,
                        Number = 987654,
                        ExpirationDate = DateTime.Now.AddYears(5),
                    },
                }
            };
            var client2 = new Client()
            {
                ID = 2,
                Name = "Second Client",
                BornDate = new DateTime(1970, 04, 25),
                ClientConditions = EnumClientConditions.Relatives |  EnumClientConditions.Children,
                Documents = new List<Document> {
                    new Document() {
                        ID = 12,
                        Type = EnumDocumentType.CPF,
                        Number = 357159754,
                        ExpirationDate = DateTime.Now.AddYears(7),
                    },
                }
            };
            await context.AddRangeAsync(customer1, client1, client2);
            context.SaveChanges();
        }
    }
}
