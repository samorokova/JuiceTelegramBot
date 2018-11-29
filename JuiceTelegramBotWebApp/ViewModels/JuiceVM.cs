using JuiceTelegramBot.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuiceTelegramBotWebApp.ViewModels
{
    public class JuiceVM:Juice
    {
        public bool IsChecked { get; set; }
        public JuiceVM()
        {
        }
        public JuiceVM(Juice juice)
        {
            this.Approved = juice.Approved;
            this.IsCustom = juice.IsCustom;
            this.JuiceDateTime = juice.JuiceDateTime;
            this.Name = juice.Name;
            this.UserName = juice.UserName;
        }
    }
}
