using System.Collections.Generic;
using System.Linq;
using ProductApp.Interfaces;
using ProductApp.Models;

namespace ProductApp.Data
{
    public class InMemoryProductData : IProductData
    {
        private readonly List<Product> _products = new();
        
        public void AddProduct(Product product) 
        {
            _products.Add(product);
        }

        public IEnumerable<Product> GetAll() => _products;
        public IEnumerable<Product> SearchProduct(string name) =>
            _products.Where(p => p.Name.ToLower().Contains(name.ToLower()));
        
    }
}