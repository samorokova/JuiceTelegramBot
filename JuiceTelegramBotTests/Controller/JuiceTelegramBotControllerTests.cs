using AutoFixture;
using AutoFixture.AutoMoq;
using JuiceTelegramBot.Core.DomainObject;
using JuiceTelegramBot.Core.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using JuiceTelegramBot.Controllers;
using Telegram.Bot.Types;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace JuiceTelegramBotTests
{
    public class JuiceTelegramBotControllerTests
    {
        private IFixture fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
    .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public void JuiceTelegramBotController_CallsJuiceRepository_WhenGetsData()
        {
            var juiceServiseMock = fixture.Freeze<Mock<IJuiceService>>();
            juiceServiseMock.Setup(r => r.AddJuice(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<DateTime>(), It.IsAny<string>()));
            juiceServiseMock.Setup(e => e.IsInJuices("/orange")).Returns(true);

            var telegramBotMock = fixture.Freeze<Mock<ITelegramBotClient>>();
            telegramBotMock.Setup(t => t.SendTextMessageAsync(It.IsAny<ChatId>(), It.IsAny<string>(), It.IsAny<ParseMode>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(), It.IsAny<System.Threading.CancellationToken>()));

            var juice = fixture.Create<Juice>();
            juice.Name = "/orange";

            var update = new Update();
            update.Message = new Message();
            update.Message.Text = juice.Name;

            var controller = fixture.Create<TelegramBotController>();

            var result = controller.Post(update);

            juiceServiseMock.Verify(r => r.AddJuice(juice.Name, juice.IsCustom, juice.Approved, juice.JuiceDateTime, juice.UserName));

        }
    }

}
