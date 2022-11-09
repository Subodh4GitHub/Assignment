// **************************************************************************
// <copyright file="IBookManager.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.Managers.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Assignment.Services.BookStore.Infrastructure.Models;

    /// <summary>
    /// Contract for the book manager class.
    /// </summary>
    public interface IBookManager
    {
        /// <summary>
        /// Add a new book to the book store repository.
        /// </summary>
        /// <param name="bookDetails">Details of the book to be added.</param>
        /// <returns>Book identifier.</returns>
        Task<Guid> AddBookAsync(BookDetails bookDetails);

        /// <summary>
        /// Gets the list of books from the book store repository.
        /// </summary>
        /// <returns>List of book details.</returns>
        Task<List<BookDetails>> GetBooksAsync();

        /// <summary>
        /// Gets a book based on the book identifier.
        /// </summary>
        /// <param name="bookId">Book identifier.</param>
        /// <returns>Book details.</returns>
        Task<BookDetails> GetBookAsync(Guid bookId);

        /// <summary>
        /// Update the book based on the book identifier.
        /// </summary>
        /// <param name="bookId">Identifier for the book.</param>
        /// <param name="bookDetails">Details of the book to be updated.</param>
        /// <returns>True if the update is successful else false.</returns>
        Task<bool> UpdateBookDetailsAsync(Guid bookId, BookDetails bookDetails);
    }
}
