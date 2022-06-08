using SearchPhotosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPhotosApp.BLL
{
    public interface IImageBusinessLayer
    {
        public Task<FeedModel> GetFeed(string word);
        public List<ImageModel> ProcessFeed(string response);
    }
}
