using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchPhotosApp.BLL;
using SearchPhotosApp.Models;
using SearchPhotosApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPhotosApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageBusinessLayer _imageBusinessLayer;

        public HomeController(IImageBusinessLayer imageBusinessLayer, ILogger<HomeController> logger)
        {
            _logger = logger;
            _imageBusinessLayer = imageBusinessLayer;
        }

        public async Task<IActionResult> Index()
        {
            var publicImagesFeed = await _imageBusinessLayer.GetFeed(null);
            return View(publicImagesFeed);
        }

        [HttpGet]
        public async Task<FeedModel> GetImagesByWord(string word)
        {
            var imageFeed = await _imageBusinessLayer.GetFeed(word);
            return imageFeed;

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
