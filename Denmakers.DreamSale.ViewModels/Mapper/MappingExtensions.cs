using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.Model.Vendors;
using Denmakers.DreamSale.Services.Addresses;
using Denmakers.DreamSale.ViewModels.AdminVM.Addresses;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using Denmakers.DreamSale.ViewModels.AdminVM.Customers;
using Denmakers.DreamSale.ViewModels.AdminVM.Logging;
using Denmakers.DreamSale.ViewModels.AdminVM.Settings;
using Denmakers.DreamSale.ViewModels.AdminVM.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.ViewModels.Mapper
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        #region Category

        public static CategoryVM ToModel(this Category entity)
        {
            return entity.MapTo<Category, CategoryVM>();
        }

        public static Category ToEntity(this CategoryVM model)
        {
            return model.MapTo<CategoryVM, Category>();
        }

        public static Category ToEntity(this CategoryVM model, Category destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Manufacturer

        public static ManufacturerVM ToModel(this Manufacturer entity)
        {
            return entity.MapTo<Manufacturer, ManufacturerVM>();
        }

        public static Manufacturer ToEntity(this ManufacturerVM model)
        {
            return model.MapTo<ManufacturerVM, Manufacturer>();
        }

        public static Manufacturer ToEntity(this ManufacturerVM model, Manufacturer destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Products

        public static ProductVM ToModel(this Product entity)
        {
            return entity.MapTo<Product, ProductVM>();
        }

        public static Product ToEntity(this ProductVM model)
        {
            return model.MapTo<ProductVM, Product>();
        }

        public static Product ToEntity(this ProductVM model, Product destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Product Editor Settings
        public static ProductEditorSettingsVM ToModel(this ProductEditorSettings entity)
        {
            //var newModel = AutoMapperConfiguration.Mapper.Map(entity, typeof(ProductEditorSettings), typeof(ProductEditorSettingsVM));
            //return (ProductEditorSettingsVM)newModel;
            return entity.MapTo<ProductEditorSettings, ProductEditorSettingsVM>();
        }

        public static ProductEditorSettings ToEntity(this ProductEditorSettingsVM model)
        {
            return model.MapTo<ProductEditorSettingsVM, ProductEditorSettings>();
        }

        public static ProductEditorSettings ToEntity(this ProductEditorSettingsVM model, ProductEditorSettings destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        #region Product attributes

        public static ProductAttributeVM ToModel(this ProductAttribute entity)
        {
            return entity.MapTo<ProductAttribute, ProductAttributeVM>();
        }

        public static ProductAttribute ToEntity(this ProductAttributeVM model)
        {
            return model.MapTo<ProductAttributeVM, ProductAttribute>();
        }

        public static ProductAttribute ToEntity(this ProductAttributeVM model, ProductAttribute destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Specification attributes

        //attributes
        public static SpecificationAttributeVM ToModel(this SpecificationAttribute entity)
        {
            return entity.MapTo<SpecificationAttribute, SpecificationAttributeVM>();
        }

        public static SpecificationAttribute ToEntity(this SpecificationAttributeVM model)
        {
            return model.MapTo<SpecificationAttributeVM, SpecificationAttribute>();
        }

        public static SpecificationAttribute ToEntity(this SpecificationAttributeVM model, SpecificationAttribute destination)
        {
            return model.MapTo(destination);
        }

        //attribute options
        public static SpecificationAttributeOptionVM ToModel(this SpecificationAttributeOption entity)
        {
            return entity.MapTo<SpecificationAttributeOption, SpecificationAttributeOptionVM>();
        }

        public static SpecificationAttributeOption ToEntity(this SpecificationAttributeOptionVM model)
        {
            return model.MapTo<SpecificationAttributeOptionVM, SpecificationAttributeOption>();
        }

        public static SpecificationAttributeOption ToEntity(this SpecificationAttributeOptionVM model, SpecificationAttributeOption destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        #region Log

        public static LogVM ToModel(this Log entity)
        {
            return entity.MapTo<Log, LogVM>();
        }

        public static Log ToEntity(this LogVM model)
        {
            return model.MapTo<LogVM, Log>();
        }

        public static Log ToEntity(this LogVM model, Log destination)
        {
            return model.MapTo(destination);
        }

        public static ActivityLogTypeVM ToModel(this ActivityLogType entity)
        {
            return entity.MapTo<ActivityLogType, ActivityLogTypeVM>();
        }

        public static ActivityLogVM ToModel(this ActivityLog entity)
        {
            return entity.MapTo<ActivityLog, ActivityLogVM>();
        }

        #endregion

        #region Address

        public static AddressVM ToModel(this Address entity)
        {
            return entity.MapTo<Address, AddressVM>();
        }

        public static Address ToEntity(this AddressVM model)
        {
            return model.MapTo<AddressVM, Address>();
        }

        public static Address ToEntity(this AddressVM model, Address destination)
        {
            return model.MapTo(destination);
        }

        public static void PrepareCustomAddressAttributes(this AddressVM model,
            Address address,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeParser addressAttributeParser)
        {
            //this method is very similar to the same one in Nop.Web project
            if (addressAttributeService == null)
                throw new ArgumentNullException("addressAttributeService");

            if (addressAttributeParser == null)
                throw new ArgumentNullException("addressAttributeParser");

            var attributes = addressAttributeService.GetAllAddressAttributes();
            foreach (var attribute in attributes)
            {
                var attributeModel = new AddressVM.AddressAttributeVM
                {
                    Id = attribute.Id,
                    Name = attribute.Name,
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = addressAttributeService.GetAddressAttributeValues(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        var attributeValueModel = new AddressVM.AddressAttributeValueVM
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.Name,
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(attributeValueModel);
                    }
                }

                //set already selected attributes
                var selectedAddressAttributes = address != null ? address.CustomAttributes : null;
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                //clear default selection
                                foreach (var item in attributeModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedValues = addressAttributeParser.ParseAddressAttributeValues(selectedAddressAttributes);
                                foreach (var attributeValue in selectedValues)
                                    foreach (var item in attributeModel.Values)
                                        if (attributeValue.Id == item.Id)
                                            item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                var enteredText = addressAttributeParser.ParseValues(selectedAddressAttributes, attribute.Id);
                                if (enteredText.Any())
                                    attributeModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    default:
                        //not supported attribute control types
                        break;
                }

                model.CustomAddressAttributes.Add(attributeModel);
            }
        }

        #endregion

        #region Customer roles

        //customer roles
        public static CustomerRoleVM ToModel(this CustomerRole entity)
        {
            return entity.MapTo<CustomerRole, CustomerRoleVM>();
        }

        public static CustomerRole ToEntity(this CustomerRoleVM model)
        {
            return model.MapTo<CustomerRoleVM, CustomerRole>();
        }

        public static CustomerRole ToEntity(this CustomerRoleVM model, CustomerRole destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Vendor

        public static VendorVM ToModel(this Vendor entity)
        {
            return entity.MapTo<Vendor, VendorVM>();
        }

        public static Vendor ToEntity(this VendorVM model)
        {
            return model.MapTo<VendorVM, Vendor>();
        }

        public static Vendor ToEntity(this VendorVM model, Vendor destination)
        {
            return model.MapTo(destination);
        }

        #endregion
    }
}
