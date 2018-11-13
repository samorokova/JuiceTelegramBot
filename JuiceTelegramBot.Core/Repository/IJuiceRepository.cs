using JuiceTelegramBot.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.Repository
{
    public interface IJuiceRepository
    {
        IList<Juice> GetJuiceList();
        void AddJuice(string answer, bool isCustom, bool approved, DateTime juiceDateTime, string username);
        void DeleteJuice(Juice juice);
    }
}
