using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchPhotosApp.Utility
{
    public static class XmlUtility
    {
        public static Object CreateObject(string XMLString, Object YourClassObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(YourClassObject.GetType());
            //The StringReader will be the stream holder for the existing XML file 
            YourClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            //initially deserialized, the data is represented by an object without a defined type 
            return YourClassObject;
        }
    }
}
