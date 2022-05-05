﻿using MongoDB.Driver;
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
        public static List<BasketItem> GetBasketItems(User user)
        {
            var basketItems = (MongoCollectionBase<BasketItem>)database.GetCollection<BasketItem>("Basket");
            return basketItems.Find(x=> x.User == user).ToList();
        }
        public static List<Order> GetOrders()
        {
            var basketItems = (MongoCollectionBase<Order>)database.GetCollection<Order>("Order");
            return basketItems.Find(x => true).ToList();
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
    }
}
