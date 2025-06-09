using ProductApp.Models;
using System.Collections.Generic;

namespace ProductApp.Interfaces
{
    public interface IProductData
    {
        void AddProduct(Product product);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> SearchProduct(string name);
    }
    
}