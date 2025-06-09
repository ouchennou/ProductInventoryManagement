using ProductApp.Interfaces;
using ProductApp.Models;
using System.Collections.Generic;

namespace ProductApp.Services
{
    public class ProductService
    {
        private readonly IProductData _data;

        public ProductService(IProductData data)
        {
            _data = data;
        }

        public void AddProduct(string name, decimal price, int stock)
        {
            Product product = new(name, price, stock);
            _data.AddProduct(product);
        }

        public IEnumerable<Product> GetAllProducts() => _data.GetAll();

        public IEnumerable<Product> SearchProduct(string searchName) =>
            _data.SearchProduct(searchName);
    }
}
