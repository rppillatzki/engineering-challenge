// -----------------------------------------------------------------------
// <copyright file="DataFactoryServiceOptions.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Options;

namespace FoodTruck.WebApi.Options
{
    /// <summary>
    /// The DataFactoryService Options.
    /// </summary>
    public class DataFactoryServiceOptions
    {
        /// <summary>
        /// Gets or sets the FileName value.
        /// </summary>
        public string FileName { get; set; }
    }
}
