using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JuiceTelegramBot.Core.Model;

namespace JuiceTelegramBot.Core.Repository
{
    public class InFileOrderRepository : IOrderRepository
    {
        private IList<Order> orderList = new List<Order>();
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
                var order = Order.DeSerialize(item);
                orderList.Add(order);
            }
        }

        public void AddOrder(string name, DateTime orderDate)
        {
            orderList.Add(new Order()
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
            return this.orderList;
        }

        private static Object syncroot = new object();
    }
}
