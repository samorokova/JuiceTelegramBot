using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuiceTelegramBot.Core.Repository;
using JuiceTelegramBot.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly Microsoft.Extensions.Logging.ILogger<TelegramBotController> logger;

        private IConfiguration Configuration { get; }

        public TelegramBotController(ITelegramBotClient botClient, IConfiguration configuration, IOrderService orderService, IJuiceService juiceService, ILogger<TelegramBotController> logger)
        {

            this.telegramBot = botClient ?? throw new ArgumentNullException(nameof(botClient));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.juiceService = juiceService ?? throw new ArgumentNullException(nameof(juiceService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
                    int v = Configuration.GetValue<int>("JuiceTelegramBotAdminId"); //ToDo: rename
                    switch (answer)
                    {
                        case "/start":
                        case "/hi":
                        case "hi":
                            await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Hi, " + update.Message.Chat.FirstName + "! Choice the beverage you want: \n We have:\n" + string.Join("\n", juiceService.GetJuiceList().Select(juice => "/" + juice)));
                            await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "If you don't see the beverage you want, you can add it by yourself. \n For adding a new beverage just imput the name");
                            if (update.Message.Chat.Id == v)
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "For manage the order list, please, choose the command: " +
                                    "\n /viewebeverageslist to show beverages list" +
                                    "\n /viewcustombeverage to show beverages added by users" +
                                    "\n /viewnotapproved to show beverages waiting for approving" +
                                    "\n For manage the order list, please, choose the command: " +
                                    "\n /vieworders to show the order list " +
                                    "\n /killthemall to clear the order list");
                            }
                            break;

                        case "/viewebeverageslist":
                            if (update.Message.Chat.Id == v)
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, string.Join("\n", juiceService.GetJuiceList().Select(item => string.Format("Beverage name: {0}, Is custom {1}, Approved:{2}, Date:{3}, User name:{4}", item.Name, item.IsCustom, item.Approved, item.JuiceDateTime.ToShortDateString(), item.UserName))));
                            }
                            else
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, string.Join("\n", juiceService.GetJuiceList().Select(juice => "/" + juice.Name)));
                            }
                            break;

                        case "/viewcustombeverage":
                            if (update.Message.Chat.Id == v)
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Custom beverages: \n" + string.Join("\n", juiceService.GetJuiceList().Where(j => j.IsCustom == true).Select(item => item.Name)));
                            }
                            else
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Sorry, " + update.Message.Chat.FirstName + ", but you haven't permissions for this operation :-(");
                            }
                            break;

                        case "/viewnotapproved":
                            if (update.Message.Chat.Id == v)
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Not approved beverages: \n" + string.Join("\n", juiceService.GetJuiceList().Where(j => j.Approved == false).Select(item => item.Name + $" Approve: /approve_{item.Name}, Delete: /delete_{item.Name}")));
                            }
                            else
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Sorry, " + update.Message.Chat.FirstName + ", but you haven't permissions for this operation :-(");
                            }
                            break;

                        case "/vieworders":
                            if (update.Message.Chat.Id == v)
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Ordered beverages: \n" + string.Join("\n", orderService.GetOrderList().Select(item => item.Juice.Name + " " + item.OrderDateTime.ToShortDateString())));
                            }
                            else
                            {
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Sorry, " + update.Message.Chat.FirstName + ", but you haven't permissions for this operation :-(");
                            }
                            break;

                        case "/killthemall":
                            if (update.Message.Chat.Id == v)
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
                                if (orderService.IsInOrders(answer))
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
                                juiceService.AddJuice(answer, true, false, DateTime.Now, update.Message.Chat.FirstName);
                                orderService.AddOrder(answer, DateTime.Now);
                                await telegramBot.SendTextMessageAsync(update.Message.Chat.Id, "Thank you for your order!");
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
                logger.LogError(ex, "Can't process message");
            }

        }
    }
}