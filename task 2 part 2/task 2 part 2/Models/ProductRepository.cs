using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task_2_part_2.Models
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();
        private int _nextId = 1;

        public ProductRepository()
        {
            Add(new Product {  Name = "Tomato soup", Category = "Groceries", Price = 1.39M });
            Add(new Product {  Name = "Yo-yo", Category = "Toys", Price = 3.75M });
            Add(new Product {  Name = "Hammer", Category = "Hardware", Price = 16.99M });
        }

        public IEnumerable<Product> GetAll() // read all
        {
            return products;
        }

        public Product Get(int id) // read specific product
        {
            return products.Find(p => p.Id == id);
        }

        public Product Add(Product item) //ceate new product
        {
            if (item == null)
            {
                throw new ArgumentNullException(" invalid item variables");
            }
            item.Id = _nextId++;
            products.Add(item);
            return item;
        }

        public void Remove(int id) // delete product
        {
            products.RemoveAll(p => p.Id == id);
        }

        public bool Update(Product item) // update product
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
                int index = products.FindIndex(p => p.Id == item.Id);
                if (index == -1)
                {
                    return false;
                }
                products.RemoveAt(index);
                products.Add(item);
                return true;
            }
        }

    }