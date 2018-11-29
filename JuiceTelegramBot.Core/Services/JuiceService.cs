using System;
using System.Collections.Generic;
using System.Text;
using JuiceTelegramBot.Core.DomainObject;
using JuiceTelegramBot.Core.Repository;

namespace JuiceTelegramBot.Core.Services
{
    public class JuiceService : IJuiceService
    {
        private readonly IJuiceRepository juiceRepository;

        public JuiceService(IJuiceRepository juiceRepository)
        {
            this.juiceRepository = juiceRepository ?? throw new ArgumentNullException(nameof(juiceRepository));
        }
        public void AddJuice(string answer, bool isCustom, bool approved, DateTime juiceDateTime, string username)
        {
            juiceRepository.AddJuice(answer, isCustom, approved, juiceDateTime, username);
        }

        public void DeleteJuice(Juice juice)
        {
            juiceRepository.DeleteJuice(juice);
        }

        public IList<Juice> GetJuiceList()
        {
            return juiceRepository.GetJuiceList();
        }

        public bool IsInJuices(string answer)
        {
            IList<Juice> juices = juiceRepository.GetJuiceList();
            if (answer[0] == '/')
            {
                for (int i = 0; i < juices.Count; i++)
                {
                    if (answer.ToLower() == "/" + juices[i].Name.ToLower())
                    {
                        return true;
                    }

                }
            }
            else
            {
                for (int i = 0; i < juices.Count; i++)
                {
                    if (answer.ToLower() == juices[i].Name.ToLower())
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public void DeleteById(string name)
        {
            juiceRepository.DeleteJuice(name);
        }
    }
}
