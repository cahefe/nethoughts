using AutoMapper;
using Corp.System.Hexagonal.Orders.Adapters.WebAPI.Dto;
using Corp.System.Hexagonal.Orders.Domain.Model;
using Corp.System.Hexagonal.Orders.Domain.Ports.Incoming;
using Corp.System.Hexagonal.Shared.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Corp.System.Hexagonal.Orders.Adapters.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        // readonly IRegister _register;
        readonly IServiceLocator<IRegister> _registerServiceLocator;
        // readonly IServiceLocator _serviceLocator;
        readonly IMapper _map;

        public OrdersController(ILogger<WeatherForecastController> logger, IServiceLocator<IRegister> registerServiceLocator, IMapper map)
        // public OrdersController(ILogger<WeatherForecastController> logger, IServiceLocator serviceLocator, IMapper map)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            // _serviceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator));
            _registerServiceLocator = registerServiceLocator ?? throw new ArgumentNullException(nameof(registerServiceLocator));
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        [HttpHead]
        [HttpGet]
        public ActionResult<OrderOutput> Get()
        {
            return Ok(new OrderOutput
            {
                IDOrder = 1,
                OrderType = Dto.EnumOrderType.Buy,
                OrderSituation = Dto.EnumOrderSituation.Registered,
            });
        }

        [HttpPost("Register")]
        // public ActionResult<OrderOutput> Post([FromBody] OrderInput orderInput)
        public async Task<IActionResult> Register([FromBody] OrderInput orderInput)
        {
            var register = _registerServiceLocator.GetService(orderInput.OrderType);
            // var register = _serviceLocator.GetService<IRegister>(orderInput.OrderType);
            var orderBaseInfo = _map.Map<OrderBaseInfo>(orderInput);
            var orderInfo = await register.Execute(orderBaseInfo);
            var orderOutput = _map.Map<OrderOutput>(orderInfo);
            return Ok(orderOutput);
        }
    }
}
