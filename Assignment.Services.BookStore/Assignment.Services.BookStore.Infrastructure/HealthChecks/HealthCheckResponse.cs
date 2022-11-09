// **************************************************************************
// <copyright file="HealthCheckResponse.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.HealthChecks
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Response class for the SQL Database health check.
    /// </summary>
    public class HealthCheckResponse
    {
        /// <summary>
        /// Gets or sets the health check status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the list of components health.
        /// </summary>
        public IEnumerable<HealthCheck> HealthChecks { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public TimeSpan Duration { get; set; }
    }
}
