// **************************************************************************
// <copyright file="BookStoreContext.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.DataContext
{
    using Assignment.Services.BookStore.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Data context class to deal with the SQL Database.
    /// </summary>
    public class BookStoreContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreContext"/> class.
        /// </summary>
        /// <param name="bookStoreContextOptions">BookStore data context options.</param>
        public BookStoreContext(DbContextOptions bookStoreContextOptions)
            : base(bookStoreContextOptions)
        {
        }

        /// <summary>
        /// Gets or sets book DbSet to be used for query and saving the books
        /// </summary>
        public DbSet<Book> Books { get; set; }
    }
}
