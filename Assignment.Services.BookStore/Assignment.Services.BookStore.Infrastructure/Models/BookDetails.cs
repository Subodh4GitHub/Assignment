// **************************************************************************
// <copyright file="BookDetails.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model that represents book details.
    /// </summary>
    public class BookDetails
    {
        /// <summary>
        /// Gets or sets the Book identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the book.
        /// </summary>
        [Required(ErrorMessage = "Name is mandatory field.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the author name of the book.
        /// </summary>
        [Required(ErrorMessage = "Please provide the name of author.")]
        public string AuthorName { get; set; }
    }
}
