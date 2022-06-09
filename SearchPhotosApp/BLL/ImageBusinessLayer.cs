using SearchPhotosApp.BLL;
using SearchPhotosApp.Models;
using SearchPhotosApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SearchPhotosApp.BLL
{
    public class ImageBusinessLayer:IImageBusinessLayer
    {
        private readonly IImageService _imageService;
        public ImageBusinessLayer(IImageService imageService)
        {
            _imageService = imageService;
        }
        public async Task<FeedModel> GetFeed(string word)
        {
            try
            {
                FeedModel feed = new FeedModel();
                var response = await _imageService.GetFeed(word);
                feed.Images = ProcessFeed(response);
                return feed;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ImageModel> ProcessFeed(string response)
        {
            List<ImageModel> imageObject = new List<ImageModel>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);
            XmlNodeList entries = doc.DocumentElement.SelectNodes("*");

            foreach (XmlElement entryNode in entries)
            {
                if (entryNode.Name == "entry")
                {
                    foreach (XmlNode objectNode in entryNode)
                    {
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
            return imageObject; 
        }
    }
}
