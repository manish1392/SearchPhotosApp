using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SearchPhotosApp.Models;
using SearchPhotosApp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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
        public async Task<FeedModel> GetFeed(string word)
        {
            var apiEndpointUri = _config.GetValue<string>("ApiSettings:FlickrFeedUrl");
            FeedModel feed = new FeedModel();
            List<ImageModel> imageObject= new List<ImageModel>();
            string uri = "";
            if (String.IsNullOrEmpty(word))
                uri = $"{apiEndpointUri}";
            else
                uri = $"{apiEndpointUri}?tags={word}";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(uri))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(apiResponse);
                    //XmlNodeList entries = doc.DocumentElement.SelectNodes("/xml/feed/entry/link[@type='image/jpeg']");
                    XmlNodeList entries = doc.DocumentElement.SelectNodes("*");

                    foreach (XmlElement entryNode in entries)
                    {
                        if(entryNode.Name == "entry")
                        {
                            foreach(XmlNode objectNode in entryNode) {
                                if (objectNode.Name == "link" && objectNode.Attributes["type"].Value == "image/jpeg")
                                {
                                    ImageModel image = new ImageModel
                                    {
                                        Link = objectNode.Attributes["href"].Value
                                    };
                                    imageObject.Add(image);
                                }
                            }
                            
                        }
                        
                    }     
                }
            }
            feed.Images = imageObject;
            return feed;
        }

    }
}

