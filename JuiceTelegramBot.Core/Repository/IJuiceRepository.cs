using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.Repository
{
    public interface IJuiceRepository
    {
        IList<string> GetJuiceList();
        void AddJuice(string name);
        void DeleteJuice(string name);
    }
}
