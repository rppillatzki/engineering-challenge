// -----------------------------------------------------------------------
// <copyright file="LocationModel.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using FoodTruck.WebApi.Constants;

namespace FoodTruck.WebApi.Models
{
    /// <summary>
    /// The Location Model.
    /// </summary>
    public class LocationModel
    {
        /// <summary>
        /// Gets or sets the Latitude value.
        /// </summary>
        [StringLength(ValidationConstants.CoodinateMaxStringLength)]
        [RegularExpression(ValidationConstants.CoordinateRegexPattern)]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the Longitude value.
        /// </summary>
        [StringLength(ValidationConstants.CoodinateMaxStringLength)]
        [RegularExpression(ValidationConstants.CoordinateRegexPattern)]
        public string Longitude { get; set; }
    }
}
