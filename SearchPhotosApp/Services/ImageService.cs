using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SearchPhotosApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace SearchPhotosApp.Services
{
    public class ImageService : IImageService
    {
        //private readonly HttpClient _client;
        private readonly IConfiguration _config;
        public ImageService(IConfiguration config)
        {
            //_client = client ?? throw new ArgumentNullException(nameof(client));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public async Task<string> GetFeed(string word)
        {
            var apiEndpointUri = _config.GetValue<string>("ApiSettings:FlickrFeedUrl");
            FeedModel feed = new FeedModel();
            List<ImageModel> imageObject= new List<ImageModel>();
            string uri = "";
            string apiResponse = "";
            if (String.IsNullOrEmpty(word))
                uri = $"{apiEndpointUri}";
            else
                uri = $"{apiEndpointUri}?tags={word}";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(uri))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();     
                }
            }
            return apiResponse;
        }

    }
}

