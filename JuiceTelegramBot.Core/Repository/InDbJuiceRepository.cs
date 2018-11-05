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
        public void AddJuice(Juice juice)
        {
            var juiceDb = new JuiceDb();
            juiceDb.Name = juice.Name;
            juiceDb.IsCustom = juice.IsCustom;
            _context.Juices.Add(juiceDb);
            _context.SaveChanges();
        }

        public void DeleteJuice(Juice juice)
        {
            var juiceDb = _context.Juices.FirstOrDefault(e => e.Name == juice.Name);
            if (juice == null)
            {
                throw new Exception("Juice not found");
            }
            _context.Juices.Remove(juiceDb);
            _context.SaveChanges();
        }

        public IList<Juice> GetJuiceList()
        {


            return _context.Juices.Select(j => new DomainObject.Juice
            {

                Name = j.Name,
                IsCustom = j.IsCustom
            }
                ).ToList();
        }
    }
}
