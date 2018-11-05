﻿using JuiceTelegramBot.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.Repository
{
    public interface IJuiceRepository
    {
        IList<Juice> GetJuiceList();
        void AddJuice(Juice juice);
        void DeleteJuice(Juice juice);
    }
}
