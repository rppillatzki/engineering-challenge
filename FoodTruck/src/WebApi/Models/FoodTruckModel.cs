// -----------------------------------------------------------------------
// <copyright file="FoodTruckModel.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using FoodTruck.WebApi.Constants;
using Newtonsoft.Json;

namespace FoodTruck.WebApi.Models
{
    /// <summary>
    /// The Food Truck Model.
    /// </summary>
    public class FoodTruckModel
    {
        /// <summary>
        /// Gets or sets the LocationId value.
        /// </summary>
        [Required]
        [Range(ValidationConstants.MinLocationId, ValidationConstants.MaxLocationId)]
        public virtual long LocationId { get; set; }

        /// <summary>
        /// Gets or sets the Applicant value.
        /// </summary>
        [Required]
        [StringLength(ValidationConstants.MaxStringLength)]
        public string Applicant { get; set; }

        /// <summary>
        /// Gets or sets the FacilityType value.
        /// </summary>
        [StringLength(50)]
        public string FacilityType { get; set; }

        /// <summary>
        /// Gets or sets the LocationDescription value.
        /// </summary>
        [StringLength(ValidationConstants.MaxStringLength)]
        public string LocationDescription { get; set; }

        /// <summary>
        /// Gets or sets the Address value.
        /// </summary>
        [StringLength(ValidationConstants.MaxStringLength)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the BlockLot value.
        /// </summary>
        [StringLength(10)]
        public string BlockLot { get; set; }

        /// <summary>
        /// Gets or sets the Block value.
        /// </summary>
        [StringLength(6)]
        public string Block { get; set; }

        /// <summary>
        /// Gets or sets the Lot value.
        /// </summary>
        [StringLength(4)]
        public string Lot { get; set; }

        /// <summary>
        /// Gets or sets the Permit value.
        /// </summary>
        [StringLength(11)]
        public string Permit { get; set; }

        /// <summary>
        /// Gets or sets the Status value.
        /// </summary>
        [StringLength(50)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the FoodItems value.
        /// </summary>
        [StringLength(ValidationConstants.MaxStringLength)]
        public string FoodItems { get; set; }

        /// <summary>
        /// Gets or sets the X value.
        /// </summary>
        [StringLength(ValidationConstants.CoodinateMaxStringLength)]
        public string X { get; set; }

        /// <summary>
        /// Gets or sets the Y value.
        /// </summary>
        [StringLength(ValidationConstants.CoodinateMaxStringLength)]
        public string Y { get; set; }

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

        /// <summary>
        /// Gets or sets the Schedule value.
        /// </summary>
        public Uri Schedule { get; set; }

        /// <summary>
        /// Gets or sets the Approved value.
        /// </summary>
        public DateTime Approved { get; set; }

        /// <summary>
        /// Gets or sets the Received value.
        /// </summary>
        [StringLength(25)]
        public string Received { get; set; }

        /// <summary>
        /// Gets or sets the PriorPermit value.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int PriorPermit { get; set; }

        /// <summary>
        /// Gets or sets the ExpirationDate value.
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the Location value.
        /// </summary>
        public LocationModel Location { get; set; }
    }
}
