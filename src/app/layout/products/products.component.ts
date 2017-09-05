import { Component, OnInit, ViewChild, Input, ElementRef } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';
import { ProductService } from './product.service';
@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss']
})

/* This is still in development ! Has bugs ! */
export class ProductsComponent implements OnInit {
    currentPageNumber:number;
    @ViewChild('f') productForm: NgForm;
    @ViewChild('t') productEditForm: NgForm;
    @ViewChild('s') productSearchForm: NgForm;
    @ViewChild('q') productAttributeForm: NgForm;

    @ViewChild('g') attributeForm: NgForm;
    @Input() multiple: boolean = false;
    submitted :boolean;
    editMode: boolean;
    product = [];
    Name:string;
    FullDescription:string;
    Price: number;
    Id: number;
    Sku:string;
    Published:boolean;
    Gtin:string;
    filteredProduct:string;
    DisplayOrder:number;
    CreatedOn:string;
    loadingProduct:boolean;
    searchProductMode:boolean;
    searchProductParameters=[];
    searchedProducts;
    tag;

    categories;
    manufacturers;
    vendors;
    stores;
    tags;
    product_attribute;
    product_attributes;
    current_Id;
    current_attribute_id;
    current_attribute;
    current_attribute_description;
    specification_attribute;
    specification_attributes;
    current_spec_attribute;
    current_spec_attribute_description;
    products;
    Attribute:string;
    Attribute_TextPrompt:string;
    Attribute_IsRequired:boolean;
    Attribute_DisplayOrder:number;

    CategoryId=[];
    SelectedCategoryIds;

    StoreId=[];
    SelectedStoreIds;
    ManufacturerId=[];
    SelectedManufacturerIds;
    StockQuantity:number;
    VendorId:number;
    currentProduct=[];
    loading:boolean;
    totalProducts:number;
    addNewProduct:boolean;
    editAttributeMode:boolean;
    editSpecAttributeMode:boolean;
    loadingImagePath='';
    showProductList:boolean;
    ManufacturerPartNumber:string;
    ShowOnHomePage:boolean;
    MarkAsNew:boolean;
    showProductInfo:boolean;
    showPictures:boolean;
    showProductAttributes:boolean;
    showSpecificationAttributes:boolean;
    showSearchedProductList:boolean;
    productAttributeFields=[];
    specAttributeFields=[];
    addAttributeMode:boolean;
    showCurrentAttributeList:boolean;
    showCurrentSpecAttributeList:boolean;
    showCurrentAttributeForm:boolean;
    showCurrentSpecAttributeForm:boolean;



    constructor(private http: Http, private productService: ProductService) { }

