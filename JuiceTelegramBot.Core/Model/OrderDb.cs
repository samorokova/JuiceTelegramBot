using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JuiceTelegramBot.Core.Model
{
    public class OrderDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDateTime { get; set; }

        [ForeignKey("JuiceId")]
        public JuiceDb Juice { get; set; }

        
    }
}
