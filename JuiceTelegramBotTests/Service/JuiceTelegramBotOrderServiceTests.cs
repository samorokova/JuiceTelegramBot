using AutoFixture;
using AutoFixture.AutoMoq;
using JuiceTelegramBot.Core.DomainObject;
using JuiceTelegramBot.Core.Repository;
using JuiceTelegramBot.Core.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBotTests.Service
{
    class JuiceTelegramBotOrderServiceTests
    {
        private IFixture fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
        }

        [Test]
        public void AddOrder()
        {
            var order = fixture.Create<Order>();

            var orderRepositoryMock = fixture.Freeze<Mock<IOrderRepository>>();
            orderRepositoryMock.Setup(r => r.AddOrder(order.Juice.Name,order.OrderDateTime));

            var serviceUnderTest = fixture.Create<OrderService>();
            serviceUnderTest.AddOrder(order.Juice.Name, order.OrderDateTime);

            orderRepositoryMock.Verify(r => r.AddOrder(order.Juice.Name, order.OrderDateTime));
        }

        [Test]
        public void ClearList()
        {
            var order = fixture.Create<Order>();

            var orderRepositoryMock = fixture.Freeze<Mock<IOrderRepository>>();
            orderRepositoryMock.Setup(r => r.ClearList());

            var serviceUnderTest = fixture.Create<OrderService>();
            serviceUnderTest.AddOrder(order.Juice.Name, order.OrderDateTime);
            serviceUnderTest.ClearList();

            orderRepositoryMock.Verify(r => r.ClearList());
        }

        [Test]
        public void GetOrderList()
        {
            //List<Order>
        }
    }
}
