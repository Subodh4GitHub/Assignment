// **************************************************************************
// <copyright file="HealthCheck.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.HealthChecks
{
    /// <summary>
    /// Represents component details for the health check.
    /// </summary>
    public class HealthCheck
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the component.
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
