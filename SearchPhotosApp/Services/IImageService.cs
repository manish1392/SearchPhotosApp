using SearchPhotosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPhotosApp.Services
{
    public interface IImageService
    {
        public Task<string> GetFeed(string word);
    }
}
