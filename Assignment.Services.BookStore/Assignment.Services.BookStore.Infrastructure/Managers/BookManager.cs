// **************************************************************************
// <copyright file="BookManager.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Assignment.Services.BookStore.Infrastructure.Entities;
    using Assignment.Services.BookStore.Infrastructure.Managers.Contracts;
    using Assignment.Services.BookStore.Infrastructure.Models;
    using Assignment.Services.BookStore.Infrastructure.Repositories.Contracts;
    using AutoMapper;

    /// <summary>
    /// Class to deal with the business logic for the book resource.
    /// </summary>
    public class BookManager : IBookManager
    {
        /// <summary>
        /// Represents object to deal with the book repository.
        /// </summary>
        private readonly IBookRepository bookRepository;

        /// <summary>
        /// Represent objects to deal with the object mapping.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookManager"/> class.
        /// </summary>
        /// <param name="bookRepository">Represents object to deal with the book repository.</param>
        /// <param name="mapper">Represent objects to deal with the object mapping.</param>
        public BookManager(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Add a new book to the book store repository.
        /// </summary>
        /// <param name="bookDetails">Details of the book to be added.</param>
        /// <returns>Book identifier.</returns>
        public async Task<Guid> AddBookAsync(BookDetails bookDetails)
        {
            var book = mapper.Map<Book>(bookDetails);
            book.Id = Guid.NewGuid();
            return await bookRepository.AddBookAsync(book);
        }

        /// <summary>
        /// Gets the list of books from the book store repository.
        /// </summary>
        /// <returns>List of book details.</returns>
        public async Task<List<BookDetails>> GetBooksAsync()
        {
            var books = await bookRepository.GetBooksAsync();
            return mapper.Map<List<BookDetails>>(books);
        }

        /// <summary>
        /// Gets a book based on the book identifier.
        /// </summary>
        /// <param name="bookId">Book identifier.</param>
        /// <returns>Book details.</returns>
        public async Task<BookDetails> GetBookAsync(Guid bookId)
        {
            var book = await bookRepository.GetBookAsync(bookId);
            return mapper.Map<BookDetails>(book);
        }

        /// <summary>
        /// Update the book based on the book identifier.
        /// </summary>
        /// <param name="bookId">Identifier for the book.</param>
        /// <param name="bookDetails">Details of the book to be updated.</param>
        /// <returns>True if the update is successful else false.</returns>
        public async Task<bool> UpdateBookDetailsAsync(Guid bookId, BookDetails bookDetails)
        {
            var book = mapper.Map<Book>(bookDetails);
            book.Id = bookId;
            return await bookRepository.UpdateBookDetailsAsync(book);
        }
    }
}
