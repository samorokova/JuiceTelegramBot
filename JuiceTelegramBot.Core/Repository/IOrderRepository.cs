using JuiceTelegramBot.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.Repository
{
    public interface IOrderRepository
    {
        IList<Order> GetOrderList();
        void AddOrder(string name, DateTime orderDate);
        void ClearList();
    }
}
