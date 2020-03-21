using System;
using Xunit;
using CashFlow.Core.Models;
using CashFlow.ServiceInterfaces;
using CashFlow.UserService;

namespace CachFlow.UTests
{
    public class UTUserService
    {
        IUserService _userService;
        User _baseUser = new User()
        {
            ID = 1,
            Login = "baseuser",
            CreateDate = new DateTime(2017, 7, 31)
        };
        public UTUserService() => _userService = new UserService();

        [Fact()]
        public void Test1() => Assert.Equal("Valor inválido", Assert.Throws<ArgumentException>(() => _userService.Check(_baseUser)).Message);

        [Fact]
        public void Test2() => Assert.True(true);
    }
}
