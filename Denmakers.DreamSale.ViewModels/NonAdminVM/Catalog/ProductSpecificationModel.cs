namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public partial class ProductSpecificationModel
    {
        public int id { get; set; }
        /// <summary>
        /// Specificartion attribute ID
        /// </summary>
        public int SpecificationAttributeId { get; set; }

        /// <summary>
        /// Specificartion attribute name
        /// </summary>
        public string SpecificationAttributeName { get; set; }

        /// <summary>
        /// Option value
        /// this value is already HTML encoded
        /// </summary>
        public string ValueRaw { get; set; }

        /// <summary>
        /// Option color (if specified). Used to display color squares
        /// </summary>
        public string ColorSquaresRgb { get; set; }
    }
}
