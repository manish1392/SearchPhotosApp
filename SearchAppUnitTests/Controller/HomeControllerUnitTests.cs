using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SearchPhotosApp.BLL;
using SearchPhotosApp.Controllers;
using SearchPhotosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SearchPhotosAppUnitTests.Controller
{
    class HomeControllerUnitTests
    {
        private readonly Mock<IImageBusinessLayer> _imageBusinessLayer;
        private readonly ILogger<HomeController> _logger;
        public HomeControllerUnitTests()
        {
            _imageBusinessLayer = new Mock<IImageBusinessLayer>();
        }
        
        [Test]
        public async Task Index_ReturnsAViewResult_WithAListOfImagesFeed()
        {
            var mockRepo = new Mock<IImageBusinessLayer>();
            _imageBusinessLayer.Setup(repo => repo.GetFeed(null))
                .ReturnsAsync(It.IsAny<FeedModel>());
            var controller = new HomeController(_imageBusinessLayer.Object, _logger);

            var result = await controller.Index();

            Assert.IsNotNull(((ViewResult)result).ViewData);
        }
    }
}


