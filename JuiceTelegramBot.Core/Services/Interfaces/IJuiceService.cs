using JuiceTelegramBot.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.Services
{
    public interface IJuiceService
    {
        IList<Juice> GetJuiceList();
        void AddJuice(string answer, bool isCustom, bool approved, DateTime juiceDateTime, string username);
        void DeleteJuice(Juice juice);
        bool IsInJuices(string answer);
        void DeleteByName(string name);
        void ApproveJuice(string name);
    }
}
