// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using BookStoreDBFirst.Controllers;
// using BookStoreDBFirst.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.InMemory;
// using Microsoft.EntityFrameworkCore.SqlServer;
// using NUnit.Framework;

// namespace BookStoreDBFirst.Tests
// {
//     [TestFixture]
//     public class BookControllerTests
//     {
//         private BookController _controller;
//         private BookStoreContext _context;

//         [SetUp]
//         public void Setup()
//         {
//             var options = new DbContextOptionsBuilder<BookStoreContext>()
//                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a new unique database name for each test
//                 .Options;

//             _context = new BookStoreContext(options);

//             // Add test data
//             _context.Books.AddRange(
//                 new List<Book>
//                 {
//                     new Book
//                     {
//                         Id = 1,
//                         Title = "Book 1",
//                         Author = "Author 1"
//                     },
//                     new Book
//                     {
//                         Id = 2,
//                         Title = "Book 2",
//                         Author = "Author 2"
//                     },
//                     new Book
//                     {
//                         Id = 3,
//                         Title = "Book 3",
//                         Author = "Author 3"
//                     }
//                 }
//             );
//             _context.SaveChanges();

//             _controller = new BookController(_context);
//         }
//         [TearDown]
//         public void TearDown()
//         {
//             _context.Dispose(); // Properly dispose the context to release resources
//         }

//         [Test]
//         public async Task GetAllBooks_ReturnsOkResultWithBooks()
//         {
//             // Act
//             var result = await _controller.GetAllBooks();

//             // Assert
//             Assert.IsInstanceOf<OkObjectResult>(result.Result);
//             var okResult = result.Result as OkObjectResult;
//             Assert.IsInstanceOf<IEnumerable<Book>>(okResult.Value);
//             var books = okResult.Value as IEnumerable<Book>;
//             Assert.AreEqual(3, books.Count());
//         }

//         [Test]
//         public async Task GetBookById_NonExistingId_ReturnsNotFound()
//         {
//             // Arrange
//             var nonExistingId = 4; // Non-existing id

//             // Act
//             var result = await _controller.GetBookById(nonExistingId);

//             // Assert
//             Assert.IsInstanceOf<NotFoundResult>(result.Result);
//         }

//         [Test]
//         public async Task GetBookById_InvalidId_ReturnsNotFound()
//         {
//             // Arrange
//             var invalidId = -1; // Invalid id

//             // Act
//             var result = await _controller.GetBookById(invalidId);

//             // Assert
//             Assert.IsInstanceOf<NotFoundResult>(result.Result);
//         }

//         [Test]
//         public async Task GetTotalNumberOfBooks_ReturnsOkResultWithTotalBooksCount()
//         {
//             // Act
//             var result = await _controller.GetTotalNumberOfBooks();

//             // Assert
//             Assert.IsInstanceOf<OkObjectResult>(result.Result);
//             var okResult = result.Result as OkObjectResult;
//             Assert.IsInstanceOf<int>(okResult.Value);
//             var totalBooks = (int)okResult.Value;
//             Assert.AreEqual(3, totalBooks);
//         }
//     }
// }
