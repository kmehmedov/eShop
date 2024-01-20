using Catalog.API.Controllers;
using Catalog.Application.Models;
using Catalog.Application.Queries;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.Test
{
    [TestClass]
    public class CatalogControllerTests
    {
        public CatalogControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<CatalogController>>();
            _dbOptions = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            SeedData();
        }

        [TestMethod]
        public async Task GetCatalogItemByIdAsync_ReturnsOkObjectResult_WhenItemExists()
        {
            // Arrange
            var controller = new CatalogController(_mediatorMock.Object, _loggerMock.Object);

            var itemId = 1;
            var expectedResult = new CatalogItemDTO { Id = 1, Name = "Galaxy S23", Description ="Galaxy S23", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = 1, AvailableQuantity = 100, Price = 300m };
            var queryResult = new QueryResult<CatalogItemDTO>
            {
                Type = QueryResultTypeEnum.Success,
                Result = expectedResult
            };

            using var dbContext = new CatalogContext(_dbOptions);
            CatalogItemRepository catalogItemRepository = new CatalogItemRepository(dbContext);
            GetCatalogItemByIdQueryHandler handler = new GetCatalogItemByIdQueryHandler(catalogItemRepository);

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCatalogItemByIdQuery>(), default))
                        .Returns(async (GetCatalogItemByIdQuery query, CancellationToken token) => await handler.Handle(query, token));

            // Act
            var result = await controller.GetCatalogItemByIdAsync(itemId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.IsInstanceOfType(okObjectResult.Value, typeof(CatalogItemDTO));
            var actualResult = (CatalogItemDTO)okObjectResult.Value;

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.Id, actualResult.Id);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
            Assert.AreEqual(expectedResult.Description, actualResult.Description);
            Assert.AreEqual(expectedResult.CatalogBrandId, actualResult.CatalogBrandId);
            Assert.AreEqual(expectedResult.Price, actualResult.Price);
        }

        [TestMethod]
        public async Task GetCatalogItemByIdAsync_ReturnsNotFoundResult_WhenItemDoesNotExist()
        {
            // Arrange
            var controller = new CatalogController(_mediatorMock.Object, _loggerMock.Object);

            var itemId = 100;
            var queryResult = new QueryResult<CatalogItemDTO>
            {
                Type = QueryResultTypeEnum.NotFound
            };

            using var dbContext = new CatalogContext(_dbOptions);
            CatalogItemRepository catalogItemRepository = new CatalogItemRepository(dbContext);
            GetCatalogItemByIdQueryHandler handler = new GetCatalogItemByIdQueryHandler(catalogItemRepository);

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCatalogItemByIdQuery>(), default))
                        .Returns(async (GetCatalogItemByIdQuery query, CancellationToken token) => await handler.Handle(query, token));

            // Act
            var result = await controller.GetCatalogItemByIdAsync(itemId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetCatalogItemsAsync_ReturnsOkObjectResult_WithListOfCatalogItems()
        {
            // Arrange
            var controller = new CatalogController(_mediatorMock.Object, _loggerMock.Object);

            var expectedResult = new List<CatalogItemDTO>()
            {
                new() { Name = "Galaxy S23", Description ="Galaxy S23", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = 1, AvailableQuantity = 100, Price = 300m },
                new() { Name = "Galaxy Z Fold4", Description ="Galaxy Z Fold4", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = 1, AvailableQuantity = 130, Price = 1450m },
                new() { Name = "Galaxy Z Flip", Description ="Galaxy Z Flip", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = 1, AvailableQuantity = 130, Price = 950m },
                new() { Name = "Galaxy A52", Description ="\"Galaxy A52", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = 1, AvailableQuantity = 130, Price = 450m },
                new() { Name = "Galaxy M14", Description ="Galaxy M14", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = 1, AvailableQuantity = 130, Price = 450m },
                new() { Name = "Galaxy F04", Description ="Galaxy F04", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = 1, AvailableQuantity = 130, Price = 450m },
            };
            var queryResult = new QueryResult<IEnumerable<CatalogItemDTO>>
            {
                Type = QueryResultTypeEnum.Success,
                Result = expectedResult
            };

            using var dbContext = new CatalogContext(_dbOptions);
            CatalogItemRepository catalogItemRepository = new CatalogItemRepository(dbContext);
            GetCatalogItemsQueryHandler handler = new GetCatalogItemsQueryHandler(catalogItemRepository);

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCatalogItemsQuery>(), default))
                        .Returns(async (GetCatalogItemsQuery query, CancellationToken token) => await handler.Handle(query, token));

            // Act
            var result = await controller.GetCatalogItemsAsync();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.IsInstanceOfType(okObjectResult.Value, typeof(IEnumerable<CatalogItemDTO>));
            var actualResult = (IEnumerable<CatalogItemDTO>)okObjectResult.Value;

            Assert.AreEqual(expectedResult.Count, actualResult.Count());
        }

        [TestMethod]
        public async Task GetCatalogBrandsAsync_ReturnsOkObjectResult_WithListOfCatalogBrands()
        {
            // Arrange
            var controller = new CatalogController(_mediatorMock.Object, _loggerMock.Object);

            var expectedResult = new List<CatalogBrandDTO>()
            {
                new() { Name = "Samsung"},
                new() { Name = "iPhone" },
                new() { Name = "Xiaomi" },
                new() { Name = "Nokia" },
                new() { Name = "Other" }
            };
            var queryResult = new QueryResult<IEnumerable<CatalogBrandDTO>>
            {
                Type = QueryResultTypeEnum.Success,
                Result = expectedResult
            };

            using var dbContext = new CatalogContext(_dbOptions);
            CatalogBrandRepository catalogBrandRepository = new CatalogBrandRepository(dbContext);
            GetCatalogBrandsQueryHandler handler = new GetCatalogBrandsQueryHandler(catalogBrandRepository);

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCatalogBrandsQuery>(), default))
                        .Returns(async (GetCatalogBrandsQuery query, CancellationToken token) => await handler.Handle(query, token));

            // Act
            var result = await controller.GetCatalogBrandsAsync();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.IsInstanceOfType(okObjectResult.Value, typeof(IEnumerable<CatalogBrandDTO>));
            var actualResult = (IEnumerable<CatalogBrandDTO>)okObjectResult.Value;

            Assert.AreEqual(expectedResult.Count, actualResult.Count());
        }

        #region Private members
        private void SeedData()
        {
            using var dbContext = new CatalogContext(_dbOptions);
            new CatalogContextSeed().SeedAsync(dbContext).GetAwaiter().GetResult();
        }

        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<CatalogController>> _loggerMock;
        private readonly DbContextOptions<CatalogContext> _dbOptions;
        #endregion
    }
}
