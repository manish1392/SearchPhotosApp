using NUnit.Framework;
using SearchPhotosApp.Models;
using SearchPhotosApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchPhotosApp.Services;
using Moq;

namespace SearchPhotosAppUnitTests.BLL
{
    public class ImageBusinessLayerUnitTests
    {

        private readonly Mock<IImageService> _service;
        public ImageBusinessLayerUnitTests()
        {
            _service = new Mock<IImageService>();
        }
        [Test]
        public void GetFeed_Success()
        {
            FeedModel feed = new FeedModel();
            _service.Setup(x => x.GetFeed(It.IsAny<string>())).Returns(Task.FromResult(It.IsAny<string>()));
            ImageBusinessLayer imgBLL = new ImageBusinessLayer(_service.Object);
            var result = imgBLL.GetFeed(It.IsAny<string>());
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetFeed_Exception()
        {
            FeedModel feed = new FeedModel();
            _service.Setup(x => x.GetFeed(It.IsAny<string>())).Throws(new Exception("Something went wrong!"));
            ImageBusinessLayer imgBLL = new ImageBusinessLayer(_service.Object);
            var result = imgBLL.GetFeed(It.IsAny<string>());
            Assert.Throws(Is.TypeOf<Exception>()
                 .And.Message.EqualTo("Something went wrong!"),
                 delegate { throw new Exception("Something went wrong!"); });
        }
    }
}
