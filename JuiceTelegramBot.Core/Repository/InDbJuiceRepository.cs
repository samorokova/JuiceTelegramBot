using JuiceTelegramBot.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JuiceTelegramBot.Core.Repository
{
    public class InDbJuiceRepository : IJuiceRepository
    {
        private readonly ApiContext _context;

        public InDbJuiceRepository(ApiContext context)
        {
            _context = context;  

        }
        public void AddJuice(string name)
        {
            var juice = new Juice();
            juice.Name = name;
            _context.Juices.Add(juice);
            _context.SaveChanges();
        }

        public void DeleteJuice(string name)
        {
            var juice = _context.Juices.FirstOrDefault(e => e.Name == name);
            if (juice == null)
            {
                throw new Exception("Juice not found");
            }
            _context.Juices.Remove(juice);
            _context.SaveChanges();
        }

        public IList<string> GetJuiceList()
        {
            return _context.Juices.Select(e => e.Name).ToList();
        }
    }
}
