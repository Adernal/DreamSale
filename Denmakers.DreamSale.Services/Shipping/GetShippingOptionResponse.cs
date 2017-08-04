using Denmakers.DreamSale.Model.Shipping;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Shipping
{
    public partial class GetShippingOptionResponse
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public GetShippingOptionResponse()
        {
            this.Errors = new List<string>();
            this.ShippingOptions = new List<ShippingOption>();
        }

        /// <summary>
        /// Gets or sets a list of shipping options
        /// </summary>
        public IList<ShippingOption> ShippingOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shipping is done from multiple locations (warehouses)
        /// </summary>
        public bool ShippingFromMultipleLocations { get; set; }

        /// <summary>
        /// Gets or sets errors
        /// </summary>
        public IList<string> Errors { get; set; }

        /// <summary>
        /// Gets a value indicating whether request has been completed successfully
        /// </summary>
        public bool Success
        {
            get
            {
                return !this.Errors.Any();
            }
        }

        /// <summary>
        /// Add error
        /// </summary>
        /// <param name="error">Error</param>
        public void AddError(string error)
        {
            this.Errors.Add(error);
        }
    }
}
