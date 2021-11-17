using System.Collections.Generic;
using System.IO;
using System.Linq;
using InnoTech.LegosForLife.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Xunit;
using System.Reflection;
using InnoTech.LegosForLife.Core.IServices;
using InnoTech.LegosForLife.Core.Models;
using Moq;

namespace InnoTech.LegosForLife.WebApi.Test.Controllers
{
    public class ProductControllerTest
    {
        #region Controller Intialization

        [Fact]
        public void ProductController_HasProductService_IsOfTypeControllerBase()
        {
            var service = new Mock<IProductService>();
            var controller = new ProductController(service.Object);
            Assert.IsAssignableFrom<ControllerBase>(controller);
        }
        
        [Fact]
        public void ProductController_WithNullProductService_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new ProductController(null)
            );

        }
        
        [Fact]
        public void ProductController_WithNullProductRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ProductController(null)
            );
            Assert.Equal("ProductService Cannot Be Null",exception.Message);
        }
        
        [Fact]
        public void ProductController_UsesApiControllerAttribute()
        {
            //Arrange
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attr = typeInfo
                .GetCustomAttributes()
                .FirstOrDefault(a => a.GetType()
                    .Name.Equals("ApiControllerAttribute"));
            //Assert
            Assert.NotNull(attr);
        }  
        
        [Fact]
        public void ProductController_UsesRouteAttribute()
        {  
            //Arrange
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attr = typeInfo
                .GetCustomAttributes()
                .FirstOrDefault(a => a.GetType()
                    .Name.Equals("RouteAttribute"));
            //Assert
            Assert.NotNull(attr);
        }
        
        [Fact]
        public void ProductController_UsesRouteAttribute_WithParamApiControllerNameRoute()
        {  
            //Arrange
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attr = typeInfo
                .GetCustomAttributes()
                .FirstOrDefault(a => a.GetType()
                    .Name.Equals("RouteAttribute")) as RouteAttribute;
            //Assert
            Assert.Equal("api/[controller]", attr.Template);
        }
        

        #endregion

        #region GetAll Method

        [Fact]
        public void ProductController_HasGetAllMethod()
        {
            var method = typeof(ProductController)
                .GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void GetAll_WithNoParams_IsPublic()
        {
            var method = typeof(ProductController)
                .GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.True(method.IsPublic);
        }
        
        [Fact]
        public void GetAll_WithNoParams_ReturnsListOfProductsInActionResult()
        {
            var method = typeof(ProductController)
                .GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<List<Product>>).FullName, method.ReturnType.FullName);
        }

        [Fact]
        public void GetAll_WithNoParams_HasGetHttpAttribute()
        {
            var methodInfo = typeof(ProductController)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "GetAll");
            var attr = methodInfo.CustomAttributes
                .FirstOrDefault(ca => ca.AttributeType.Name == "HttpGetAttribute");
            Assert.NotNull(attr);
        }
        
        [Fact]
        public void GetAll_CallsServicesGetProducts_Once()
        {
            //Arrange
            var mockService = new Mock<IProductService>();
            var controller = new ProductController(mockService.Object);
            
            //Act
            controller.GetAll();
            
            //Assert
            mockService.Verify(s => s.GetProducts(),Times.Once);

        }


        #endregion

        #region Post Method

        

        #endregion
        
    }
}

