using System.Collections.Generic;
using InnoTech.LegosForLife.Core.Models;

namespace InnoTech.LegosForLife.Core.IServices
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProductById(int id);

        Product DeleteProduct(int id);

        Product CreateProduct(string name);

        Product UpdateProduct(Product productUpdate);
    }
}