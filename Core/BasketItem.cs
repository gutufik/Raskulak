using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BasketItem
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
