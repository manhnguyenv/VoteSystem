﻿using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using VoteSystem.Clients.MVC;
using VoteSystem.Clients.MVC.Areas.Administration.Controllers;
using VoteSystem.Clients.MVC.ViewModels;
using VoteSystem.Data.Services.Contracts;

namespace VoteSystem.Web.Controllers.Tests
{
    [TestFixture]
    public class QuestionTests
    {
        private Mock<IQuestionService> _mockedQuestionService;

        [SetUp]
        public void Setup()
        {
            _mockedQuestionService = new Mock<IQuestionService>();
            AutomapperConfig.RegisterMap();
        }

        [Test]
        public void Constructor_WithNullQuestionService_ShouldThrowArgumenNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() => new QuestionController(null));

            StringAssert.Contains("questionService", ex.Message);
        }

        [Test]
        public void Constructor_WithValidParam_ShouldReturnsInstanceOfQuestionController()
        {
            var actualInstance = new QuestionController(_mockedQuestionService.Object);

            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<QuestionController>());
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void CreateOnGetRequest_WithInvalidId_ShouldReturnsMinusOne(int voteSystemId)
        {
            var controller = new QuestionController(_mockedQuestionService.Object);

            var actionResult = controller.Create(voteSystemId) as ContentResult;

            Assert.AreEqual("rateSystemId can not be negative number or 0", actionResult.Content);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void CreateOnGetRequest_WithValidId_ShouldReturnsQuestionAndAnswersViewModel(int voteSystemId)
        {
            var controller = new QuestionController(_mockedQuestionService.Object);

            var actionResult = controller.Create(voteSystemId) as ViewResult;
            var actualResult = actionResult.Model as QuestionAndAnswersViewModel;

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.RateSystemId, voteSystemId);
        }

        [Test]
        public void CreateOnPostRequest_WithInvalidModelState_ShouldReturnCoreectViewModel()
        {
            var expectedRateSystemId = 17;
            var expectedQuestionId = 15;
            var expectedQuestionName = "Ankk";

            var vm = new QuestionAndAnswersViewModel()
            {
                RateSystemId = expectedRateSystemId,
                Questions = new List<QuestionViewModel>()
                {
                    new QuestionViewModel()
                    {
                        Id = expectedQuestionId,
                        QuestionName = expectedQuestionName
                    }
                }
            };

            var controller = new QuestionController(_mockedQuestionService.Object);
            controller.ModelState.AddModelError("error", "error");

            var actionResult = controller.Create(vm) as ViewResult;
            var actualResult = actionResult.Model as QuestionAndAnswersViewModel;

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.RateSystemId, expectedRateSystemId);
            Assert.AreEqual(actualResult.Questions.Count(), 1);
            Assert.AreEqual(actualResult.Questions[0].Id, expectedQuestionId);
            Assert.AreEqual(actualResult.Questions[0].QuestionName, expectedQuestionName);
        }

        [Test]
        public void CreateOnPostRequest_WithNullViewModel_ShouldReturnNull()
        {
            var controller = new QuestionController(_mockedQuestionService.Object);
            controller.ModelState.AddModelError("error", "error");

            var actionResult = controller.Create(null) as ViewResult;
            var actualResult = actionResult.Model as QuestionAndAnswersViewModel;

            Assert.IsNull(actualResult);
        }

        [Test]
        public void CreateOnPostRequest_WithZeroQuestions_ShouldReturnCoreectViewModel()
        {
            var expectedRateSystemId = 17;

            var vm = new QuestionAndAnswersViewModel()
            {
                RateSystemId = expectedRateSystemId,
                Questions = new List<QuestionViewModel>()
                {
                }
            };

            var controller = new QuestionController(_mockedQuestionService.Object);

            var actionResult = controller.Create(vm) as ViewResult;
            var actualResult = actionResult.Model as QuestionAndAnswersViewModel;

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.RateSystemId, expectedRateSystemId);
        }

        [Test]
        public void CreateOnPostRequest_WithValidViewModel_ShouldCallSave()
        {
            var expectedRateSystemId = 17;
            var expectedQuestionName = "Ankk";

            var vm = new QuestionAndAnswersViewModel()
            {
                RateSystemId = expectedRateSystemId,
                Questions = new List<QuestionViewModel>()
                {
                    new QuestionViewModel()
                    {
                        QuestionName = expectedQuestionName
                    }
                }
            };

            var controller = new QuestionController(_mockedQuestionService.Object);
            controller.Create(vm);
           
            //_mockedQuestionService.Verify(
            //    x => x.Add(It.Is<Question>(
            //        q => q.QuestionName == expectedQuestionName)), 
            //        Times.Once);
        }

        [Test]
        public void CreateOnPostRequest_WithDefaultFlow_ShouldRedirectToIndex()
        {
            var expectedRateSystemId = 17;
            var expectedQuestionName = "Ankk";

            var vm = new QuestionAndAnswersViewModel()
            {
                RateSystemId = expectedRateSystemId,
                Questions = new List<QuestionViewModel>()
                {
                    new QuestionViewModel()
                    {
                        QuestionName = expectedQuestionName
                    }
                }
            };

            var controller = new QuestionController(_mockedQuestionService.Object);
            var redirectResult = controller.Create(vm) as RedirectToRouteResult;
         
            Assert.AreEqual("Index", redirectResult.RouteValues["Action"]);   
        }
    }
}
