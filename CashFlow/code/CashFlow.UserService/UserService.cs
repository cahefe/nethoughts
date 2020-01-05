using System;
using CashFlow.ServiceInterfaces;
using CashFlow.Core.Models;

namespace CashFlow.UserService
{
    public class UserService : IUserService
    {
        public void Check(User user)
        {
            if (user.ID == 1)
                throw new ArgumentException("Valor inválido");
        }
    }
}