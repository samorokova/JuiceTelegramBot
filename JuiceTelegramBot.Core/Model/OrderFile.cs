using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.Model
{
    public class OrderFile
    {
        public string Name { get; set; }
        public DateTime OrderDateTime { get; set; }

        public string Serialize()
        {
            return Name + "," + OrderDateTime.ToFileTimeUtc();
        }

        public static OrderFile DeSerialize(string s)
        {
            var parts = s.Split(new[] { ',' });
            var o = new OrderFile();
            o.Name = parts[0];
            o.OrderDateTime = DateTime.FromFileTimeUtc(long.Parse(parts[1]));
            return o;
        }
    }

}
