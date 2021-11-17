using System.Collections.Generic;
using InnoTech.LegosForLife.Core.Models;

namespace InnoTech.LegosForLife.Domain.IRepositories
{
    public interface IProductRepository
    {
        List<Product> FindAll();
        Product GetProductById(int id);
        Product DeleteProduct(int id);
        Product CreateProduct(string name);
    }
}