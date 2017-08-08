using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.Model.Messages;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.Model.Shipping;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.Model.Vendors;
using Denmakers.DreamSale.Services.Addresses;
using Denmakers.DreamSale.ViewModels.AdminVM.Addresses;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using Denmakers.DreamSale.ViewModels.AdminVM.Common;
using Denmakers.DreamSale.ViewModels.AdminVM.Customers;
using Denmakers.DreamSale.ViewModels.AdminVM.Logging;
using Denmakers.DreamSale.ViewModels.AdminVM.Messages;
using Denmakers.DreamSale.ViewModels.AdminVM.Orders;
using Denmakers.DreamSale.ViewModels.AdminVM.Settings;
using Denmakers.DreamSale.ViewModels.AdminVM.Stores;
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

        #region Checkout attributes

        //attributes
        public static CheckoutAttributeVM ToModel(this CheckoutAttribute entity)
        {
            return entity.MapTo<CheckoutAttribute, CheckoutAttributeVM>();
        }

        public static CheckoutAttribute ToEntity(this CheckoutAttributeVM model)
        {
            return model.MapTo<CheckoutAttributeVM, CheckoutAttribute>();
        }

        public static CheckoutAttribute ToEntity(this CheckoutAttributeVM model, CheckoutAttribute destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Customer attributes
        public static CustomerAttributeVM ToModel(this CustomerAttribute entity)
        {
            return entity.MapTo<CustomerAttribute, CustomerAttributeVM>();
        }

        public static CustomerAttribute ToEntity(this CustomerAttributeVM model)
        {
            return model.MapTo<CustomerAttributeVM, CustomerAttribute>();
        }

        public static CustomerAttribute ToEntity(this CustomerAttributeVM model, CustomerAttribute destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Address attributes
        public static AddressAttributeVM ToModel(this AddressAttribute entity)
        {
            return entity.MapTo<AddressAttribute, AddressAttributeVM>();
        }

        public static AddressAttribute ToEntity(this AddressAttributeVM model)
        {
            return model.MapTo<AddressAttributeVM, AddressAttribute>();
        }

        public static AddressAttribute ToEntity(this AddressAttributeVM model, AddressAttribute destination)
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

        #region Email account

        public static EmailAccountVM ToModel(this EmailAccount entity)
        {
            return entity.MapTo<EmailAccount, EmailAccountVM>();
        }

        public static EmailAccount ToEntity(this EmailAccountVM model)
        {
            return model.MapTo<EmailAccountVM, EmailAccount>();
        }

        public static EmailAccount ToEntity(this EmailAccountVM model, EmailAccount destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Stores

        public static StoreVM ToModel(this Store entity)
        {
            return entity.MapTo<Store, StoreVM>();
        }

        public static Store ToEntity(this StoreVM model)
        {
            return model.MapTo<StoreVM, Store>();
        }

        public static Store ToEntity(this StoreVM model, Store destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Queued email

        public static QueuedEmailVM ToModel(this QueuedEmail entity)
        {
            return entity.MapTo<QueuedEmail, QueuedEmailVM>();
        }

        public static QueuedEmail ToEntity(this QueuedEmailVM model)
        {
            return model.MapTo<QueuedEmailVM, QueuedEmail>();
        }

        public static QueuedEmail ToEntity(this QueuedEmailVM model, QueuedEmail destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Settings

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
        public static ShippingSettingsVM ToModel(this ShippingSettings entity)
        {
            return entity.MapTo<ShippingSettings, ShippingSettingsVM>();
        }
        public static ShippingSettings ToEntity(this ShippingSettingsVM model, ShippingSettings destination)
        {
            return model.MapTo(destination);
        }

        public static VendorSettingsVM ToModel(this VendorSettings entity)
        {
            return entity.MapTo<VendorSettings, VendorSettingsVM>();
        }
        public static VendorSettings ToEntity(this VendorSettingsVM model, VendorSettings destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        #region Return request reason

        public static ReturnRequestReasonVM ToModel(this ReturnRequestReason entity)
        {
            return entity.MapTo<ReturnRequestReason, ReturnRequestReasonVM>();
        }

        public static ReturnRequestReason ToEntity(this ReturnRequestReasonVM model)
        {
            return model.MapTo<ReturnRequestReasonVM, ReturnRequestReason>();
        }

        public static ReturnRequestReason ToEntity(this ReturnRequestReasonVM model, ReturnRequestReason destination)
        {
            return model.MapTo(destination);
        }

        #endregion
    }
}
