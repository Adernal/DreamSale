using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Model.Catalog;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Manufacturers
{
    public partial interface IManufacturerService
    {
        #region Manufacturer
        IPagedList<Manufacturer> GetAllManufacturers(string manufacturerName = "", int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        Manufacturer GetManufacturerById(int manufacturerId);

        void InsertManufacturer(Manufacturer manufacturer);

        void UpdateManufacturer(Manufacturer manufacturer);
        void DeleteManufacturer(Manufacturer manufacturer);

        #endregion

        #region Product Manufacturer
        IPagedList<ProductManufacturer> GetProductManufacturersByManufacturerId(int manufacturerId, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        IList<ProductManufacturer> GetProductManufacturersByProductId(int productId, bool showHidden = false);

        ProductManufacturer GetProductManufacturerById(int productManufacturerId);

        IDictionary<int, int[]> GetProductManufacturerIds(int[] productIds);

        string[] GetNotExistingManufacturers(string[] manufacturerNames);

        void InsertProductManufacturer(ProductManufacturer productManufacturer);

        void UpdateProductManufacturer(ProductManufacturer productManufacturer);
        void DeleteProductManufacturer(ProductManufacturer productManufacturer);
#endregion
    }
}
