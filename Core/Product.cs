using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Core
{
    public class Product
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        private ObjectId _id { get; set; }
        public ObjectId Id { get { return _id; } }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public BitmapImage Image
        {
            get
            {
                try 
                {
                    return new BitmapImage(new Uri(ImageLink, UriKind.Absolute));
                }
                catch
                {
                    return new BitmapImage();
                }
            }
        }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}
