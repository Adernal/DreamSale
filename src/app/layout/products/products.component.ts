import { Component, OnInit, ViewChild, Input, ElementRef } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http, ResponseContentType } from '@angular/http';
import { ProductService } from './product.service';
import { ProductAttributesService } from './product-attributes/product-attributes.service';
import { RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Headers} from '@angular/http';
import {CsvService} from "angular2-json2csv";
import * as FileSaver from 'file-saver';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss']
})


export class ProductsComponent implements OnInit {
   updatedProduct={};
    Token: string;
    currentPageNumber:number;
    @ViewChild('f') productForm: NgForm;
    @ViewChild('t') productEditForm: NgForm;
    @ViewChild('s') productSearchForm: NgForm;
    @ViewChild('q') productAttributeForm: NgForm;
    @ViewChild('u') specAttributeForm: NgForm;


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
    searchProductParameters={};
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
    current_attribute=[];
    current_attribute_description;
    current_spec_attribute_id;
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
    addSpecAttributeMode:boolean;
    Spec_Attribute_Id='';
    ValueRaw='';
    public selectedFiles;





    constructor(private http: Http, private productService: ProductService,private productAttributeService: ProductAttributesService,private _csvService: CsvService) { }

    ngOnInit() {

        this.Name='';
        this.FullDescription='';
        this.filteredProduct='';

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
        this.addAttributeMode=false;
        this.showCurrentAttributeList=true;
        this.showCurrentSpecAttributeList=true;
        this.showCurrentAttributeForm=false;
        this.showCurrentSpecAttributeForm=false;
        this.addSpecAttributeMode=false;

        this.getProducts(1);
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
          //  this.convertStringtoNumber();

            console.log(this.CategoryId);
            console.log(this.ManufacturerId);
            console.log(this.StoreId);
            //this.tag = (this.productForm.value.tag_name);
            //this.product_attribute = this.productForm.value.prod_attributes;
            //this.specification_attribute = this.productForm.value.spec_attributes;
                      this.product.push({
    "CustomProperties": {},
    "Id": 0,
    "PictureThumbnailUrl": null,
    "ProductTypeId": 5,
    "ProductTypeName": null,
    "AssociatedToProductId": 0,
    "AssociatedToProductName": null,
    "VisibleIndividually": true,
    "ProductTemplateId": 1,
    "Name": this.Name,
    "ShortDescription": "Test product (Token)",
    "FullDescription":this.FullDescription,
    "AdminComment": null,
    "ShowOnHomePage": this.ShowOnHomePage,
    "MetaKeywords": null,
    "MetaDescription": null,
    "MetaTitle": null,
    "SeName": null,
    "AllowCustomerReviews": true,
    "ProductTags": null,
    "Sku":this.Sku,
    "ManufacturerPartNumber":this.ManufacturerPartNumber,
    "Gtin": this.Gtin,
    "IsGiftCard": false,
    "GiftCardTypeId": 0,
    "OverriddenGiftCardAmount": null,
    "RequiredProductIds": null,
    "AutomaticallyAddRequiredProducts": false,
    "IsDownload": false,
    "DownloadId": 0,
    "UnlimitedDownloads": true,
    "MaxNumberOfDownloads": 10,
    "DownloadExpirationDays": null,
    "DownloadActivationTypeId": 0,
    "HasSampleDownload": false,
    "SampleDownloadId": 0,
    "HasUserAgreement": false,
    "UserAgreementText": null,
    "IsRecurring": false,
    "RecurringCycleLength": 100,
    "RecurringCyclePeriodId": 0,
    "RecurringTotalCycles": 10,
    "IsRental": false,
    "RentalPriceLength": 1,
    "RentalPricePeriodId": 0,
    "IsShipEnabled": true,
    "IsFreeShipping": false,
    "ShipSeparately": false,
    "AdditionalShippingCharge": 0,
    "DeliveryDateId": 0,

    "IsTaxExempt": false,
    "TaxCategoryId": 0,

    "IsTelecommunicationsOrBroadcastingOrElectronicServices": false,
    "ManageInventoryMethodId": 0,
    "ProductAvailabilityRangeId": 0,

    "UseMultipleWarehouses": false,
    "WarehouseId": 1,

    "StockQuantity": this.StockQuantity,
    "LastStockQuantity": 0,
    "StockQuantityStr": null,
    "AvailableBasepriceUnits": [
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "ounce(s)",
            "Value": "1"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "lb(s)",
            "Value": "2"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "kg(s)",
            "Value": "3"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "gram(s)",
            "Value": "4"
        }
    ],
    "BasepriceBaseAmount": 0,
    "BasepriceBaseUnitId": 0,
    "AvailableBasepriceBaseUnits": [
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "ounce(s)",
            "Value": "1"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "lb(s)",
            "Value": "2"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "kg(s)",
            "Value": "3"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "gram(s)",
            "Value": "4"
        }
    ],
    "MarkAsNew": this.MarkAsNew,
    "MarkAsNewStartDateTimeUtc": null,
    "MarkAsNewEndDateTimeUtc": null,
    "Weight": 0,
    "Length": 0,
    "Width": 0,
    "Height": 0,
    "AvailableStartDateTimeUtc": null,
    "AvailableEndDateTimeUtc": null,
    "DisplayOrder": this.DisplayOrder,
    "Published": this.Published,
    "CreatedOn": null,
    "UpdatedOn": null,
    "PrimaryStoreCurrencyCode": "INR",
    "BaseDimensionIn": "inch(es)",
    "BaseWeightIn": "lb(s)",
    "Locales": [],
    "SelectedCustomerRoleIds": [],
    "AvailableCustomerRoles": [
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Administrators",
            "Value": "1"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Forum Moderators",
            "Value": "2"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Guests",
            "Value": "4"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Registered",
            "Value": "3"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Vendors",
            "Value": "5"
        }
    ],
    "SelectedStoreIds": this.StoreId,
    "AvailableStores": [
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Your store name",
            "Value": "1"
        }
    ],
    "SelectedCategoryIds": this.CategoryId,
    "AvailableCategories": [
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Computers",
            "Value": "1"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Computers >> Desktops",
            "Value": "2"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Computers >> Notebooks",
            "Value": "3"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Computers >> Software",
            "Value": "4"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Shoes",
            "Value": "18"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Electronics",
            "Value": "5"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Electronics >> Camera & photo",
            "Value": "6"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Electronics >> Cell phones",
            "Value": "7"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Electronics >> Others",
            "Value": "8"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Apparel",
            "Value": "9"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Apparel >> Shoes",
            "Value": "10"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Apparel >> Clothing",
            "Value": "11"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Apparel >> Clothing >> Jeans",
            "Value": "17"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Apparel >> Clothing >> Jeans >> level 4",
            "Value": "19"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Apparel >> Accessories",
            "Value": "12"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Digital downloads",
            "Value": "13"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Books",
            "Value": "14"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Jewelry",
            "Value": "15"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Gift Cards",
            "Value": "16"
        }
    ],
    "SelectedManufacturerIds": this.ManufacturerId,
    "AvailableManufacturers": [
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Test manufacturer updated 1",
            "Value": "5"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Apple",
            "Value": "1"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "HP",
            "Value": "2"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Nike",
            "Value": "3"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Test manufacturer",
            "Value": "6"
        }
    ],
    "VendorId": this.VendorId,
    "AvailableVendors": [
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "No vendor",
            "Value": "0"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Sandeep Vendor",
            "Value": "3"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Vendor 1 update test",
            "Value": "1"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Vendor 2",
            "Value": "2"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Create Vendor Test",
            "Value": "5"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Create Vendor Test",
            "Value": "6"
        }
    ],
    "SelectedDiscountIds": [],
    "AvailableDiscounts": [
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "Sample discount with coupon code",
            "Value": "1"
        }
    ],
    "IsLoggedInAsVendor": false,
    "AvailableProductAttributes": [],
    "AddPictureModel": {
        "Id": 0,
        "CustomProperties": null,
        "ProductId": 0,
        "PictureId": 0,
        "PictureUrl": null,
        "DisplayOrder": 0,
        "OverrideAltAttribute": null,
        "OverrideTitleAttribute": null
    },
    "ProductPictureModels": [],
    "AddSpecificationAttributeModel": {
        "Id": 0,
        "CustomProperties": {},
        "SpecificationAttributeId": 0,
        "AttributeTypeId": 0,
        "SpecificationAttributeOptionId": 0,
        "CustomValue": null,
        "AllowFiltering": false,
        "ShowOnProductPage": false,
        "DisplayOrder": 0,
        "AvailableAttributes": [],
        "AvailableOptions": []
    },
    "ProductWarehouseInventoryModels": [
        {
            "Id": 0,
            "WarehouseId": 1,
            "WarehouseName": "Warehouse 1 (New York)",
            "WarehouseUsed": false,
            "StockQuantity": 0,
            "ReservedQuantity": 0,
            "PlannedQuantity": 0
        },
        {
            "Id": 0,
            "WarehouseId": 2,
            "WarehouseName": "Warehouse 2 (Los Angeles)",
            "WarehouseUsed": false,
            "StockQuantity": 0,
            "ReservedQuantity": 0,
            "PlannedQuantity": 0
        }
    ],
    "CopyProductModel": {
        "Id": 0,
        "CustomProperties": null,
        "Name": null,
        "CopyImages": false,
        "Published": false
    },
    "ProductEditorSettingsModel": {
        "CustomProperties": null,
        "Id": false,
        "ProductType": false,
        "VisibleIndividually": false,
        "ProductTemplate": false,
        "AdminComment": true,
        "Vendor": false,
        "Stores": false,
        "ACL": false,
        "ShowOnHomePage": false,
        "DisplayOrder": false,
        "AllowCustomerReviews": false,
        "ProductTags": false,
        "ManufacturerPartNumber": false,
        "GTIN": false,
        "ProductCost": false,
        "TierPrices": false,
        "Discounts": false,
        "DisableBuyButton": false,
        "DisableWishlistButton": false,
        "AvailableForPreOrder": false,
        "CallForPrice": false,
        "OldPrice": false,
        "CustomerEntersPrice": false,
        "PAngV": false,
        "RequireOtherProductsAddedToTheCart": false,
        "IsGiftCard": false,
        "DownloadableProduct": false,
        "RecurringProduct": false,
        "IsRental": false,
        "FreeShipping": false,
        "ShipSeparately": false,
        "AdditionalShippingCharge": false,
        "DeliveryDate": false,
        "TelecommunicationsBroadcastingElectronicServices": false,
        "ProductAvailabilityRange": false,
        "UseMultipleWarehouses": false,
        "Warehouse": false,
        "DisplayStockAvailability": false,
        "DisplayStockQuantity": false,
        "MinimumStockQuantity": false,
        "LowStockActivity": false,
        "NotifyAdminForQuantityBelow": false,
        "Backorders": false,
        "AllowBackInStockSubscriptions": false,
        "MinimumCartQuantity": false,
        "MaximumCartQuantity": false,
        "AllowedQuantities": false,
        "AllowAddingOnlyExistingAttributeCombinations": false,
        "NotReturnable": false,
        "Weight": true,
        "Dimensions": true,
        "AvailableStartDate": false,
        "AvailableEndDate": false,
        "MarkAsNew": false,
        "MarkAsNewStartDate": false,
        "MarkAsNewEndDate": false,
        "Published": false,
        "CreatedOn": false,
        "UpdatedOn": false,
        "RelatedProducts": false,
        "CrossSellsProducts": false,
        "Seo": false,
        "PurchasedWithOrders": false,
        "OneColumnProductPage": false,
        "ProductAttributes": true,
        "SpecificationAttributes": true,
        "Manufacturers": false,
        "StockQuantityHistory": false
    },
    "StockQuantityHistory": {
        "Id": 0,
        "CustomProperties": null,
        "SearchWarehouseId": 0,
        "WarehouseName": null,
        "AttributeCombination": null,
        "QuantityAdjustment": 0,
        "StockQuantity": 0,
        "Message": null,
        "CreatedOn": "0001-01-01T00:00:00"
    }
});

          this.productService.storeProduct(this.product)
                  .subscribe(
                        (data) => {
                            this.loadingProduct=false;
                            alert("Product Added");
                            this.productForm.reset();
                            this.getProducts(this.currentPageNumber);
                        },
                        (error) => {
                          console.log(error);
                          alert('Can\'t add Product ! Please refresh or check your connnection !');
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
        this.showProductAttributes=false;
        this.showPictures=false;
        this.showSpecificationAttributes=false;
        this.Id = +id.name;
        this.currentProduct = this.getCurrentProduct(this.Id)[0];

        this.Name = this.currentProduct["Name"];
        this.FullDescription = this.currentProduct["FullDescription"];
        this.Price = this.currentProduct["Price"];
        this.CategoryId = this.currentProduct["SelectedCategoryIds"];
        console.log("Selected Category Ids :"+this.CategoryId);
        this.StoreId = this.currentProduct["SelectedStoreIds"];
        console.log("Selected Store Ids:"+this.SelectedStoreIds);
        this.ManufacturerId = this.currentProduct["SelectedManufacturerIds"];
        console.log("Selected Manufacturer Ids :"+this.ManufacturerId);
        this.Sku = this.currentProduct["Sku"];
        this.Published = this.currentProduct["Published"];
        this.Gtin = this.currentProduct["Gtin"];
        this.ManufacturerPartNumber = this.currentProduct["ManufacturerPartNumber"];
        this.ShowOnHomePage = this.currentProduct["ShowOnHomePage"];
        this.MarkAsNew = this.currentProduct["MarkAsNew"];
        this.DisplayOrder = this.currentProduct["DisplayOrder"];
        this.StockQuantity = this.currentProduct["StockQuantity"];
        this.VendorId = this.currentProduct["VendorId"];
        console.log("Vendor Id :"+this.VendorId);

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
        console.log("Stock Quantity: "+this.StockQuantity);
        this.VendorId = this.productEditForm.value.VendorId;



        // this.currentProduct["Id"]=this.Id;
        // this.currentProduct["Name"]=this.Name;
        // this.currentProduct["FullDescription"]=this.FullDescription ;
        // this.currentProduct["Price"]=this.Price ;
        // this.currentProduct["SelectedCategoryIds"]=[+this.CategoryId]  ;
        // this.currentProduct["SelectedStoreIds"]=[+this.StoreId]  ;
        // this.currentProduct["SelectedManufacturerIds"]=[+this.ManufacturerId];
        // this.currentProduct["Sku"]=this.Sku;
        // this.currentProduct["Published"]=this.Published ;
        // this.currentProduct["Gtin"]=this.Gtin;
        // this.currentProduct["ManufacturerPartNumber"]=this.ManufacturerPartNumber;
        // this.currentProduct["ShowOnHomePage"]=this.ShowOnHomePage;
        // this.currentProduct["MarkAsNew"]=this.MarkAsNew;
        // this.currentProduct["DisplayOrder"]=this.DisplayOrder;
        // this.currentProduct["StockQuantity"]=this.StockQuantity;
        // this.currentProduct["VendorId"]=this.VendorId;

        this.updatedProduct["Id"]=this.Id;
        this.updatedProduct["Name"]=this.Name;
        this.updatedProduct["FullDescription"]=this.FullDescription ;
        this.updatedProduct["Price"]=this.Price ;
        this.updatedProduct["SelectedCategoryIds"]=[+this.CategoryId]  ;
        this.updatedProduct["SelectedStoreIds"]=[+this.StoreId]  ;
        this.updatedProduct["SelectedManufacturerIds"]=[+this.ManufacturerId];
        this.updatedProduct["Sku"]=this.Sku;
        this.updatedProduct["Published"]=this.Published ;
        this.updatedProduct["Gtin"]=this.Gtin;
        this.updatedProduct["ManufacturerPartNumber"]=this.ManufacturerPartNumber;
        this.updatedProduct["ShowOnHomePage"]=this.ShowOnHomePage;
        this.updatedProduct["MarkAsNew"]=this.MarkAsNew;
        this.updatedProduct["DisplayOrder"]=this.DisplayOrder;
        this.updatedProduct["StockQuantity"]=this.StockQuantity;
    this.updatedProduct["LastStockQuantity"]=this.currentProduct["LastStockQuantity"];
        this.updatedProduct["VendorId"]=this.VendorId;
        console.log("Current Product: "+JSON.stringify(this.updatedProduct));

        this.productService.updateProduct((this.updatedProduct))
             .subscribe(
             (response) => {
                 this.loadingProduct=false;
                 alert("Product Updated !");
             },
             (error) =>      {
                     console.log(error);
                     alert("Can't update Product ! Please refresh or check your connnection !");
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
              alert('Can\'t delete Product ! Please refresh or check your connnection !')
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
                this.totalProducts = (response.json().Total);
                console.log("Total Products:  "+this.totalProducts);

                //this.product = JSON.parse(this.products);
               // console.log((this.products));
                //  this.attribute =[this.attributes];
            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch Products ! Please refresh or check your connnection !");
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

        this.searchProductParameters={
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
}
        this.productService.searchProduct(this.searchProductParameters)
            .subscribe(
            (response) => {
                this.loading=false;
                this.showSearchedProductList=true;
                this.showProductList=false;
                this.searchedProducts = (response.json().Data);
                console.log(this.searchedProducts);
                this.productSearchForm.reset();
            },
            (error) =>      {
                    console.log(error);
                    alert("Can't search Products ! Please refresh or check your connnection !");
                  }
            );

    }
    getAttributes() {
        this.productAttributeService.getAttributes()
            .subscribe(
            (response) => {
                this.product_attributes = (response.json().Data);
                console.log("Attributes fetched");

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch Product Attributes  ! Please refresh or check your connnection !");
                  }
            );

    }
    getSpecAttributes() {
        this.productService.getSpecAttributes()
            .subscribe(
            (response) => {
                this.specification_attributes = (response.json().Data);
                console.log("Spec Attributes fetched");

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch Spec Attributes ! Please refresh or check your connnection !");
                  }
            );

    }
    getCategory() {
        this.productService.getCategory()
            .subscribe(
            (response) => {
                this.categories = (response.json().Data);
                console.log("Category fetched");
            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch Category ! Please refresh or check your connnection !");
                  }
            );
    }
    getAllData() {
        //this.getProducts(0);

        this.getCategory();
        this.getManufacturers();
        this.getStores();
        this.getVendors();
        this.getAttributes();
        this.getSpecAttributes();
    }

    showList(){
        this.editMode=false;
        this.addNewProduct = false;

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
      // this.getPicture();
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
       //this.getCurrentSpecAttributes();
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

                this.manufacturers = (response.json().Data);
                console.log("Manufacturers fetched");

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch Manufacturers ! Please refresh or check your connnection !");
                  }
            );

    }
    getVendors(){
        this.productService.getVendors()
            .subscribe(
            (response) => {

                this.vendors = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch Vendors ! Please refresh or check your connnection !");
                  }
            );
    }
    getStores(){
        this.productService.getStores()
            .subscribe(
            (response) => {

                this.stores = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch Stores ! Please refresh or check your connnection !");
                  }
            );
    }
    addProductMode(){
            this.searchProductMode=false;
      this.showProductList=false;
      this.addNewProduct = true;
      this.showToggle(1);
      this.getAllData();
    }

    getCurrentProduct(id:number){
        return this.products.filter(
          function(product){ return product.Id == id }
      );
    }
    getCurrentAttributeName(id:number){
        return this.product_attributes.filter(
          function(attribute){ return attribute.Id == id }
      );
    }
    checkCurrentAttributeId(id:number){
        return this.productAttributeFields.filter(
          function(attribute){ return attribute.ProductAttributeId == id }
      );
    }
    getCurrentAttributes(){
        this.addAttributeMode=false;
        this.productService.getCurrentAttributes(this.Id)
            .subscribe(
            (response) => {
                this.productAttributeFields = (response.json().Data);
                console.log("Attributes Retrieved");
                console.log(this.productAttributeFields);
                this.addAttributeMode = true;

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch Current Product Attribute ! Please refresh or check your connnection !");
                  }
            );

    }

    addAttribute(){

      this.current_attribute_id = this.productAttributeForm.value.current_attribute_id;
      var checkId = this.checkCurrentAttributeId(this.current_attribute_id);
      console.log(checkId);

      if(checkId.length!=0){
          alert("Attribute already present !");
      }
      else{
          this.Attribute = this.getCurrentAttributeName(this.current_attribute_id)[0].Name;

          this.Attribute_TextPrompt = this.productAttributeForm.value.TextPrompt;
          this.Attribute_IsRequired = this.productAttributeForm.value.IsRequired;
          this.Attribute_DisplayOrder = this.productAttributeForm.value.DisplayOrder;
          this.current_attribute.push({
      "Id": 0,
      "ProductId": this.Id,
      "ProductAttributeId": this.current_attribute_id,
      "ProductAttribute": this.Attribute,
      "TextPrompt": this.Attribute_TextPrompt,
      "IsRequired": this.Attribute_IsRequired,
      "AttributeControlTypeId": 7,
      "AttributeControlType": "sample string 8",
      "DisplayOrder": this.Attribute_DisplayOrder,
      "ShouldHaveValues": true,
      "TotalValues": 11,
      "ValidationRulesAllowed": true,
      "ValidationMinLength": 1,
      "ValidationMaxLength": 1,
      "ValidationFileAllowedExtensions": "sample string 13",
      "ValidationFileMaximumSize": 1,
      "DefaultValue": "sample string 14",
      "ValidationRulesString": "sample string 15",
      "ConditionAllowed": true,
      "ConditionString": "sample string 17"
    });
    this.productService.addAttribute(this.current_attribute)
        .subscribe(
        (response) => {
          this.getCurrentAttributes();
          this.current_attribute=[];
            alert("Added !");
        },
        (error) =>      {
                console.log(error);
                alert("Can't add Attribute ! Please refresh or check your connnection !");
              }
        );
      }

    }
    deleteCurrentAttribute(id: HTMLFormElement) {
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
          this.productService.deleteAttribute(+id.name)
            .subscribe(
            (data) => {

              alert('Deleted !');
              this.getCurrentAttributes();
            },
            (error) => {
              console.log(error)
              alert('Can\'t delete Attribute ! Please refresh or check your connnection !')
            }
            );
        }
    }

    importProducts(file){
        console.log(file.serverResponse.json());
    }


