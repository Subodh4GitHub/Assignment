// **************************************************************************
// <copyright file="BookRepository.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Assignment.Services.BookStore.Infrastructure.DataContext;
    using Assignment.Services.BookStore.Infrastructure.Entities;
    using Assignment.Services.BookStore.Infrastructure.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Book repository to deal with the database operations.
    /// </summary>
    public class BookRepository : IBookRepository
    {
        /// <summary>
        /// Data context object to deal with the database.
        /// </summary>
        private BookStoreContext bookStoreContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="bookStoreContext">Data context object to deal with the database.</param>
        public BookRepository(BookStoreContext bookStoreContext)
        {
            this.bookStoreContext = bookStoreContext;
        }

        /// <summary>
        /// Add a new book to the book store repository.
        /// </summary>
        /// <param name="bookDetails">Details of the book to be added.</param>
        /// <returns>Book identifier.</returns>
        public async Task<Guid> AddBookAsync(Book bookDetails)
        {
            try
            {
                bookStoreContext.Books.Add(bookDetails);
                await bookStoreContext.SaveChangesAsync();

                return bookDetails.Id;
            }
            catch (Exception exception)
            {
                var message =
                    $"{exception.Message}{Environment.NewLine}Error Details: AddBookAsync Failed. {Environment.NewLine}{exception.StackTrace}";
                throw new Exception(message);
            }
        }

        /// <summary>
        /// Gets the list of books from the book store repository.
        /// </summary>
        /// <returns>List of book details.</returns>
        public async Task<List<Book>> GetBooksAsync()
        {
            try
            {
                return await bookStoreContext.Books.ToListAsync();
            }
            catch (Exception exception)
            {
                var message =
                    $"{exception.Message}{Environment.NewLine}Error Details: GetBooksAsync Failed. {Environment.NewLine}{exception.StackTrace}";
                throw new Exception(message);
            }
        }

        /// <summary>
        /// Gets a book based on the book identifier.
        /// </summary>
        /// <param name="bookId">Book identifier.</param>
        /// <returns>Book details.</returns>
        public async Task<Book> GetBookAsync(Guid bookId)
        {
            try
            {
                return await bookStoreContext.Books.FirstOrDefaultAsync(book => book.Id == bookId);
            }
            catch (Exception exception)
            {
                var message =
                    $"{exception.Message}{Environment.NewLine}Error Details: GetBookAsync Failed. {Environment.NewLine}{exception.StackTrace}";
                throw new Exception(message);
            }
        }

        /// <summary>
        /// Update the book based on the book identifier.
        /// </summary>
        /// <param name="bookDetails">Details of the book to be updated.</param>
        /// <returns>True if the update is successful else false.</returns>
        public async Task<bool> UpdateBookDetailsAsync(Book bookDetails)
        {
            try
            {
                bookStoreContext.Books.Update(bookDetails);
                await bookStoreContext.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                var message =
                    $"{exception.Message}{Environment.NewLine}Error Details: UpdateBookDetailsAsync Failed. {Environment.NewLine}{exception.StackTrace}";
                throw new Exception(message);
            }
        }
    }
}
