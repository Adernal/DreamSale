using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.Services.Attributes;
using Denmakers.DreamSale.Services.Categories;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.ExportImport.Help;
using Denmakers.DreamSale.Services.Manufacturers;
using Denmakers.DreamSale.Services.Media;
using Denmakers.DreamSale.Services.Products;
using Denmakers.DreamSale.Services.Stores;
using Denmakers.DreamSale.Services.Vendors;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Denmakers.DreamSale.Services.ExportImport
{
    public partial class ExportManager : IExportManager
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ICustomerService _customerService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IPictureService _pictureService;
        //private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;
        private readonly ProductEditorSettings _productEditorSettings;
        private readonly IVendorService _vendorService;
        private readonly IProductTemplateService _productTemplateService;
        //private readonly IDateRangeService _dateRangeService;
        //private readonly ITaxCategoryService _taxCategoryService;
        //private readonly IMeasureService _measureService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IGenericAttributeService _genericAttributeService;
        //private readonly ICustomerAttributeFormatter _customerAttributeFormatter;
        private readonly OrderSettings _orderSettings;
        private readonly ISettingService _settingService;
        #endregion

        #region Ctor

        public ExportManager(ICategoryService categoryService,
            IManufacturerService manufacturerService,
            ICustomerService customerService,
            IProductAttributeService productAttributeService,
            IPictureService pictureService,
            //INewsLetterSubscriptionService newsLetterSubscriptionService,
            IStoreService storeService,
            IWorkContext workContext,
            ProductEditorSettings productEditorSettings,
            IVendorService vendorService,
            IProductTemplateService productTemplateService,
            //IDateRangeService dateRangeService,
            //ITaxCategoryService taxCategoryService,
            //IMeasureService measureService,
            IGenericAttributeService genericAttributeService,
            //ICustomerAttributeFormatter customerAttributeFormatter,
            ISettingService settingService)
        {
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._customerService = customerService;
            this._productAttributeService = productAttributeService;
            this._pictureService = pictureService;
            //this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._storeService = storeService;
            this._workContext = workContext;
            this._productEditorSettings = productEditorSettings;
            this._vendorService = vendorService;
            this._productTemplateService = productTemplateService;
            //this._dateRangeService = dateRangeService;
            //this._taxCategoryService = taxCategoryService;
            //this._measureService = measureService;
            this._settingService = settingService;
            this._genericAttributeService = genericAttributeService;
            //this._customerAttributeFormatter = customerAttributeFormatter;
            this._catalogSettings = _settingService.LoadSetting<CatalogSettings>();
            this._orderSettings = _settingService.LoadSetting<OrderSettings>();
        }

        #endregion

        #region Utilities

        protected virtual void WriteCategories(XmlWriter xmlWriter, int parentCategoryId)
        {
            var categories = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId, true);
            if (categories != null && categories.Any())
            {
                foreach (var category in categories)
                {
                    xmlWriter.WriteStartElement("Category");

                    xmlWriter.WriteString("Id", category.Id);

                    xmlWriter.WriteString("Name", category.Name);
                    xmlWriter.WriteString("Description", category.Description);
                    xmlWriter.WriteString("CategoryTemplateId", category.CategoryTemplateId);
                    xmlWriter.WriteString("MetaKeywords", category.MetaKeywords, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("MetaDescription", category.MetaDescription, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("MetaTitle", category.MetaTitle, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("SeName", category.GetSeName(0), IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("ParentCategoryId", category.ParentCategoryId);
                    xmlWriter.WriteString("PictureId", category.PictureId);
                    xmlWriter.WriteString("PageSize", category.PageSize, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("AllowCustomersToSelectPageSize", category.AllowCustomersToSelectPageSize, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("PageSizeOptions", category.PageSizeOptions, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("PriceRanges", category.PriceRanges, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("ShowOnHomePage", category.ShowOnHomePage, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("IncludeInTopMenu", category.IncludeInTopMenu, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("Published", category.Published, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("Deleted", category.Deleted, true);
                    xmlWriter.WriteString("DisplayOrder", category.DisplayOrder);
                    xmlWriter.WriteString("CreatedOnUtc", category.CreatedOnUtc, IgnoreExportCategoryProperty());
                    xmlWriter.WriteString("UpdatedOnUtc", category.UpdatedOnUtc, IgnoreExportCategoryProperty());

                    xmlWriter.WriteStartElement("Products");
                    var productCategories = _categoryService.GetProductCategoriesByCategoryId(category.Id, showHidden: true);
                    foreach (var productCategory in productCategories)
                    {
                        var product = productCategory.Product;
                        if (product != null && !product.Deleted)
                        {
                            xmlWriter.WriteStartElement("ProductCategory");
                            xmlWriter.WriteString("ProductCategoryId", productCategory.Id);
                            xmlWriter.WriteString("ProductId", productCategory.ProductId);
                            xmlWriter.WriteString("ProductName", product.Name);
                            xmlWriter.WriteString("IsFeaturedProduct", productCategory.IsFeaturedProduct);
                            xmlWriter.WriteString("DisplayOrder", productCategory.DisplayOrder);
                            xmlWriter.WriteEndElement();
                        }
                    }
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("SubCategories");
                    WriteCategories(xmlWriter, category.Id);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
            }
        }

        protected virtual void SetCaptionStyle(ExcelStyle style)
        {
            style.Fill.PatternType = ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            style.Font.Bold = true;
        }

        /// <summary>
        /// Returns the path to the image file by ID
        /// </summary>
        /// <param name="pictureId">Picture ID</param>
        /// <returns>Path to the image file</returns>
        protected virtual string GetPictures(int pictureId)
        {
            var picture = _pictureService.GetPictureById(pictureId);
            return _pictureService.GetThumbLocalPath(picture);
        }

        /// <summary>
        /// Returns the list of categories for a product separated by a ";"
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>List of categories</returns>
        protected virtual string GetCategories(Product product)
        {
            string categoryNames = null;
            foreach (var pc in _categoryService.GetProductCategoriesByProductId(product.Id, true))
            {
                categoryNames += pc.Category.Name;
                categoryNames += ";";
            }
            return categoryNames;
        }

        /// <summary>
        /// Returns the list of manufacturer for a product separated by a ";"
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>List of manufacturer</returns>
        protected virtual string GetManufacturers(Product product)
        {
            string manufacturerNames = null;
            foreach (var pm in _manufacturerService.GetProductManufacturersByProductId(product.Id, true))
            {
                manufacturerNames += pm.Manufacturer.Name;
                manufacturerNames += ";";
            }
            return manufacturerNames;
        }

        /// <summary>
        /// Returns the list of product tag for a product separated by a ";"
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>List of product tag</returns>
        protected virtual string GetProductTags(Product product)
        {
            string productTagNames = null;

            foreach (var productTag in product.ProductTags)
            {
                productTagNames += productTag.Name;
                productTagNames += ";";
            }
            return productTagNames;
        }

        /// <summary>
        /// Returns the three first image associated with the product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>three first image</returns>
        protected virtual string[] GetPictures(Product product)
        {
            //pictures (up to 3 pictures)
            string picture1 = null;
            string picture2 = null;
            string picture3 = null;
            var pictures = _pictureService.GetPicturesByProductId(product.Id, 3);
            for (var i = 0; i < pictures.Count; i++)
            {
                var pictureLocalPath = _pictureService.GetThumbLocalPath(pictures[i]);
                switch (i)
                {
                    case 0:
                        picture1 = pictureLocalPath;
                        break;
                    case 1:
                        picture2 = pictureLocalPath;
                        break;
                    case 2:
                        picture3 = pictureLocalPath;
                        break;
                }
            }
            return new[] { picture1, picture2, picture3 };
        }

        private bool IgnoreExportPoductProperty(Func<ProductEditorSettings, bool> func)
        {
            var productAdvancedMode = _workContext.CurrentCustomer.GetAttribute<bool>("product-advanced-mode");
            return !productAdvancedMode && !func(_productEditorSettings);
        }

        private bool IgnoreExportCategoryProperty()
        {
            return !_workContext.CurrentCustomer.GetAttribute<bool>("category-advanced-mode");
        }

        private bool IgnoreExportManufacturerProperty()
        {
            return !_workContext.CurrentCustomer.GetAttribute<bool>("manufacturer-advanced-mode");
        }

        /// <summary>
        /// Export objects to XLSX
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="properties">Class access to the object through its properties</param>
        /// <param name="itemsToExport">The objects to export</param>
        /// <returns></returns>
        protected virtual byte[] ExportToXlsx<T>(PropertyByName<T>[] properties, IEnumerable<T> itemsToExport)
        {
            using (var stream = new MemoryStream())
            {
                // ok, we can run the real code of the sample now
                using (var xlPackage = new ExcelPackage(stream))
                {
                    // uncomment this line if you want the XML written out to the outputDir
                    //xlPackage.DebugMode = true; 

                    // get handles to the worksheets
                    var worksheet = xlPackage.Workbook.Worksheets.Add(typeof(T).Name);
                    var fWorksheet = xlPackage.Workbook.Worksheets.Add("DataForFilters");
                    fWorksheet.Hidden = eWorkSheetHidden.VeryHidden;

                    //create Headers and format them 
                    var manager = new PropertyManager<T>(properties.Where(p => !p.Ignore));
                    manager.WriteCaption(worksheet, SetCaptionStyle);

                    var row = 2;
                    foreach (var items in itemsToExport)
                    {
                        manager.CurrentObject = items;
                        manager.WriteToXlsx(worksheet, row++, _catalogSettings.ExportImportUseDropdownlistsForAssociatedEntities, fWorksheet: fWorksheet);
                    }

                    xlPackage.Save();
                }
                return stream.ToArray();
            }
        }

        private byte[] ExportProductsToXlsxWithAttributes(PropertyByName<Product>[] properties, IEnumerable<Product> itemsToExport)
        {
            var attributeProperties = new[]
            {
                new PropertyByName<ExportProductAttribute>("AttributeId", p => p.AttributeId),
                new PropertyByName<ExportProductAttribute>("AttributeName", p => p.AttributeName),
                new PropertyByName<ExportProductAttribute>("AttributeTextPrompt", p => p.AttributeTextPrompt),
                new PropertyByName<ExportProductAttribute>("AttributeIsRequired", p => p.AttributeIsRequired),
                new PropertyByName<ExportProductAttribute>("AttributeControlType", p => p.AttributeControlTypeId)
                {
                    //DropDownElements = AttributeControlType.TextBox.ToSelectList(useLocalization: false)
                },
                new PropertyByName<ExportProductAttribute>("AttributeDisplayOrder", p => p.AttributeDisplayOrder),
                new PropertyByName<ExportProductAttribute>("ProductAttributeValueId", p => p.Id),
                new PropertyByName<ExportProductAttribute>("ValueName", p => p.Name),
                new PropertyByName<ExportProductAttribute>("AttributeValueType", p => p.AttributeValueTypeId)
                {
                    //DropDownElements = AttributeValueType.Simple.ToSelectList(useLocalization: false)
                },
                new PropertyByName<ExportProductAttribute>("AssociatedProductId", p => p.AssociatedProductId),
                new PropertyByName<ExportProductAttribute>("ColorSquaresRgb", p => p.ColorSquaresRgb),
                new PropertyByName<ExportProductAttribute>("ImageSquaresPictureId", p => p.ImageSquaresPictureId),
                new PropertyByName<ExportProductAttribute>("PriceAdjustment", p => p.PriceAdjustment),
                new PropertyByName<ExportProductAttribute>("WeightAdjustment", p => p.WeightAdjustment),
                new PropertyByName<ExportProductAttribute>("Cost", p => p.Cost),
                new PropertyByName<ExportProductAttribute>("CustomerEntersQty", p => p.CustomerEntersQty),
                new PropertyByName<ExportProductAttribute>("Quantity", p => p.Quantity),
                new PropertyByName<ExportProductAttribute>("IsPreSelected", p => p.IsPreSelected),
                new PropertyByName<ExportProductAttribute>("DisplayOrder", p => p.DisplayOrder),
                new PropertyByName<ExportProductAttribute>("PictureId", p => p.PictureId)
            };

            var attributeManager = new PropertyManager<ExportProductAttribute>(attributeProperties);

            using (var stream = new MemoryStream())
            {
                // ok, we can run the real code of the sample now
                using (var xlPackage = new ExcelPackage(stream))
                {
                    // uncomment this line if you want the XML written out to the outputDir
                    //xlPackage.DebugMode = true; 

                    // get handles to the worksheets
                    var worksheet = xlPackage.Workbook.Worksheets.Add(typeof(Product).Name);
                    var fpWorksheet = xlPackage.Workbook.Worksheets.Add("DataForProductsFilters");
                    fpWorksheet.Hidden = eWorkSheetHidden.VeryHidden;
                    var faWorksheet = xlPackage.Workbook.Worksheets.Add("DataForProductAttributesFilters");
                    faWorksheet.Hidden = eWorkSheetHidden.VeryHidden;

                    //create Headers and format them 
                    var manager = new PropertyManager<Product>(properties.Where(p => !p.Ignore));
                    manager.WriteCaption(worksheet, SetCaptionStyle);

                    var row = 2;
                    foreach (var item in itemsToExport)
                    {
                        manager.CurrentObject = item;
                        manager.WriteToXlsx(worksheet, row++, _catalogSettings.ExportImportUseDropdownlistsForAssociatedEntities, fWorksheet: fpWorksheet);

                        var attributes = item.ProductAttributeMappings.SelectMany(pam => pam.ProductAttributeValues.Select(pav => new ExportProductAttribute
                        {
                            AttributeId = pam.ProductAttribute.Id,
                            AttributeName = pam.ProductAttribute.Name,
                            AttributeTextPrompt = pam.TextPrompt,
                            AttributeIsRequired = pam.IsRequired,
                            AttributeControlTypeId = pam.AttributeControlTypeId,
                            AssociatedProductId = pav.AssociatedProductId,
                            AttributeDisplayOrder = pam.DisplayOrder,
                            Id = pav.Id,
                            Name = pav.Name,
                            AttributeValueTypeId = pav.AttributeValueTypeId,
                            ColorSquaresRgb = pav.ColorSquaresRgb,
                            ImageSquaresPictureId = pav.ImageSquaresPictureId,
                            PriceAdjustment = pav.PriceAdjustment,
                            WeightAdjustment = pav.WeightAdjustment,
                            Cost = pav.Cost,
                            CustomerEntersQty = pav.CustomerEntersQty,
                            Quantity = pav.Quantity,
                            IsPreSelected = pav.IsPreSelected,
                            DisplayOrder = pav.DisplayOrder,
                            PictureId = pav.PictureId
                        })).ToList();

                        attributes.AddRange(item.ProductAttributeMappings.Where(pam => !pam.ProductAttributeValues.Any()).Select(pam => new ExportProductAttribute
                        {
                            AttributeId = pam.ProductAttribute.Id,
                            AttributeName = pam.ProductAttribute.Name,
                            AttributeTextPrompt = pam.TextPrompt,
                            AttributeIsRequired = pam.IsRequired,
                            AttributeControlTypeId = pam.AttributeControlTypeId
                        }));

                        if (!attributes.Any())
                            continue;

                        attributeManager.WriteCaption(worksheet, SetCaptionStyle, row, ExportProductAttribute.ProducAttributeCellOffset);
                        worksheet.Row(row).OutlineLevel = 1;
                        worksheet.Row(row).Collapsed = true;

                        foreach (var exportProducAttribute in attributes)
                        {
                            row++;
                            attributeManager.CurrentObject = exportProducAttribute;
                            attributeManager.WriteToXlsx(worksheet, row, _catalogSettings.ExportImportUseDropdownlistsForAssociatedEntities, ExportProductAttribute.ProducAttributeCellOffset, faWorksheet);
                            worksheet.Row(row).OutlineLevel = 1;
                            worksheet.Row(row).Collapsed = true;
                        }

                        row++;
                    }

                    xlPackage.Save();
                }
                return stream.ToArray();
            }
        }

        //private byte[] ExportOrderToXlsxWithProducts(PropertyByName<Order>[] properties, IEnumerable<Order> itemsToExport)
        //{
        //    var orderItemProperties = new[]
        //    {
        //        new PropertyByName<OrderItem>("Name", oi => oi.Product.Name),
        //        new PropertyByName<OrderItem>("Sku", oi => oi.Product.Sku),
        //        new PropertyByName<OrderItem>("PriceExclTax", oi => oi.UnitPriceExclTax),
        //        new PropertyByName<OrderItem>("PriceInclTax", oi => oi.UnitPriceInclTax),
        //        new PropertyByName<OrderItem>("Quantity", oi => oi.Quantity),
        //        new PropertyByName<OrderItem>("DiscountExclTax", oi => oi.DiscountAmountExclTax),
        //        new PropertyByName<OrderItem>("DiscountInclTax", oi => oi.DiscountAmountInclTax),
        //        new PropertyByName<OrderItem>("TotalExclTax", oi => oi.PriceExclTax),
        //        new PropertyByName<OrderItem>("TotalInclTax", oi => oi.PriceInclTax)
        //    };

        //    var orderItemsManager = new PropertyManager<OrderItem>(orderItemProperties);

        //    using (var stream = new MemoryStream())
        //    {
        //        // ok, we can run the real code of the sample now
        //        using (var xlPackage = new ExcelPackage(stream))
        //        {
        //            // uncomment this line if you want the XML written out to the outputDir
        //            //xlPackage.DebugMode = true; 

        //            // get handles to the worksheets
        //            var worksheet = xlPackage.Workbook.Worksheets.Add(typeof(Order).Name);
        //            var fpWorksheet = xlPackage.Workbook.Worksheets.Add("DataForProductsFilters");
        //            fpWorksheet.Hidden = eWorkSheetHidden.VeryHidden;

        //            //create Headers and format them 
        //            var manager = new PropertyManager<Order>(properties.Where(p => !p.Ignore));
        //            manager.WriteCaption(worksheet, SetCaptionStyle);

        //            var row = 2;
        //            foreach (var order in itemsToExport)
        //            {
        //                manager.CurrentObject = order;
        //                manager.WriteToXlsx(worksheet, row++, _catalogSettings.ExportImportUseDropdownlistsForAssociatedEntities);

        //                //products
        //                var orederItems = order.OrderItems.ToList();

        //                //a vendor should have access only to his products
        //                if (_workContext.CurrentVendor != null)
        //                    orederItems = orederItems.Where(p => p.Product.VendorId == _workContext.CurrentVendor.Id).ToList();

        //                if (!orederItems.Any())
        //                    continue;

        //                orderItemsManager.WriteCaption(worksheet, SetCaptionStyle, row, 2);
        //                worksheet.Row(row).OutlineLevel = 1;
        //                worksheet.Row(row).Collapsed = true;

        //                foreach (var orederItem in orederItems)
        //                {
        //                    row++;
        //                    orderItemsManager.CurrentObject = orederItem;
        //                    orderItemsManager.WriteToXlsx(worksheet, row, _catalogSettings.ExportImportUseDropdownlistsForAssociatedEntities, 2, fpWorksheet);
        //                    worksheet.Row(row).OutlineLevel = 1;
        //                    worksheet.Row(row).Collapsed = true;
        //                }

        //                row++;
        //            }

        //            xlPackage.Save();
        //        }
        //        return stream.ToArray();
        //    }
        //}

        private string GetCustomCustomerAttributes(Customer customer)
        {
            //var selectedCustomerAttributes = customer.GetAttribute<string>(SystemCustomerAttributeNames.CustomCustomerAttributes, _genericAttributeService);
            //return _customerAttributeFormatter.FormatAttributes(selectedCustomerAttributes, ";");
            return "";
        }

        #endregion

        #region Manufacturer
        /// <summary>
        /// Export manufacturer list to xml
        /// </summary>
        /// <param name="manufacturers">Manufacturers</param>
        /// <returns>Result in XML format</returns>
        public string ExportManufacturersToXml(IList<Manufacturer> manufacturers)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Export manufacturers to XLSX
        /// </summary>
        /// <param name="manufacturers">Manufactures</param>
        public byte[] ExportManufacturersToXlsx(IEnumerable<Manufacturer> manufacturers)
        {
            //property array
            var properties = new[]
            {
                new PropertyByName<Manufacturer>("Id", p => p.Id),
                new PropertyByName<Manufacturer>("Name", p => p.Name),
                new PropertyByName<Manufacturer>("Description", p => p.Description),
                new PropertyByName<Manufacturer>("ManufacturerTemplateId", p => p.ManufacturerTemplateId),
                new PropertyByName<Manufacturer>("MetaKeywords", p => p.MetaKeywords, IgnoreExportManufacturerProperty()),
                new PropertyByName<Manufacturer>("MetaDescription", p => p.MetaDescription, IgnoreExportManufacturerProperty()),
                new PropertyByName<Manufacturer>("MetaTitle", p => p.MetaTitle, IgnoreExportManufacturerProperty()),
                new PropertyByName<Manufacturer>("SeName", p => p.GetSeName(0), IgnoreExportManufacturerProperty()),
                new PropertyByName<Manufacturer>("Picture", p => GetPictures(p.PictureId)),
                new PropertyByName<Manufacturer>("PageSize", p => p.PageSize, IgnoreExportManufacturerProperty()),
                new PropertyByName<Manufacturer>("AllowCustomersToSelectPageSize", p => p.AllowCustomersToSelectPageSize, IgnoreExportManufacturerProperty()),
                new PropertyByName<Manufacturer>("PageSizeOptions", p => p.PageSizeOptions, IgnoreExportManufacturerProperty()),
                new PropertyByName<Manufacturer>("PriceRanges", p => p.PriceRanges, IgnoreExportManufacturerProperty()),
                new PropertyByName<Manufacturer>("Published", p => p.Published, IgnoreExportManufacturerProperty()),
                new PropertyByName<Manufacturer>("DisplayOrder", p => p.DisplayOrder)
            };

            return ExportToXlsx(properties, manufacturers);
        }

        #endregion

        #region Categories
        /// <summary>
        /// Export category list to xml
        /// </summary>
        /// <returns>Result in XML format</returns>
        public string ExportCategoriesToXml()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Export categories to XLSX
        /// </summary>
        /// <param name="categories">Categories</param>
        public byte[] ExportCategoriesToXlsx(IEnumerable<Category> categories)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Products
        /// <summary>
        /// Export product list to xml
        /// </summary>
        /// <param name="products">Products</param>
        /// <returns>Result in XML format</returns>
        public string ExportProductsToXml(IList<Product> products)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Export products to XLSX
        /// </summary>
        /// <param name="products">Products</param>
        public byte[] ExportProductsToXlsx(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Customers
        /// <summary>
        /// Export customer list to XLSX
        /// </summary>
        /// <param name="customers">Customers</param>
        public byte[] ExportCustomersToXlsx(IList<Customer> customers)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Export customer list to xml
        /// </summary>
        /// <param name="customers">Customers</param>
        /// <returns>Result in XML format</returns>
        public string ExportCustomersToXml(IList<Customer> customers)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
