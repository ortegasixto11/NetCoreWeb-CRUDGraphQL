using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreWeb_CRUDGraphQL.Controllers;
using NetCoreWeb_CRUDGraphQL.Data;
using NetCoreWeb_CRUDGraphQL.Models;
using NetCoreWeb_CRUDGraphQL.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace UnitTesting
{
    public class ProductControllerTests
    {

        // https://www.c-sharpcorner.com/article/crud-operations-unit-testing-in-asp-net-core-web-api-with-xunit/
        // https://ardalis.com/testing-production-api-endpoints-with-xunit
        // https://www.codingame.com/playgrounds/35462/creating-web-api-in-asp-net-core-2-0/part-3---integration-tests
        // https://dotnetcorecentral.com/blog/asp-net-core-web-api-integration-testing-with-xunit/

        private readonly ApplicationDbContext _context;
        private HttpClient Client;

        public ProductControllerTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DB_CRUD_GraphQL;Trusted_Connection=True;MultipleActiveResultSets=true");
            _context = new ApplicationDbContext(optionsBuilder.Options);
        }


        #region Get Product By Id
        [Fact]
        public async void GetProductById_Return_Ok()
        {
            //Arrange  
            var controller = new ProductsController(_context);
            var id = Guid.Parse("9dab63d7-f537-42f9-2b88-08d7c1316ce0");

            //Act  
            var data = await controller.GetProduct(id);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void GetProductById_Return_NotFound()
        {
            //Arrange  
            var controller = new ProductsController(_context);
            var id = Guid.Parse("55195f54-b6f1-4504-9934-8ca552f0dc9e");

            //Act  
            var data = await controller.GetProduct(id);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data);
        }
        #endregion


        #region Create Product
        [Fact]
        public async void PostProduct_Return_Ok()
        {
            //Arrange
            var controller = new ProductsController(_context);
            var product = new Product {
                Name = "TEST"
            };

            //Act
            var data = await controller.PostProduct(product);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void PostProduct_NullObject_Return_BadRequest()
        {
            //Arrange
            var controller = new ProductsController(_context);

            //Act
            var data = await controller.PostProduct(null);

            //Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async void PostProduct_EmptyName_Return_BadRequest()
        {
            //Arrange
            var controller = new ProductsController(_context);
            var product = new Product
            {
                Name = ""
            };

            //Act
            var data = await controller.PostProduct(product);

            //Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }
        #endregion


        #region Update Product
        [Fact]
        public async void PutProduct_Return_Ok()
        {
            //Arrange
            var controller = new ProductsController(_context);
            var product = new Product
            {
                ID = Guid.Parse("a2357f6d-79ab-42d6-dbc7-08d7c1ffbf5e"),
                Name = "TEST"
            };

            //Act
            var data = await controller.PutProduct(product.ID, product);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void PutProduct_EmptyName_Returns_BadRequest()
        {
            //Arrange
            var controller = new ProductsController(_context);
            var product = new Product
            {
                ID = Guid.Parse("a2357f6d-79ab-42d6-dbc7-08d7c1ffbf5e"),
                Name = ""
            };

            //Act
            var data = await controller.PutProduct(product.ID, product);

            //Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async void PutProduct_NullObject_Returns_BadRequest()
        {
            //Arrange
            var controller = new ProductsController(_context);

            //Act
            var data = await controller.PutProduct(Guid.Empty, null);

            //Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }
        #endregion


        #region Delete Product

        [Fact]
        public async void DeleteProduct_Returns_Ok()
        {
            //Arrange
            var controller = new ProductsController(_context);
            var id = Guid.Parse("fb8a9841-c697-49fa-ba6f-08d7c455606e");

            //Act
            var data = await controller.DeleteProduct(id);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void DeleteProduct_IdNotExists_Returns_NotFound()
        {
            //Arrange
            var controller = new ProductsController(_context);
            var id = Guid.Parse("454f84ee-323b-469d-aaec-2552a20a0407");

            //Act
            var data = await controller.DeleteProduct(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(data);
        }

        [Fact]
        public async void DeleteProduct_EmptyId_Returns_NotFound()
        {
            //Arrange
            var controller = new ProductsController(_context);
            var id = Guid.Empty;

            //Act
            var data = await controller.DeleteProduct(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(data);
        }
        #endregion


    }
}
