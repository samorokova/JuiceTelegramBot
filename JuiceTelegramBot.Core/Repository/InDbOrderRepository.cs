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
            var order = new OrderDb();
            order.Juice = _context.Juices.Single(j => j.Name == name);
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

        public IList<DomainObject.Order> GetOrderList()
        {
            return _context.Orders.Select(o => new DomainObject.Order
            {
                Juice = new DomainObject.Juice
                {
                    Name = o.Juice.Name,
                    IsCustom = o.Juice.IsCustom
                },
                OrderDateTime = o.OrderDateTime
            }).ToList();
        }
    }
}
