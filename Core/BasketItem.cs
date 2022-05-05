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
        private ObjectId _id;
        public ObjectId Id { get { return _id; } }
        public User User { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
