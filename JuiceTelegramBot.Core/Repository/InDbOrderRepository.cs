using System;
using System.Collections.Generic;
using System.Text;
using JuiceTelegramBot.Core.Model;
using System.Linq;

namespace JuiceTelegramBot.Core.Repository
{
    public class InDbOrderRepository : IOrderRepository
    {
        private readonly ApiContext _context;

        public InDbOrderRepository(ApiContext context)
        {
            _context = context;
        }

        public void AddOrder(string name, DateTime orderDate)
        {
            var order = new Order();
            order.Name = name;
            order.OrderDateTime = orderDate;
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void ClearList()
        {
            var orders = _context.Orders.ToList();
            _context.Orders.RemoveRange(orders);
            _context.SaveChanges();
        }

        public IList<Order> GetOrderList()
        {
            return _context.Orders.ToList();
        }
    }
}
