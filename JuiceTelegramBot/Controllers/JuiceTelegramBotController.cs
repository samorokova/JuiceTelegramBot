using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuiceTelegramBot.Core.Repository;
using JuiceTelegramBot.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace JuiceTelegramBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramBotController : ControllerBase
    {
        private readonly ITelegramBotClient telegramBot;
        private readonly IOrderService orderService;
        private readonly IJuiceService juiceService;

        private IConfiguration Configuration { get; }

        public TelegramBotController(ITelegramBotClient botClient, IConfiguration configuration, IOrderService orderService, IJuiceService juiceService)
        {

            this.telegramBot = botClient;
            Configuration = configuration;
            this.orderService = orderService;
            this.juiceService = juiceService;
        }
        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] Update update)
        {
            try
            {
                if (update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                {
                    string answer = update.Message.Text.ToLower();
                    switch (answer)
                    {
                        case "/start":
                        case "/hi":
                        case "hi":
                            await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Hi, " + update.Message.Chat.FirstName + "! Choice the juice you want: \n We have:\n" + string.Join("\n", juiceService.GetJuiceList().Select(juice => "/" + juice)));
                            if (update.Message.Chat.Id == Configuration.GetValue<int>("JuiceTelegramBotAdminId"))
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "For manage the Order List, please choice the command: \n /vieworders to show the Order List \n /killthemall to clear the Order List");
                            }
                            break;
                        case "/vieworders":
                            if (update.Message.Chat.Id == Configuration.GetValue<int>("JuiceTelegramBotAdminId"))
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Ordered Juice: \n" + string.Join("\n", orderService.GetOrderList().Select(item => item.Juice.Name + " " + item.OrderDateTime.ToShortDateString())));
                            }
                            else
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Sorry, " + update.Message.Chat.FirstName + ", but you haven't permissions for this operation :-(");
                            }

                            break;
                        case "/killthemall":
                            if (update.Message.Chat.Id == Configuration.GetValue<int>("JuiceTelegramBotAdminId"))
                            {
                                orderService.ClearList();
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "All orders are cleared!");
                            }
                            else
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Sorry, " + update.Message.Chat.FirstName + ", but you haven't permissions for this operation :-(");
                            }

                            break;
                        default:
                            if (juiceService.IsInJuices(answer))
                            {
                                if (juiceService.IsInJuices(answer))
                                {
                                    await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, answer + " is already ordered.");
                                }
                                else
                                {
                                    orderService.AddOrder(answer, DateTime.Now);
                                    await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Thank you for your order!");
                                }
                            }
                            else
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Sorry, can't help with this. Try to say /Hi");
                            }
                            break;
                    }


                }
                else
                {
                    await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Sorry, I can understand you. Try to say /Hi");
                }

            }
            catch (Exception ex)
            {

            }

        }
    }
}