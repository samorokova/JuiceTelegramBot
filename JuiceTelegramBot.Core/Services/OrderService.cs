using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JuiceTelegramBot.Core.DomainObject;
using JuiceTelegramBot.Core.Repository;

namespace JuiceTelegramBot.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }
        public void AddOrder(string name, DateTime dateTime)
        {
            orderRepository.AddOrder(name, dateTime);
        }

        public void ClearList()
        {
            orderRepository.ClearList();
        }

        public IList<Order> GetOrderList()
        {
            return orderRepository.GetOrderList();
        }

        public bool IsInOrders(string answer)
        {
            var orders = orderRepository.GetOrderList();
            var order = orders.FirstOrDefault(item => item.Juice.Name == answer);
            return order != null;
        }
    }
}
