using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.DomainObject
{
    public class Juice
    {
        public string Name { get; set; }
        public bool IsCustom { get; set; }
        public bool Approved { get; set; }
        public DateTime JuiceDateTime { get; set; }
        public string UserName { get; set; }
    }
}
