using System;
using System.Collections.Generic;
using System.IO;
using InnoTech.LegosForLife.Core.IServices;
using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.Domain.IRepositories;
using InnoTech.LegosForLife.Domain.Services;
using Moq;
using Xunit;

namespace InnoTech.LegosForLife.Domain.Test
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _mock;
        private readonly ProductService _service;
        private readonly List<Product> _expected;

        public ProductServiceTest()
        {
            _mock = new Mock<IProductRepository>();
            _service = new ProductService(_mock.Object);
            _expected = new List<Product>
            {
                new Product { Id = 1, Name = "Lego1" },
                new Product { Id = 2, Name = "Lego2" }
            };
        }
        
        [Fact]
        public void ProductService_IsIProductService()
        {
            Assert.True(_service is IProductService);
        }
        
        
        [Fact]
        public void ProductService_WithNullProductRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new ProductService(null)
                );

        }
        
        [Fact]
        public void ProductService_WithNullProductRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ProductService(null)
            );
            Assert.Equal("ProductRepository Cannot Be Null",exception.Message);
        }
        
        
        [Fact]
        public void GetProducts_CallsProductRepositoriesFindAll_ExactlyOnce()
        {
            _service.GetProducts();
            _mock.Verify(r => r.FindAll(), Times.Once);
        }
        
        [Fact]
        public void GetProducts_NoFilter_ReturnsListOfAllProducts()
        {
            _mock.Setup(r => r.FindAll())
                .Returns(_expected);
            var actual = _service.GetProducts();
            Assert.Equal(_expected, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void GetProductById_Less_Than_Zero_Exception(int value)
        {
            var ex = Assert.Throws<InvalidDataException>(() => _service.GetProductById(value));
            Assert.Equal("Product Id must be above zero", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        public void GetProductById_Null_Exception(int value)
        {
            
            var ex = Assert.Throws<InvalidDataException>(() => _service.GetProductById(value));
            Assert.Equal("Product Id must be above zero", ex.Message);
        }

        [Fact]
        public void GetProductById_IdIntMaxValue_Exception()
        {
            int maxValue = Int32.MaxValue;
            var ex = Assert.Throws<InvalidDataException>(() => _service.GetProductById(2147483647));
            Assert.Equal("Product Id limit reached", ex.Message);
        }
    }
}