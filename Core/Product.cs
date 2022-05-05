using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Product
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}
