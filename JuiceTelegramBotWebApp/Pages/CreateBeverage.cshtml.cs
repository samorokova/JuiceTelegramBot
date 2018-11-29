using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuiceTelegramBot.Core.Model;
using JuiceTelegramBot.Core.Services;
using JuiceTelegramBotWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace JuiceTelegramBotWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private ILogger<CreateModel> Log;
        private readonly IJuiceService juiceService;
        public CreateModel( ILogger<CreateModel> logger, IJuiceService juiceService)
        {
            Log = logger;
            this.juiceService = juiceService ?? throw new ArgumentNullException(nameof(juiceService));
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public JuiceVM Juice { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (juiceService.IsInJuices(Juice.Name))
            {
                Message = $"Bevarage {Juice.Name} already exist!";
                return Page();
            }
            juiceService.AddJuice(Juice.Name, false, true, DateTime.Now, "Admin");
            juiceService.IsInJuices(Juice.Name);
            return RedirectToPage("/Index");
        }
    }
}