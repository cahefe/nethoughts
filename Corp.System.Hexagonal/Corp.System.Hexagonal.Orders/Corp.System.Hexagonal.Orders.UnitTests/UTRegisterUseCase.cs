using System;
using System.Threading.Tasks;
using Corp.System.Hexagonal.Orders.Domain.Model;
using Corp.System.Hexagonal.Orders.Domain.Ports.Outgoing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Corp.System.Hexagonal.Orders.Application;
using Corp.System.Hexagonal.Shared.Domain.Interfaces;
using Corp.System.Hexagonal.Orders.Domain.Ports.Incoming;

namespace Corp.System.Hexagonal.Orders.UnitTests
{
    [TestClass]
    public class UTRegisterUseCase
    {
        readonly Random _rnd;
        readonly IOrdersRepository _orderRepository;
        readonly IServiceProvider _services;

        public UTRegisterUseCase()
        {
            //  Base instances
            _rnd = new Random();

            //  Arrange mocks:
            var mock = new Mock<IOrdersRepository>();
            //  1) Prepare scenarios
            mock.Setup(s => s.Create(It.IsAny<OrderInfo>())).Returns<OrderInfo>(r => Task.FromResult(new OrderInfo
            {
                IDOrder = _rnd.Next(1000, 10000),
                ClientParty = r.ClientParty,
                OrderItems = r.OrderItems?.Select(oi => new OrderItemInfo
                {
                    IDBond = oi.IDBond,
                    Quantity = oi.Quantity,
                    BondUnityPrice = oi.BondUnityPrice,
                    GrossValue = Math.Truncate(100 * oi.Quantity * oi.BondUnityPrice) / 100m,
                }),
                Marks = new MarksInfo { Register = DateTime.Now },
            }));
            //  Arrange injections
            _orderRepository = mock.Object;

            var config = Shared.Domain.Extensions.ConfigurationExtensions.InitConfiguration("appsettings.json");

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(config);
            services.AddOrdersModuleDependency();
            // services.AddOrdersModuleDependency(opt => opt.UseInMemoryDatabase(databaseName: "TestCustomerContext"));
            _services = services.BuildServiceProvider();

        }

        [TestMethod]
        public void Execute_InvalidOrderType_ThrowsArgumentException()
        {
            var registerInput = new OrderBaseInfo
            {
                OrderType = EnumOrderType.Unknown,
            };
            var registerLocator = (IServiceLocator<IRegister>)_services.GetRequiredService<IServiceLocator<IRegister>>();
            registerLocator.Should().NotBeNull();
            var register = registerLocator.GetService(registerInput.OrderType);
            register.Invoking(m => m.Execute(registerInput).Result).Should().Throw<ArgumentException>();
        }
        [TestMethod]
        public void Execute_ValidRegisterInput_CreatesRegisterOutput()
        {
            var registerInput = new OrderBaseInfo
            {
                OrderType = EnumOrderType.Buy,
                ClientParty = new ClientInfo
                {
                    IDClient = 222,
                    IDBroker = 333
                },
                OrderItems = new OrderItemBasetInfo[] {
                    new OrderItemBasetInfo {
                        IDBond = 999,
                        Quantity = 1.05m,
                        BondUnityPrice = 12504.16m
                    }
                }
            };
            // var registerLocator = _services.GetService<IServiceLocator<IRegister>>();
            // var register = registerLocator.GetService(registerInput.OrderType);
            var registerLocator = _services.GetService<IServiceLocator>();
            var register = registerLocator.GetService<IRegister>(registerInput.OrderType);
            var actual = register.Execute(registerInput).Result;
            actual.Should().NotBeNull().And.BeOfType<OrderInfo>();
        }
    }
}
