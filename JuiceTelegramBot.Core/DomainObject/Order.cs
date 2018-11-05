using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.DomainObject
{
    public class Order
    {
        public DateTime OrderDateTime { get; set; }
        public Juice Juice { get; set; }
    }   
}
