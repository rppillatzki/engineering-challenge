// -----------------------------------------------------------------------
// <copyright file="ValidationConstants.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace FoodTruck.WebApi.Constants
{
    /// <summary>
    /// Validation Constant Values.
    /// </summary>
    public class ValidationConstants
    {
        /// <summary>
        /// Max Location Id value.
        /// </summary>
        public const long MaxLocationId = 1000000000;

        /// <summary>
        /// Min Location Id value.
        /// </summary>
        public const long MinLocationId = 1;

        /// <summary>
        /// Coordinate Regex Pattern value.
        /// </summary>
        public const string CoordinateRegexPattern = @"^((\-?|\+?)?\d+(\.\d+)?)$";

        /// <summary>
        /// Coordinate max string length value.
        /// </summary>
        public const int CoodinateMaxStringLength = 20;

        /// <summary>
        /// Max input string length value.
        /// </summary>
        public const int MaxStringLength = 2048;
    }
}
