using JuiceTelegramBot.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.Services
{
    public interface IJuiceService
    {
        IList<Juice> GetJuiceList();
        void AddJuice(Juice juice);
        void DeleteJuice(Juice juice);
        bool IsInJuices(string answer);
    }
}
