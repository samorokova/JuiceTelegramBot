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
            Log = logger ?? throw new ArgumentNullException(nameof(logger));
            this.juiceService = juiceService ?? throw new ArgumentNullException(nameof(juiceService));
        }

        [BindProperty]
        public IList<JuiceVM> Juices { get; set; }

        public IList<OrderDb> Orders { get; private set; }

        public void OnGetAsync()
        {
            Juices =  juiceService.GetJuiceList().Select(j => new JuiceVM(j)).ToList();
        }

        // async method is required by convention
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IActionResult> OnPostApprovedAsync()
        {
            Juices.AsParallel()
                .Where(juice => juice.IsChecked)
                .ForAll(juice => juiceService.ApproveJuice(juice.Name));
            
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string name)
        {
            Juices
                .Where(juice => juice.IsChecked)
                .ToList()
                .ForEach(juice => juiceService.DeleteByName(juice.Name));

            return RedirectToPage();
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
