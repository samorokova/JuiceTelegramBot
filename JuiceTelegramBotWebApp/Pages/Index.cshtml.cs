using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuiceTelegramBot.Core.Model;
using JuiceTelegramBot.Core.Services;
using JuiceTelegramBotWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JuiceTelegramBotWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private ILogger<CreateModel> Log;
        private readonly IJuiceService juiceService;
        public IndexModel(ILogger<CreateModel> logger, IJuiceService juiceService)
        {
            Log = logger;
            this.juiceService = juiceService ?? throw new ArgumentNullException(nameof(juiceService));
        }

        [BindProperty]
        public IList<JuiceVM> Juices { get; set; }

        public IList<OrderDb> Orders { get; private set; }

        //[TempData]
        //public string Message { get; set; } 

        public void OnGetAsync()
        {
            Juices =  juiceService.GetJuiceList().Select(j => new JuiceVM(j)).ToList();
        }

        public async Task<IActionResult> OnPostApprovedAsync()
        {
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string name)
        {
            juiceService.DeleteById(name); 
            return RedirectToPage();
        }
    }
}
