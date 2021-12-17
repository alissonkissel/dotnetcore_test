using NUnit.Framework;
using App.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Test
{
    public class Tests
    {
        private IApplication _application;
        private IRepository _repository;

        public Tests()
        {
            _repository = new App.Infra.Data.Repository();
            _application = new Application.Application(_repository);
        }

        [SetUp]
        public void Setup()
        {
            Helper.ClearTable();
            Helper.InitTable();
        }

        [Test]
        public async Task Test_GetAll_OK()
        {
            const int AppCount = 4;
            var listApp = await _application.GetAll();

            Assert.AreEqual(AppCount, listApp.Count);
        }

        [Test]
        [TestCase("www.google.com")]
        [TestCase("www.facebook.com")]
        [TestCase("www.instagram.com")]
        [TestCase("www.twitch.tv")]
        public async Task Test_GetID_OK(string url)
        {
            var app = _application.GetAll().Result.Where(a => a.Url == url).FirstOrDefault().Application; //melhorar
            var appID = await _application.GetID(app);

            Assert.AreEqual(app, appID.Application);
        }

        [Test]
        [TestCaseSource("PostInit")]
        public async Task Test_Post_OK(Domain.Entities.App app)
        {
            var value = await _application.Post(app);

            Assert.IsTrue(value);
        }


        [Test]
        [TestCase("www.twitch.tv")]
        [TestCase("www.google.com")]
        [TestCase("www.facebook.com")]
        [TestCase("www.instagram.com")]
        public async Task Test_Delete_OK(string url)
        {
            var app = _application.GetAll().Result.Where(a => a.Url == url).FirstOrDefault().Application; //melhorar

            await _application.Delete(app);
            var exist = await _application.GetID(app);

            Assert.IsNull(exist);
        }

        private static IEnumerable<Domain.Entities.App> PostInit
        {
            get
            {
                yield return new Domain.Entities.App() { Url = "www.google.com", PathLocal = @"c:\programas\google", DebuggingMode = false };
                yield return new Domain.Entities.App() { Url = "www.facebook.com", PathLocal = @"c:\programas\facebook", DebuggingMode = true };
                yield return new Domain.Entities.App() { Url = "www.instagram.com", PathLocal = @"c:\programas\instagram", DebuggingMode = true };
                yield return new Domain.Entities.App() { Url = "www.instagram.com", PathLocal = @"c:\programas\twitch", DebuggingMode = false };
            }
        }
    }
}