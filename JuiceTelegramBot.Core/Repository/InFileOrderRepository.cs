using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JuiceTelegramBot.Core.DomainObject;
using JuiceTelegramBot.Core.Model;

namespace JuiceTelegramBot.Core.Repository
{
    public class InFileOrderRepository : IOrderRepository
    {
        private IList<OrderFile> orderList = new List<OrderFile>();
        public const string Path = @"Orders.txt";
        private readonly string[] lines = null;
        public InFileOrderRepository()
        {
            lock (syncroot)
            {
                lines = File.ReadAllLines(Path);
            }

            foreach (var item in lines)
            {
                var order = OrderFile.DeSerialize(item);
                orderList.Add(order);
            }
        }

        public void AddOrder(string name, DateTime orderDate)
        {
            orderList.Add(new OrderFile()
            {
                Name = name,
                OrderDateTime = orderDate
            });
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in orderList)
            {
                var str = item.Serialize();
                stringBuilder.AppendLine(str);
            }
            lock (syncroot)
            {
                File.WriteAllText(Path, stringBuilder.ToString());
            }


        }

        public void ClearList()
        {
            orderList.Clear();
            lock (syncroot)
            {
                File.CreateText(Path);
            }

        }

        public IList<Order> GetOrderList()
        {
            return orderList.Select(e => new Order
            {
                OrderDateTime = e.OrderDateTime,
                Juice = new Juice
                {
                    Name = e.Name,
                    IsCustom = false
                }
            }).ToList();
        }

        private static Object syncroot = new object();
    }
}
