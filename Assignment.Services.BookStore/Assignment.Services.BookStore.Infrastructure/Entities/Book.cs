// **************************************************************************
// <copyright file="Book.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entity which represents book resource.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the identifier for the key.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the book.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the author name of the book.
        /// </summary>
        public string AuthorName { get; set; }
    }
}
