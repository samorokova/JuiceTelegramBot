using JuiceTelegramBot.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JuiceTelegramBot.Core.DomainObject;

namespace JuiceTelegramBot.Core.Repository
{
    public class InDbJuiceRepository : IJuiceRepository
    {
        private readonly ApiContext _context;

        public InDbJuiceRepository(ApiContext context)
        {
            _context = context;  

        }
        public void AddJuice(string answer, bool isCustom, bool approved, DateTime juiceDateTime, string username)
        {
            var juiceDb = new JuiceDb();
            juiceDb.Name = answer;
            juiceDb.IsCustom = isCustom;
            juiceDb.Approved = approved;
            juiceDb.JuiceDateTime = juiceDateTime;
            juiceDb.UserName = username;
            _context.Juices.Add(juiceDb);
            _context.SaveChanges();
        }

        public void DeleteJuice(Juice juice)
        {
            this.DeleteJuice(juice.Name);
        }

        public void DeleteJuice (string name)
        {
            var juiceDb = _context.Juices.FirstOrDefault(e => e.Name == name);
            if (juiceDb == null)
            {
                throw new Exception("Juice not found");
            }
            if (_context.Orders.Any(o=>o.Juice.Id==juiceDb.Id)) 
            {
                foreach (var order in _context.Orders.Where(o=>o.Juice.Id==juiceDb.Id))
                {
                    juiceDb.Orders.Remove(order);
                }
            }
            
            _context.Juices.Remove(juiceDb);
            _context.SaveChanges();
        }

        public IList<Juice> GetJuiceList()
        {


            return _context.Juices.Select(j => new Juice
            {

                Name = j.Name,
                IsCustom = j.IsCustom,
                Approved = j.Approved,
                JuiceDateTime = j.JuiceDateTime,
                UserName = j.UserName,


            }
                ).ToList();
        }
    }
}
