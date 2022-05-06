using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DataAccess
    {
        public static MongoClient client = new MongoClient("mongodb://localhost");
        public static MongoDatabaseBase database = (MongoDatabaseBase)client.GetDatabase("Raskulak");
        public delegate void NewItemAddedDelegate();

        public static event NewItemAddedDelegate NewItemAddedEvent;

        public static List<Product> GetProducts()
        {
            var products = (MongoCollectionBase<Product>)database.GetCollection<Product>("Product");
            return products.Find(x => true).ToList();
        }
        public static void AddToBasket(Product product, int count, User user)
        { 
            var basket = (MongoCollectionBase<BasketItem>)database.GetCollection<BasketItem>("Basket");

            var basketItem = new BasketItem 
            {
                Product = product,
                User = user,
                Count = count
            };

            basket.InsertOneAsync(basketItem);
        }

        public static void DeleteOrder(Order order)
        {
            var orders = (MongoCollectionBase<Order>)database.GetCollection<Order>("Order");
            var builder = Builders<Order>.Filter;
            var filter = builder.Eq(x => x.Id, order.Id);
            orders.DeleteOneAsync(filter);
        }

        public static List<BasketItem> GetBasketItems(User user)
        {
            var basketItems = (MongoCollectionBase<BasketItem>)database.GetCollection<BasketItem>("Basket");
            return basketItems.Find(x=> x.User == user).ToList();
        }
        public static List<Order> GetOrders()
        {
            var orders = (MongoCollectionBase<Order>)database.GetCollection<Order>("Order");
            return orders.Find(x => true).ToList();
        }
        public static List<Order> GetOrders(User user)
        {
            var orders = GetOrders();
            return orders.Where(o => o.User.Login == user.Login).ToList();
        }
        public static void CreateOrder(Order order)
        {
            var orders = (MongoCollectionBase<Order>)database.GetCollection<Order>("Order");
            var basket = (MongoCollectionBase<BasketItem>)database.GetCollection<BasketItem>("Basket");
            foreach (var item in GetBasketItems(order.User))
            {
                basket.DeleteOneAsync(x=> x.User == item.User);
            }

            orders.InsertOneAsync(order);
        }

        public static User GetUser(string login, string password)
        {
            var users = (MongoCollectionBase<User>)database.GetCollection<User>("User");
            return users.Find(x => x.Login == login && x.Password == password).FirstOrDefault();
        }
        public static void AddUser(User user)
        {
            var users = (MongoCollectionBase<User>)database.GetCollection<User>("User");
            users.InsertOneAsync(user);
        }
        public static void AddProduct(Product product)
        {
            var products = (MongoCollectionBase<Product>)database.GetCollection<Product>("Product");
            products.InsertOneAsync(product);
            NewItemAddedEvent?.Invoke();
        }

        public static void UpdateCount(Product product, int count)
        {
            var products = (MongoCollectionBase<Product>)database.GetCollection<Product>("Product");
            product.Count -= count;

            var filter = Builders<Product>.Filter.Eq(s => s.Id, product.Id);
            var result = products.ReplaceOneAsync(filter, product);

            if (product.Count == 0)
                DeleteProduct(product);
            NewItemAddedEvent?.Invoke();
        }
        public static void UpdateProduct(Product product)
        {
            var products = (MongoCollectionBase<Product>)database.GetCollection<Product>("Product");
            var filter = Builders<Product>.Filter.Eq(s => s.Id, product.Id);
            var result = products.ReplaceOneAsync(filter, product);
            if (product.Count == 0)
                DeleteProduct(product);
            NewItemAddedEvent?.Invoke();
        }

        public static List<Role> GetRoles()
        {
            var roles = (MongoCollectionBase<Role>)database.GetCollection<Role>("Role");

            return roles.Find(x => true).ToList();
        }
        public static Role GetRole(string name)
        {
            return GetRoles().FirstOrDefault(x => x.Name == name);
        }
        public static void DeleteProduct(Product product)
        {
            var products = (MongoCollectionBase<Product>)database.GetCollection<Product>("Product");
            var builder = Builders<Product>.Filter;
            var filter = builder.Eq(x => x.Id, product.Id);
            products.DeleteOneAsync(filter);
            NewItemAddedEvent?.Invoke();
        }
    }
}
