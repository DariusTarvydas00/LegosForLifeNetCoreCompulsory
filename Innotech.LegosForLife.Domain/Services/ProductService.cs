using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using InnoTech.LegosForLife.Core.IServices;
using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.Domain.IRepositories;

namespace InnoTech.LegosForLife.Domain.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new InvalidDataException("ProductRepository Cannot Be Null");
        }
        public List<Product> GetProducts()
        {
            return _productRepository.FindAll();
        }

        public Product GetProductById(int id)
        {
            if (id > 2147483645)
            {
                throw new InvalidDataException("Product Id limit reached");
            }
            if (id < 1 || id == null || id == 0)
            {
                throw new InvalidDataException("Product Id must be above zero");
            }

            return _productRepository.GetProductById(id);
        }

        public Product DeleteProduct(int id)
        {
            return _productRepository.DeleteProduct(id);
        }

        public Product CreateProduct(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidDataException("Product should contain valid name");
            }
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new InvalidDataException("Product should contain valid name");
            }
            
            var product = new Product()
            {
                Name = name
            };
            return product;
        }
    }
}