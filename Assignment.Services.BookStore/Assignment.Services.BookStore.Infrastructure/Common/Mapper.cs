// **************************************************************************
// <copyright file="Mapper.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.Common
{
    using Assignment.Services.BookStore.Infrastructure.Entities;
    using Assignment.Services.BookStore.Infrastructure.Models;
    using AutoMapper;

    /// <summary>
    /// Mapping profile to deal with the object mapping.
    /// </summary>
    public class Mapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mapper"/> class.
        /// </summary>
        public Mapper()
        {
            CreateMap<Book, BookDetails>().ReverseMap();
        }
    }
}
