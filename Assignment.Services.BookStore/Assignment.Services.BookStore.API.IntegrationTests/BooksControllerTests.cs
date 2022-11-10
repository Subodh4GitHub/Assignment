// **************************************************************************
// <copyright file="BooksControllerTests.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.API.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using Assignment.Services.BookStore.API.Controllers;
    using Assignment.Services.BookStore.Infrastructure.Common;
    using Assignment.Services.BookStore.Infrastructure.DataContext;
    using Assignment.Services.BookStore.Infrastructure.Entities;
    using Assignment.Services.BookStore.Infrastructure.Logger;
    using Assignment.Services.BookStore.Infrastructure.Managers;
    using Assignment.Services.BookStore.Infrastructure.Managers.Contracts;
    using Assignment.Services.BookStore.Infrastructure.Models;
    using Assignment.Services.BookStore.Infrastructure.Repositories.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Test class for Api Controller.
    /// </summary>
    [TestClass]
    public class BooksControllerTests
    {
        /// <summary>
        /// Mock object of type IBookRepository.
        /// </summary>
        private readonly Mock<IBookRepository> mockBookRepository;

        /// <summary>
        /// Mock object of type ILogger of type book controller.
        /// </summary>
        private readonly Mock<ILogger<BooksController>> mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksControllerTests"/> class.
        /// </summary>
        public BooksControllerTests()
        {
            mockBookRepository = new Mock<IBookRepository>();
            mockLogger = new Mock<ILogger<BooksController>>();
        }

        // positive test

        /// <summary>
        /// AddAsync should add the book when model is valid
        /// </summary>
        [TestMethod]
        public void AddAsyncShouldAddTheBookWhenModelIsValid()
        {
            // arrange
            var bookId = Guid.NewGuid();
            mockBookRepository.Setup(x => x.AddBookAsync(It.IsAny<Book>())).ReturnsAsync(bookId);

            var provider = GetServiceProvider();

            var controller = new BooksController(provider.GetService<IBookManager>(), mockLogger.Object);
            var bookDetails = new BookDetails()
            {
                Name = "TestBook",
                AuthorName = "TestAuthorName"
            };

            // act
            var result = controller.AddAsync(bookDetails).Result;

            // assert
            Assert.AreEqual(bookId, ((object[])((CreatedAtActionResult)result).RouteValues.Values)[0]);
        }

        /// <summary>
        /// GetAllAsync should return all books when no database exception is thrown.
        /// </summary>
        [TestMethod]
        public void GetAllAsyncShouldReturnAllBooksWhenNoDatabaseException()
        {
            // arrange
            mockBookRepository.Setup(x => x.GetBooksAsync()).ReturnsAsync(new List<Book>()
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = "TestName",
                    AuthorName = "AuthorName"
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = "TestName1",
                    AuthorName = "AuthorName1"
                }
            });

            var provider = GetServiceProvider();

            var controller = new BooksController(provider.GetService<IBookManager>(), mockLogger.Object);

            // act
            var result = controller.GetAllAsync().Result as OkObjectResult;
            var contentResult = result?.Value as List<BookDetails>;

            // assert
            Assert.AreEqual(2, contentResult?.Count);
        }

        /// <summary>
        /// GetAsync should return the book based on identifier when no database exception is thrown.
        /// </summary>
        [TestMethod]
        public void GetAsyncShouldReturnABookWhenNoDatabaseException()
        {
            // arrange
            mockBookRepository.Setup(x => x.GetBookAsync(It.IsAny<Guid>())).ReturnsAsync(new Book()
            {
                Id = Guid.NewGuid(),
                Name = "TestName",
                AuthorName = "AuthorName"
            });

            var provider = GetServiceProvider();

            var controller = new BooksController(provider.GetService<IBookManager>(), mockLogger.Object);

            // act
            var result = controller.GetAsync(Guid.NewGuid()).Result as OkObjectResult;
            var contentResult = result?.Value as BookDetails;

            // assert
            Assert.AreEqual("TestName", contentResult?.Name);
        }

        /// <summary>
        /// UpdateAsync should update book details when no database exception is thrown.
        /// </summary>
        [TestMethod]
        public void UpdateAsyncShouldUpdateBookDetailsWhenNoDatabaseException()
        {
            // arrange
            mockBookRepository.Setup(x => x.UpdateBookDetailsAsync(It.IsAny<Book>())).ReturnsAsync(true);

            var provider = GetServiceProvider();

            var bookDetails = new BookDetails()
            {
                Name = "TestBook",
                AuthorName = "TestAuthorName"
            };

            var controller = new BooksController(provider.GetService<IBookManager>(), mockLogger.Object);

            // act
            var result = controller.UpdateAsync(Guid.NewGuid(), bookDetails).Result as OkObjectResult;

            // assert
            Assert.IsNotNull(result?.Value.ToString());
        }

        // negative test

        /// <summary>
        /// AddAsync should throw internal error when Database connection is lost.
        /// </summary>
        [TestMethod]
        public void AddAsyncShouldThrowInternalErrorWhenDatabaseConnectionIsLost()
        {
            // arrange
            mockBookRepository.Setup(x => x.AddBookAsync(It.IsAny<Book>())).ThrowsAsync(new Exception("Unable to connect to database."));

            var provider = GetServiceProvider();

            var controller = new BooksController(provider.GetService<IBookManager>(), mockLogger.Object);
            var bookDetails = new BookDetails()
            {
                Name = "TestBook",
                AuthorName = "TestAuthorName"
            };

            // act
            var result = controller.AddAsync(bookDetails).Result;

            // assert
            Assert.AreEqual(500, ((ObjectResult)result).StatusCode.Value);
        }

        /// <summary>
        /// GetAsync should return status code as 404 when  o books are available.
        /// </summary>
        [TestMethod]
        public void GetAsyncShouldReturnStatusCode404WhenNoBooIsAvailable()
        {
            // arrange
            mockBookRepository.Setup(x => x.GetBookAsync(It.IsAny<Guid>())).ReturnsAsync((Book)null);

            var provider = GetServiceProvider();

            var controller = new BooksController(provider.GetService<IBookManager>(), mockLogger.Object);

            // act
            var result = controller.GetAsync(Guid.NewGuid()).Result;

            // assert
            Assert.AreEqual(404, ((ObjectResult)result).StatusCode.Value);
        }

        /// <summary>
        /// GetAsync should return BadRequest when book identifier is invalid.
        /// </summary>
        [TestMethod]
        public void GetAsyncShouldReturnBadRequestWhenBookIdentifierIsInvalid()
        {
            // arrange
            mockBookRepository.Setup(x => x.GetBookAsync(It.IsAny<Guid>())).ReturnsAsync((Book)null);

            var provider = GetServiceProvider();

            var controller = new BooksController(provider.GetService<IBookManager>(), mockLogger.Object);

            // act
            var result = controller.GetAsync(default).Result;

            // assert
            Assert.AreEqual(400, ((ObjectResult)result).StatusCode.Value);
        }

        /// <summary>
        /// Build Mediator container.
        /// </summary>
        /// <returns>Service collection</returns>
        private IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton(mockBookRepository.Object);
            services.AddScoped<IBookManager, BookManager>();
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("TestConnectionString"));
            services.AddAutoMapper(typeof(Mapper));
            SerilogExtensions.AddSerilog();
            return services.BuildServiceProvider();
        }
    }
}
