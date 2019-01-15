using JuiceTelegramBot.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.Services
{
    public interface IOrderService
    {
        IList<Order> GetOrderList();
        void AddOrder(string name, DateTime dateTime);
        void ClearList();
        bool IsInOrders(string answer);
    }
}
