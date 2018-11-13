using AutoFixture;
using AutoFixture.AutoMoq;
using JuiceTelegramBot.Core.DomainObject;
using JuiceTelegramBot.Core.Repository;
using JuiceTelegramBot.Core.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuiceTelegramBotTests.Service
{
    class JuiceTelegramBotJuiceServiceTests
    {
        private IFixture fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
        }

        [Test]
        public void GetJuiceList()
        {
            //ARRANGE - create test data and dependencies
            List<Juice> juiceList = new List<Juice>();
            var juice = fixture.Create<Juice>();
            juiceList.Add(juice);

            var juiceRepositoryMock = fixture.Freeze<Mock<IJuiceRepository>>();
            juiceRepositoryMock.Setup(r => r.GetJuiceList()).Returns(juiceList);

            var serviceUnderTest = fixture.Create<JuiceService>();

            //ACT - waht do we test
            var serviceResult = serviceUnderTest.GetJuiceList();
            //ASSERT - check the result
            Assert.IsTrue(serviceResult.SequenceEqual(juiceList));
        }

        [Test]
        public void AddJuice()
        {
            var juice = fixture.Create<Juice>();

            var juiceRepositoryMock = fixture.Freeze<Mock<IJuiceRepository>>();
            juiceRepositoryMock.Setup(r => r.AddJuice(juice.Name, juice.IsCustom, juice.Approved, juice.JuiceDateTime, juice.UserName));

            var serviceUnderTest = fixture.Create<JuiceService>();

            //act
            serviceUnderTest.AddJuice(juice.Name, juice.IsCustom, juice.Approved, juice.JuiceDateTime, juice.UserName);

            //assert
            juiceRepositoryMock.Verify(r => r.AddJuice(juice.Name, juice.IsCustom, juice.Approved, juice.JuiceDateTime, juice.UserName));
        }

        [Test]
        public void DeleteJuice()
        {
            var juice = fixture.Create<Juice>();

            var juiceRepositoryMock = fixture.Freeze<Mock<IJuiceRepository>>();
            juiceRepositoryMock.Setup(r => r.DeleteJuice(juice));

            var serviceUnderTest = fixture.Create<JuiceService>();

            serviceUnderTest.DeleteJuice(juice);

            juiceRepositoryMock.Verify(r => r.DeleteJuice(juice));
        }

        [Test]
        public void IsInJuices()
        {
            List<Juice> juiceList = new List<Juice>();
            var juice = fixture.Create<Juice>();
            juiceList.Add(juice);

            var juiceRepositoryMock = fixture.Freeze<Mock<IJuiceRepository>>();
            juiceRepositoryMock.Setup(r => r.GetJuiceList()).Returns(juiceList);

            var serviceUnderTest = fixture.Create<JuiceService>();
            var ExpectedJuiceName = "/" + juice.Name.ToLower();
            var serviceAnswer = serviceUnderTest.IsInJuices(ExpectedJuiceName);

            Assert.AreEqual(serviceAnswer, true);
        }
    }
}
