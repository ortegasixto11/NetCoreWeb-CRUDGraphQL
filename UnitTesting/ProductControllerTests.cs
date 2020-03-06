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


        #region GetProductById
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

        #region CreateProduct
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
                Name = "TEST"
            };

            //Act
            var data = await controller.PostProduct(product);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }


        #endregion


    }
}
