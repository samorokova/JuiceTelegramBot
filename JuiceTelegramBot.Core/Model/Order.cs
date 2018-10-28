using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JuiceTelegramBot.Core.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime OrderDateTime { get; set; }

        public string Serialize()
        {
            return Name + "," + OrderDateTime.ToFileTimeUtc();
        }

        public static Order DeSerialize(string s)
        {
            var parts = s.Split(new[] { ',' });
            var o = new Order
            {
                Name = parts[0],
                OrderDateTime = DateTime.FromFileTimeUtc(long.Parse(parts[1]))
            };
            return o;
        }
    }
}
