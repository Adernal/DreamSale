using Denmakers.DreamSale.Services.Categories;
using Denmakers.DreamSale.Services.Manufacturers;
using Denmakers.DreamSale.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Denmakers.DreamSale.Services.Helpers
{
    public static class SelectListHelper
    {
        /// <summary>
        /// Get category list
        /// </summary>
        /// <param name="categoryService">Category service</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category list</returns>
        public static List<SelectListItem> GetCategoryList(ICategoryService categoryService, bool showHidden = false)
        {
            if (categoryService == null)
                throw new ArgumentNullException("categoryService");

            var categories = categoryService.GetAllCategories(showHidden: showHidden);
            var listItems = categories.Select(c => new SelectListItem
            {
                Text = c.GetFormattedBreadCrumb(categories),
                Value = c.Id.ToString()
            });
            

            var result = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in listItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            return result;
        }

        /// <summary>
        /// Get manufacturer list
        /// </summary>
        /// <param name="manufacturerService">Manufacturer service</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Manufacturer list</returns>
        public static List<SelectListItem> GetManufacturerList(IManufacturerService manufacturerService, bool showHidden = false)
        {
            if (manufacturerService == null)
                throw new ArgumentNullException("manufacturerService");

            var manufacturers = manufacturerService.GetAllManufacturers(showHidden: showHidden);
            var listItems = manufacturers.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            });

            var result = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in listItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            return result;
        }

        /// <summary>
        /// Get vendor list
        /// </summary>
        /// <param name="vendorService">Vendor service</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Vendor list</returns>
        public static List<SelectListItem> GetVendorList(IVendorService vendorService, bool showHidden = false)
        {
            if (vendorService == null)
                throw new ArgumentNullException("vendorService");

            var vendors = vendorService.GetAllVendors(showHidden: showHidden);
            var listItems = vendors.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });

            var result = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in listItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            return result;
        }
    }
}