fileChange(event) {
    let fileList: FileList = event.target.files;
    if(fileList.length > 0) {
        let file: File = fileList[0];
        let formData:FormData = new FormData();
        console.log(file);
        this.Token = localStorage.getItem("Token");

        formData.append('importexcelfile', file, file.name);
        let headers = new Headers({ 'Accept':'application/json' ,'Authorization':'Token '+this.Token });


        let options = new RequestOptions({ headers: headers });
        this.http.post('http://denmakers3-001-site1.ctempurl.com/api/products/ImportXlsx', formData, options)
        .subscribe(
            (data) => {

              alert('Uploaded !');
              console.log(data);
              this.getProducts(this.currentPageNumber);
              //this.getCurrentAttributes();
            },
            (error) => {
              console.log(error)
             // alert('Can\'t delete Attribute ! Please refresh or check your connnection !')
            }
            );
    }
}
productExport(){
    this.Token = localStorage.getItem("Token");
        const url = 'http://denmakers3-001-site1.ctempurl.com/api/Products/ExportXlsx';

         let headers = new Headers({'Content-Type':'application/x-www-form-urlencoded', 'Accept':'application/json' ,'Authorization':'Token '+this.Token });
         let options = new RequestOptions({responseType: ResponseContentType.Blob, headers });


         this.http.get(url, options)
         .map(res => res.blob())
         .subscribe(
         data => {
 FileSaver.saveAs(data, 'Export.xlsx');
         },
         err => {
             console.log('error');
             console.error(err);
         });

}
getCurrentCategory(){

}
}
