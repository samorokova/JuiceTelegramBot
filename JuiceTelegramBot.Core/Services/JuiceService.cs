﻿using System;
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
        public void AddJuice(Juice juice)
        {
            juiceRepository.AddJuice(juice);
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
            for (int i = 0; i < juices.Count; i++)
            {
                if (answer == "/" + juices[i].Name.ToLower())
                {
                    return true;
                }

            }
            return false;
        }
    }
}
