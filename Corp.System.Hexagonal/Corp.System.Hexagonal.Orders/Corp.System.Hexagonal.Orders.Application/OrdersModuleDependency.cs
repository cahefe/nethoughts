using AutoMapper;
using Corp.System.Hexagonal.Orders.Adapters.Repository.EF;
using Corp.System.Hexagonal.Orders.Application.Mappings;
using Corp.System.Hexagonal.Orders.Application.ServiceLocators;
using Corp.System.Hexagonal.Orders.Application.UseCases;
using Corp.System.Hexagonal.Orders.Application.Validators;
using Corp.System.Hexagonal.Orders.Domain.Model;
using Corp.System.Hexagonal.Orders.Domain.Ports.Incoming;
using Corp.System.Hexagonal.Orders.Domain.Ports.Outgoing;
using Corp.System.Hexagonal.Shared.Domain.Interfaces;
using Corp.System.Hexagonal.Shared.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Corp.System.Hexagonal.Orders.Application
{
    public static class OrdersModuleDependency
    {
        public static void AddOrdersModuleDependency(this IServiceCollection services)
        {
            //  Repositories
            services.AddDbContext<OrdersContext>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();

            //  Service locator pattern
            services.AddSingleton<IServiceLocator, ServiceLocator>();

            //  Validators:
            services.AddSingleton<IServiceLocator<AbstractValidator<OrderBaseInfo>>, RegisterInputServiceLocator>();
            services.AddScoped<RegisterInputValidator_Buy>();

            //  IRegister Implementations...
            // services.AddSingleton<IServiceLocator<IRegister>, RegisterServiceLocator>();
            // services.AddScoped<RegisterUseCase_Buy>();
            // services.AddScoped<RegisterUseCase_Sell>();
            services.AddScoped<IRegister, RegisterUseCase_Buy>();
            services.AddScoped<IRegister, RegisterUseCase_Sell>();

            //  Mapping configuration...
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile<OrdersProfile>());
            mapConfig.AssertConfigurationIsValid();
            services.AddSingleton<IMapper>(mapConfig.CreateMapper());
        }
    }
}
