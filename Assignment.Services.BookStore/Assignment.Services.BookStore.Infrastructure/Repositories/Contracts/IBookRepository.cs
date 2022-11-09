// **************************************************************************
// <copyright file="IBookRepository.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.Repositories.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Assignment.Services.BookStore.Infrastructure.Entities;

    /// <summary>
    /// Contract for the book repository.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Add a new book to the book store repository.
        /// </summary>
        /// <param name="bookDetails">Details of the book to be added.</param>
        /// <returns>Book identifier.</returns>
        Task<Guid> AddBookAsync(Book bookDetails);

        /// <summary>
        /// Gets the list of books from the book store repository.
        /// </summary>
        /// <returns>List of book details.</returns>
        Task<List<Book>> GetBooksAsync();

        /// <summary>
        /// Gets a book based on the book identifier.
        /// </summary>
        /// <param name="bookId">Book identifier.</param>
        /// <returns>Book details.</returns>
        Task<Book> GetBookAsync(Guid bookId);

        /// <summary>
        /// Update the book based on the book identifier.
        /// </summary>
        /// <param name="bookDetails">Details of the book to be updated.</param>
        /// <returns>True if the update is successful else false.</returns>
        Task<bool> UpdateBookDetailsAsync(Book bookDetails);
    }
}