    ngOnInit() {

        this.Name='';
        this.FullDescription='';
        this.filteredProduct='';
        this.totalProducts=25878;
        this.loadingImagePath = '../../assets/images/ajax-loader.gif';
        this.tags = JSON.parse(localStorage.getItem("tags"));
        this.addNewProduct=false;
        this.submitted=false;
        this.editMode=false;
        this.currentPageNumber=1;
        this.editAttributeMode=false;
        this.editSpecAttributeMode=false;
        this.showProductList=true;
        this.Sku='';
        this.Gtin='';
        this.Published=false;
        this.ManufacturerPartNumber='';
        this.ShowOnHomePage=false;
        this.MarkAsNew=false;
        this.showProductInfo=false;
        this.showPictures=false;
        this.showProductAttributes=false;
        this.showSpecificationAttributes=false;
        this.DisplayOrder=0;
        this.VendorId=0;
        this.StockQuantity=0;
        this.CreatedOn='';
        this.loadingProduct=false;
        this.searchProductMode=true;
        this.showSearchedProductList=false;
        this.addAttributeMode=true;
        this.showCurrentAttributeList=true;
        this.showCurrentSpecAttributeList=true;
        this.showCurrentAttributeForm=false;
        this.showCurrentSpecAttributeForm=false;

        this.getProducts(0);
        this.getAllData();


    }
    addProduct() {
        this.submitted = true;
        this.loadingProduct=true;
        if (this.editMode) {
            this.editProduct();
        }
        else {
            if (this.product.length == 0) {
                this.Id = 1;
            }
            else {
                this.Id = +this.product[this.product.length - 1].Id + 1;
            }
            this.Name = this.productForm.value.Name;
            this.FullDescription = this.productForm.value.FullDescription;
            this.Price = this.productForm.value.Price;
            this.CategoryId = this.productForm.value.CategoryId;
            this.StoreId = this.productForm.value.StoreId;
            this.ManufacturerId = this.productForm.value.ManufacturerId;
            this.Sku = this.productForm.value.Sku;
            this.Published = this.productForm.value.Published;
            this.Gtin = this.productForm.value.Gtin;
            this.ManufacturerPartNumber = this.productForm.value.ManufacturerPartNumber;
            this.ShowOnHomePage = this.productForm.value.ShowOnHomePage;
            this.MarkAsNew = this.productForm.value.MarkAsNew;
            this.DisplayOrder = this.productForm.value.DisplayOrder;
            this.StockQuantity = this.productForm.value.StockQuantity;
            this.CreatedOn=new Date(new Date().getTime()).toLocaleString();
            this.convertStringtoNumber();

            console.log(this.CategoryId);
            console.log(this.ManufacturerId);
            console.log(this.StoreId);
            //this.tag = (this.productForm.value.tag_name);
            //this.product_attribute = this.productForm.value.prod_attributes;
            //this.specification_attribute = this.productForm.value.spec_attributes;
                      this.product.push({
            "CustomProperties": {
              "sample string 1": {},
              "sample string 3": {}
            },
            "Id": 0,
            "PictureThumbnailUrl": "sample string 2",
            "ProductTypeId": 3,
            "ProductTypeName": "sample string 4",
            "AssociatedToProductId": 5,
            "AssociatedToProductName": "sample string 6",
            "VisibleIndividually": true,
            "ProductTemplateId": 8,
            "AvailableProductTemplates": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "ProductsTypesSupportedByProductTemplates": {
              "1": [
                {
                  "Disabled": true,
                  "Group": {
                    "Disabled": true,
                    "Name": "sample string 2"
                  },
                  "Selected": true,
                  "Text": "sample string 3",
                  "Value": "sample string 4"
                },
                {
                  "Disabled": true,
                  "Group": {
                    "Disabled": true,
                    "Name": "sample string 2"
                  },
                  "Selected": true,
                  "Text": "sample string 3",
                  "Value": "sample string 4"
                }
              ],
              "2": [
                {
                  "Disabled": true,
                  "Group": {
                    "Disabled": true,
                    "Name": "sample string 2"
                  },
                  "Selected": true,
                  "Text": "sample string 3",
                  "Value": "sample string 4"
                },
                {
                  "Disabled": true,
                  "Group": {
                    "Disabled": true,
                    "Name": "sample string 2"
                  },
                  "Selected": true,
                  "Text": "sample string 3",
                  "Value": "sample string 4"
                }
              ]
            },
            "Name":this.Name,
            "ShortFullDescription": "",
            "FullFullDescription": this.FullDescription,
            "AdminComment": "",
            "ShowOnHomePage": this.ShowOnHomePage,
            "MetaKeywords": "",
            "MetaFullDescription": "",
            "MetaTitle": "",
            "SeName": "",
            "AllowCustomerReviews": true,
            "ProductTags": "",
            "Sku": this.Sku,
            "ManufacturerPartNumber": this.ManufacturerPartNumber,
            "Gtin": this.Gtin,
            "IsGiftCard": true,
            "GiftCardTypeId": 24,
            "OverriddenGiftCardAmount": 1.0,
            "RequireOtherProducts": true,
            "RequiredProductIds": "sample string 26",
            "AutomaticallyAddRequiredProducts": true,
            "IsDownload": true,
            "DownloadId": 29,
            "UnlimitedDownloads": true,
            "MaxNumberOfDownloads": 31,
            "DownloadExpirationDays": 1,
            "DownloadActivationTypeId": 32,
            "HasSampleDownload": true,
            "SampleDownloadId": 34,
            "HasUserAgreement": true,
            "UserAgreementText": "sample string 36",
            "IsRecurring": true,
            "RecurringCycleLength": 38,
            "RecurringCyclePeriodId": 39,
            "RecurringTotalCycles": 40,
            "IsRental": true,
            "RentalPriceLength": 42,
            "RentalPricePeriodId": 43,
            "IsShipEnabled": true,
            "IsFreeShipping": true,
            "ShipSeparately": true,
            "AdditionalShippingCharge": 47.0,
            "DeliveryDateId": 48,
            "AvailableDeliveryDates": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "IsTaxExempt": true,
            "TaxCategoryId": 50,
            "AvailableTaxCategories": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "IsTelecommunicationsOrBroadcastingOrElectronicServices": true,
            "ManageInventoryMethodId": 52,
            "ProductAvailabilityRangeId": 53,
            "AvailableProductAvailabilityRanges": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "UseMultipleWarehouses": true,
            "WarehouseId": 55,
            "AvailableWarehouses": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "StockQuantity": this.StockQuantity,
            "LastStockQuantity": 57,
            "StockQuantityStr": "sample string 58",
            "DisplayStockAvailability": true,
            "DisplayStockQuantity": true,
            "MinStockQuantity": 1,
            "LowStockActivityId": 62,
            "NotifyAdminForQuantityBelow": 63,
            "BackorderModeId": 64,
            "AllowBackInStockSubscriptions": true,
            "OrderMinimumQuantity": 1,
            "OrderMaximumQuantity": 1000,
            "AllowedQuantities": "sample string 68",
            "AllowAddingOnlyExistingAttributeCombinations": true,
            "NotReturnable": true,
            "DisableBuyButton": true,
            "DisableWishlistButton": true,
            "AvailableForPreOrder": true,
            "PreOrderAvailabilityStartDateTimeUtc": "2017-09-05T07:58:32.0580719-07:00",
            "CallForPrice": true,
            "Price": this.Price,
            "OldPrice": 0.0,
            "ProductCost": 77.0,
            "CustomerEntersPrice": true,
            "MinimumCustomerEnteredPrice": 79.0,
            "MaximumCustomerEnteredPrice": 80.0,
            "BasepriceEnabled": true,
            "BasepriceAmount": 82.0,
            "BasepriceUnitId": 83,
            "AvailableBasepriceUnits": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "BasepriceBaseAmount": 84.0,
            "BasepriceBaseUnitId": 85,
            "AvailableBasepriceBaseUnits": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "MarkAsNew": this.MarkAsNew,
            "MarkAsNewStartDateTimeUtc": "2017-09-05T07:58:32.0580719-07:00",
            "MarkAsNewEndDateTimeUtc": "2017-09-05T07:58:32.0580719-07:00",
            "Weight": 87.0,
            "Length": 88.0,
            "Width": 89.0,
            "Height": 90.0,
            "AvailableStartDateTimeUtc": "2017-09-05T07:58:32.0580719-07:00",
            "AvailableEndDateTimeUtc": "2017-09-05T07:58:32.0580719-07:00",
            "DisplayOrder": this.DisplayOrder,
            "Published": this.Published,
            "CreatedOn": "2017-09-05T07:58:32.0580719-07:00",
            "UpdatedOn": "2017-09-05T07:58:32.0580719-07:00",
            "PrimaryStoreCurrencyCode": "sample string 93",
            "BaseDimensionIn": "sample string 94",
            "BaseWeightIn": "sample string 95",
            "Locales": [
              {
                "LanguageId": 1,
                "Name": "sample string 2",
                "ShortFullDescription": "sample string 3",
                "FullFullDescription": "sample string 4",
                "MetaKeywords": "sample string 5",
                "MetaFullDescription": "sample string 6",
                "MetaTitle": "sample string 7",
                "SeName": "sample string 8"
              },
              {
                "LanguageId": 1,
                "Name": "sample string 2",
                "ShortFullDescription": "sample string 3",
                "FullFullDescription": "sample string 4",
                "MetaKeywords": "sample string 5",
                "MetaFullDescription": "sample string 6",
                "MetaTitle": "sample string 7",
                "SeName": "sample string 8"
              }
            ],
            "SelectedCustomerRoleIds": [
              1,
              2
            ],
            "AvailableCustomerRoles": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "SelectedStoreIds": this.StoreId,
            "AvailableStores": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "SelectedCategoryIds":this.CategoryId,
            "AvailableCategories": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "SelectedManufacturerIds": this.ManufacturerId,
            "AvailableManufacturers": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "VendorId": this.VendorId,
            "AvailableVendors": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "SelectedDiscountIds": [
              1,
              2
            ],
            "AvailableDiscounts": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "IsLoggedInAsVendor": true,
            "AvailableProductAttributes": [
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              },
              {
                "Disabled": true,
                "Group": {
                  "Disabled": true,
                  "Name": "sample string 2"
                },
                "Selected": true,
                "Text": "sample string 3",
                "Value": "sample string 4"
              }
            ],
            "AddPictureModel": {
              "Id": 1,
              "CustomProperties": {
                "sample string 1": {},
                "sample string 3": {}
              },
              "ProductId": 2,
              "PictureId": 3,
              "PictureUrl": "sample string 4",
              "DisplayOrder": 5,
              "OverrideAltAttribute": "sample string 6",
              "OverrideTitleAttribute": "sample string 7"
            },
            "ProductPictureModels": [
              {
                "Id": 1,
                "CustomProperties": {
                  "sample string 1": {},
                  "sample string 3": {}
                },
                "ProductId": 2,
                "PictureId": 3,
                "PictureUrl": "sample string 4",
                "DisplayOrder": 5,
                "OverrideAltAttribute": "sample string 6",
                "OverrideTitleAttribute": "sample string 7"
              },
              {
                "Id": 1,
                "CustomProperties": {
                  "sample string 1": {},
                  "sample string 3": {}
                },
                "ProductId": 2,
                "PictureId": 3,
                "PictureUrl": "sample string 4",
                "DisplayOrder": 5,
                "OverrideAltAttribute": "sample string 6",
                "OverrideTitleAttribute": "sample string 7"
              }
            ],
            "AddSpecificationAttributeModel": {
              "Id": 1,
              "CustomProperties": {
                "sample string 1": {},
                "sample string 3": {}
              },
              "SpecificationAttributeId": 2,
              "AttributeTypeId": 3,
              "SpecificationAttributeOptionId": 4,
              "CustomValue": "sample string 5",
              "AllowFiltering": true,
              "ShowOnProductPage": true,
              "DisplayOrder": 8,
              "AvailableAttributes": [
                {
                  "Disabled": true,
                  "Group": {
                    "Disabled": true,
                    "Name": "sample string 2"
                  },
                  "Selected": true,
                  "Text": "sample string 3",
                  "Value": "sample string 4"
                },
                {
                  "Disabled": true,
                  "Group": {
                    "Disabled": true,
                    "Name": "sample string 2"
                  },
                  "Selected": true,
                  "Text": "sample string 3",
                  "Value": "sample string 4"
                }
              ],
              "AvailableOptions": [
                {
                  "Disabled": true,
                  "Group": {
                    "Disabled": true,
                    "Name": "sample string 2"
                  },
                  "Selected": true,
                  "Text": "sample string 3",
                  "Value": "sample string 4"
                },
                {
                  "Disabled": true,
                  "Group": {
                    "Disabled": true,
                    "Name": "sample string 2"
                  },
                  "Selected": true,
                  "Text": "sample string 3",
                  "Value": "sample string 4"
                }
              ]
            },
            "ProductWarehouseInventoryModels": [
              {
                "Id": 1,
                "WarehouseId": 2,
                "WarehouseName": "sample string 3",
                "WarehouseUsed": true,
                "StockQuantity": 5,
                "ReservedQuantity": 6,
                "PlannedQuantity": 7
              },
              {
                "Id": 1,
                "WarehouseId": 2,
                "WarehouseName": "sample string 3",
                "WarehouseUsed": true,
                "StockQuantity": 5,
                "ReservedQuantity": 6,
                "PlannedQuantity": 7
              }
            ],
            "CopyProductModel": {
              "Id": 1,
              "CustomProperties": {
                "sample string 1": {},
                "sample string 3": {}
              },
              "Name": "sample string 2",
              "CopyImages": true,
              "Published": true
            },
            "ProductEditorSettingsModel": {
              "CustomProperties": {
                "sample string 1": {},
                "sample string 3": {}
              },
              "Id": true,
              "ProductType": true,
              "VisibleIndividually": true,
              "ProductTemplate": true,
              "AdminComment": true,
              "Vendor": true,
              "Stores": true,
              "ACL": true,
              "ShowOnHomePage": true,
              "DisplayOrder": true,
              "AllowCustomerReviews": true,
              "ProductTags": true,
              "ManufacturerPartNumber": true,
              "GTIN": true,
              "ProductCost": true,
              "TierPrices": true,
              "Discounts": true,
              "DisableBuyButton": true,
              "DisableWishlistButton": true,
              "AvailableForPreOrder": true,
              "CallForPrice": true,
              "OldPrice": true,
              "CustomerEntersPrice": true,
              "PAngV": true,
              "RequireOtherProductsAddedToTheCart": true,
              "IsGiftCard": true,
              "DownloadableProduct": true,
              "RecurringProduct": true,
              "IsRental": true,
              "FreeShipping": true,
              "ShipSeparately": true,
              "AdditionalShippingCharge": true,
              "DeliveryDate": true,
              "TelecommunicationsBroadcastingElectronicServices": true,
              "ProductAvailabilityRange": true,
              "UseMultipleWarehouses": true,
              "Warehouse": true,
              "DisplayStockAvailability": true,
              "DisplayStockQuantity": true,
              "MinimumStockQuantity": true,
              "LowStockActivity": true,
              "NotifyAdminForQuantityBelow": true,
              "Backorders": true,
              "AllowBackInStockSubscriptions": true,
              "MinimumCartQuantity": true,
              "MaximumCartQuantity": true,
              "AllowedQuantities": true,
              "AllowAddingOnlyExistingAttributeCombinations": true,
              "NotReturnable": true,
              "Weight": true,
              "Dimensions": true,
              "AvailableStartDate": true,
              "AvailableEndDate": true,
              "MarkAsNew": true,
              "MarkAsNewStartDate": true,
              "MarkAsNewEndDate": true,
              "Published": true,
              "CreatedOn": true,
              "UpdatedOn": true,
              "RelatedProducts": true,
              "CrossSellsProducts": true,
              "Seo": true,
              "PurchasedWithOrders": true,
              "OneColumnProductPage": true,
              "ProductAttributes": true,
              "SpecificationAttributes": true,
              "Manufacturers": true,
              "StockQuantityHistory": true
            },
            "StockQuantityHistory": {
              "Id": 1,
              "CustomProperties": {
                "sample string 1": {},
                "sample string 3": {}
              },
              "SearchWarehouseId": 2,
              "WarehouseName": "sample string 3",
              "AttributeCombination": "sample string 4",
              "QuantityAdjustment": 5,
              "StockQuantity": this.StockQuantity,
              "Message": "sample string 7",
              "CreatedOn": this.CreatedOn
            }
          });

          this.productService.storeProduct(this.product)
                  .subscribe(
                        (data) => {
                            this.loadingProduct=false;
                            alert("Product Added");
                            this.productForm.reset();
                        },
                        (error) => {
                          console.log(error);
                          alert('Can\'t fetch data ! Please refresh or check your connnection !');
                        }
            );



        }
    }
    editProductMode(id: HTMLFormElement) {
        this.getAllData();
        this.showProductList=false;
        this.searchProductMode=false;
        this.editMode = true;
        this.showProductInfo=true;
        this.Id = +id.name;
        this.currentProduct = this.getCurrentProduct(this.Id)[0];
        // console.log(this.product[1].Name);p
        this.Name = this.currentProduct["Name"];
        this.FullDescription = this.currentProduct["FullDescription"];
        this.Price = this.currentProduct["Price"];
        this.CategoryId = this.currentProduct["CategoryId"];
        this.StoreId = this.currentProduct["StoreId"];
        this.ManufacturerId = this.currentProduct["ManufacturerId"];
        this.Sku = this.currentProduct["Sku"];
        this.Published = this.currentProduct["Published"];
        this.Gtin = this.currentProduct["Gtin"];
        this.ManufacturerPartNumber = this.currentProduct["ManufacturerPartNumber"];
        this.ShowOnHomePage = this.currentProduct["ShowOnHomePage"];
        this.MarkAsNew = this.currentProduct["MarkAsNew"];
        this.DisplayOrder = this.currentProduct["DisplayOrder"];
        this.StockQuantity = this.currentProduct["StockQuantity"];
        //this.SelectedCategoryId = this.product[+this.Id].SelectedCategoryId;

        // this.product_attribute = (this.product[+this.Id].product_attributes);
        // this.specification_attribute = (this.product[+this.Id].specification_attributes);

    }
    editProduct() {
        this.loadingProduct=true;
        this.Name = this.productEditForm.value.Name;
        this.FullDescription = this.productEditForm.value.FullDescription;
        this.Price = this.productEditForm.value.Price;
        this.CategoryId = this.productEditForm.value.CategoryId;
        this.StoreId = this.productEditForm.value.StoreId;
        this.ManufacturerId = this.productEditForm.value.ManufacturerId;
        this.Sku = this.productEditForm.value.Sku;
        this.Published = this.productEditForm.value.Published;
        this.Gtin = this.productEditForm.value.Gtin;
        this.ManufacturerPartNumber = this.productEditForm.value.ManufacturerPartNumber;
        this.ShowOnHomePage = this.productEditForm.value.ShowOnHomePage;
        this.MarkAsNew = this.productEditForm.value.MarkAsNew;
        this.DisplayOrder = this.productEditForm.value.DisplayOrder;
        this.StockQuantity = this.productEditForm.value.StockQuantity;
        //this.CreatedOn=new Date(new Date().getTime()).toLocaleString();
        this.convertStringtoNumber();

        console.log(this.CategoryId);
        console.log(this.ManufacturerId);
        console.log(this.StoreId);
         this.currentProduct["Name"]=this.Name;
        this.currentProduct["FullDescription"]=this.FullDescription ;
        this.currentProduct["Price"]=this.Price ;
        this.currentProduct["CategoryId"]=this.CategoryId  ;
        this.currentProduct["StoreId"]=this.StoreId  ;
         this.currentProduct["ManufacturerId"]=this.ManufacturerId;
        this.currentProduct["Sku"]=this.Sku;
         this.currentProduct["Published"]=this.Published ;
        this.currentProduct["Gtin"]=this.Gtin;
         this.currentProduct["ManufacturerPartNumber"]=this.ManufacturerPartNumber;
         this.currentProduct["ShowOnHomePage"]=this.ShowOnHomePage;
         this.currentProduct["MarkAsNew"]=this.MarkAsNew;
         this.currentProduct["DisplayOrder"]=this.DisplayOrder;
         this.currentProduct["StockQuantity"]=this.StockQuantity;
         console.log(this.currentProduct);
         this.productService.updateProduct(this.currentProduct)
             .subscribe(
             (response) => {
                 this.loadingProduct=false;
                 alert("Product Updated !");
             },
             (error) =>      {
                     console.log(error);
                     alert("Can't fetch data ! Please refresh or check your connnection !");
                   }
             );

    }
    deleteProduct(id: HTMLFormElement) {
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
          this.productService.deleteProduct(+id.name)
            .subscribe(
            (data) => {

              alert('Deleted !');
              this.getProducts(this.currentPageNumber);
            },
            (error) => {
              console.log(error)
              alert('Can\'t fetch data ! Please refresh or check your connnection !')
            }
            );
        }
    }
    getProducts(page:number){
        this.loading=true;

        this.productService.getAllProducts(page)
            .subscribe(
            (response) => {
                this.loading=false;
                this.currentPageNumber=page;


                this.products = (response.json().Data);

                //this.product = JSON.parse(this.products);
                console.log((this.products));
                //  this.attribute =[this.attributes];
            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );

    }
    searchProduct(){
        this.loading=true;
        this.Name = this.productSearchForm.value.Name;
        this.Price = this.productSearchForm.value.Price;
        this.CategoryId = this.productSearchForm.value.CategoryId;
        this.StoreId = this.productSearchForm.value.StoreId;
        this.ManufacturerId = this.productSearchForm.value.ManufacturerId;
        //this.convertStringtoNumber();

        this.searchProductParameters.push({
  "Id": 0,
  "CustomProperties": {
    "sample string 1": {},
    "sample string 3": {}
  },
  "SearchProductName": this.Name,
  "SearchCategoryId": +this.CategoryId,
  "SearchIncludeSubCategories": true,
  "SearchManufacturerId": +this.ManufacturerId,
  "SearchStoreId": +this.StoreId,
  "SearchVendorId": +this.VendorId,
  "SearchWarehouseId": null,
  "SearchProductTypeId": null,
  "SearchPublishedId": null,
  "GoDirectlyToSku": null,
  "IsLoggedInAsVendor": null,
  "AllowVendorsToImportProducts": null,
  "AvailableCategories": [
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    },
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    }
  ],
  "AvailableManufacturers": [
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    },
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    }
  ],
  "AvailableStores": [
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    },
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    }
  ],
  "AvailableWarehouses": [
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    },
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    }
  ],
  "AvailableVendors": [
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    },
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    }
  ],
  "AvailableProductTypes": [
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    },
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    }
  ],
  "AvailablePublishedOptions": [
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    },
    {
      "Disabled": true,
      "Group": {
        "Disabled": true,
        "Name": "sample string 2"
      },
      "Selected": true,
      "Text": "sample string 3",
      "Value": "sample string 4"
    }
  ]
})
        this.productService.searchProduct(this.searchProductParameters)
            .subscribe(
            (response) => {
                this.loading=false;
                this.showSearchedProductList=true;
                this.showProductList=false;
                this.searchedProducts = (response.json().Data);
            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );

    }
    getAttributes() {
        this.productService.getAttributes()
            .subscribe(
            (response) => {
                this.product_attributes = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );

    }
    getSpecAttributes() {
        this.productService.getSpecAttributes()
            .subscribe(
            (response) => {
                this.specification_attributes = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );

    }
    getCategory() {
        this.productService.getCategory()
            .subscribe(
            (response) => {
                this.categories = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );
    }
    getAllData() {
        //this.getProducts(0);

        // this.getCategory();
        // this.getManufacturers();
        // this.getStores();
        // this.getVendors();
        this.getAttributes();
        this.getSpecAttributes();
    }

    editProductAttribute(prod_id,attr_id){
      console.log(prod_id,attr_id);
      this.editAttributeMode=true;
      this.current_Id =prod_id;
      this.current_attribute_id =attr_id;
      this.current_attribute = this.product[prod_id].product_attributes[attr_id];
      this.current_attribute_description = this.product[prod_id].product_attributes[attr_id].description;
      console.log(this.current_attribute);
    }
    saveAttribute(){
      this.current_attribute_description = this.attributeForm.value.prod_attrib;
      this.product[+this.current_Id].product_attributes[this.current_attribute_id].description = this.current_attribute_description;
      localStorage.setItem("products",JSON.stringify(this.product));
      console.log("Up")

    }
    showList(){
        this.editMode=false;
        this.showProductList=true;
        this.searchProductMode=true;
        this.showSearchedProductList=false;

    }
    showToggle(toggle:number){
        switch(toggle) {
   case 1: {
      this.showProductInfo=true;
      this.showPictures=false;
      this.showProductAttributes=false;
      this.showSpecificationAttributes=false;
      break;
   }
   case 2: {
       this.showProductInfo=false;
       this.showPictures=true;
       this.showProductAttributes=false;
       this.showSpecificationAttributes=false;
      break;
   }
   case 3: {
       this.showProductInfo=false;
       this.showPictures=false;
       this.showProductAttributes=true;
       this.showCurrentAttributeForm=true;
       this.showSpecificationAttributes=false;
       this.getCurrentAttributes();
      break;
   }
   case 4: {
       this.showProductInfo=false;
       this.showPictures=false;
       this.showProductAttributes=false;
       this.showSpecificationAttributes=true;
       this.getCurrentSpecAttributes();
      break;
   }
   default: {
       this.showProductInfo=true;
       this.showPictures=false;
       this.showProductAttributes=false;
       this.showSpecificationAttributes=false;
      break;
   }
}
    }
    getManufacturers(){
        this.productService.getManufacturers()
            .subscribe(
            (response) => {
                this.loading=false;
                this.manufacturers = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );

    }
    getVendors(){
        this.productService.getVendors()
            .subscribe(
            (response) => {
                this.loading=false;
                this.vendors = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );
    }
    getStores(){
        this.productService.getStores()
            .subscribe(
            (response) => {
                this.loading=false;
                this.stores = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );
    }
    addProductMode(){
      this.showProductList=false;
      this.addNewProduct = true;
      this.showToggle(1);
      this.getAllData();
    }
    convertStringtoNumber(){
        for (let index in this.CategoryId){
            this.CategoryId[index]= +this.CategoryId[index];
        }
        for(let index in this.ManufacturerId){
            this.ManufacturerId[index]=+this.ManufacturerId[index];
        }
        for(let index in this.StoreId){
            this.StoreId[index]=+this.StoreId[index];
        }
    }
    getCurrentProduct(id:number){
        return this.products.filter(
          function(product){ return product.Id == id }
      );
    }
    getCurrentAttributes(){
        this.productService.getCurrentAttributes(this.Id)
            .subscribe(
            (response) => {
                this.productAttributeFields = (response.json().Data);
                console.log("Attributes Retrieved");

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );

    }
    getCurrentSpecAttributes(){
        this.productService.getCurrentSpecAttributes(this.Id)
            .subscribe(
            (response) => {
                this.specAttributeFields = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );
    }
    addCurrentAttributeMode(){
        this.addAttributeMode=false;
        this.showCurrentAttributeList=false;
        this.showCurrentAttributeForm=true;

    }
    addAttribute(){

    }

}
