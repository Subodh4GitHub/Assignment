// **************************************************************************
// <copyright file="BooksController.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Assignment.Services.BookStore.Infrastructure.Managers.Contracts;
    using Assignment.Services.BookStore.Infrastructure.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Controller class to receive incoming REST API request.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        /// <summary>
        /// Manager class to deal with the business logic.
        /// </summary>
        private IBookManager bookManager;

        /// <summary>
        /// Represents object to deal with logging.
        /// </summary>
        private ILogger<BooksController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksController"/> class.
        /// </summary>
        /// <param name="bookManager">Manager class to deal with the business logic.</param>
        /// <param name="logger">Represents object to deal with logging.</param>
        public BooksController(IBookManager bookManager, ILogger<BooksController> logger)
        {
            this.bookManager = bookManager;
            this.logger = logger;
        }

        /// <summary>
        /// Adds a new book.
        /// </summary>
        /// <param name="bookDetails">Details of the resource to be added.</param>
        /// <returns>Created object.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody] BookDetails bookDetails)
        {
            try
            {
                var bookId = await bookManager.AddBookAsync(bookDetails);
                bookDetails.Id = bookId;
                return CreatedAtAction("Get", new { id = bookId }, bookDetails);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Problem("Error in processing the request. Please try again later");
            }
        }

        /// <summary>
        /// Get all the books from the repository.
        /// </summary>
        /// <returns>List of books.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var books = await bookManager.GetBooksAsync();
                if (books == null)
                {
                    return NotFound("No record found.");
                }

                return Ok(books);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
                return Problem("Error in processing the request. Please try again later");
            }
        }

        /// <summary>
        /// Get the book based on the book identifier.
        /// </summary>
        /// <param name="id">Identifier for the book.</param>
        /// <returns>Book based on Identifier.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                if (id == default)
                {
                    ModelState.AddModelError(nameof(id), "Id cannot be the default guid.");
                    return BadRequest(ModelState);
                }

                var book = await bookManager.GetBookAsync(id);

                if (book == null)
                {
                    return NotFound("No record found.");
                }

                return Ok(book);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
                return Problem("Error in processing the request. Please try again later");
            }
        }

        /// <summary>
        /// Update the book details.
        /// </summary>
        /// <param name="id">Identifier for the book.</param>
        /// <param name="bookDetails">Details of the book to be updated.</param>
        /// <returns>Status whether the book details are updated.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] BookDetails bookDetails)
        {
            try
            {
                if (id == default)
                {
                    ModelState.AddModelError(nameof(id), "Id cannot be the default guid.");
                    return BadRequest(ModelState);
                }

                await bookManager.UpdateBookDetailsAsync(id, bookDetails);
                return Ok($"Book with Id : {id} updated successfully.");
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
                return Problem("Error in processing the request. Please try again later");
            }
        }
    }
}
