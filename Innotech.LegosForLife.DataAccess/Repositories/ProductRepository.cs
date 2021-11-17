using System.Collections.Generic;
using System.IO;
using System.Linq;
using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.DataAccess.Entities;
using InnoTech.LegosForLife.Domain.IRepositories;

namespace InnoTech.LegosForLife.DataAccess.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly MainDbContext _ctx;

        public ProductRepository(MainDbContext ctx)
        {
            if (ctx == null) throw new InvalidDataException("Product Repository Must have a DBContext");
            _ctx = ctx;
        }
        public List<Product> FindAll()
        {
            return _ctx.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name
                })
                .ToList();
        }

        public Product GetProductById(int id)
        {
            var pEntity = _ctx.Products.Select(productEntity => new ProductEntity()
            {
                Id = productEntity.Id,
                Name = productEntity.Name
            }).FirstOrDefault();
            if (pEntity != null)
                return new Product()
                {
                    Id = pEntity.Id,
                    Name = pEntity.Name
                };
            return null;
        }

        public Product DeleteProduct(int id)
        {
            var pEntity = _ctx.Products.Remove(new ProductEntity()
            {
                Id = id,
            }).Entity;
            return new Product()
            {
                Id = pEntity.Id,
                Name = pEntity.Name
            };
        }

        public Product CreateProduct(string name)
        {
            var pEntity = _ctx.Products.Add(new ProductEntity()
            {
                Name = name
            }).Entity;
            return new Product()
            {
                Id = pEntity.Id,
                Name = pEntity.Name
            };
        }

        public Product UpdateProduct(Product productUpdate)
        {
            var pEntity = _ctx.Products.Update(new ProductEntity()
            {
                Id = productUpdate.Id,
                Name = productUpdate.Name
            }).Entity;
            return new Product()
            {
                Id = pEntity.Id,
                Name = pEntity.Name
            };

        }
    }
}