// **************************************************************************
// <copyright file="ErrorsController.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.API.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Book controller to receive incoming REST API request.
    /// </summary>
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class ErrorsController : ControllerBase
    {
        /// <summary>
        /// Represents logger to deal with the error logger controller.
        /// </summary>
        private ILogger<ErrorsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorsController"/> class.
        /// </summary>
        /// <param name="logger">Application logger object.</param>
        public ErrorsController(ILogger<ErrorsController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Handles global errors.
        /// </summary>
        /// <returns>Internal server error details.</returns>
        [HttpGet]
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            logger.LogError(context.Error, context.Error.Message);

            return Problem();
        }
    }
}
