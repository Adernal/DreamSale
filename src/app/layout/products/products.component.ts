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
    @ViewChild('u') specAttributeForm: NgForm;
    @ViewChild('p') pictureForm: NgForm;

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
    pictureId:number;
    imageUrl:string;
    pictureList;
    currentPicture=[];
    pictureDisplayOrder:number;
    addSpecAttributeMode:boolean;
    Spec_Attribute_Id='';
    ValueRaw='';



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
        this.addAttributeMode=false;
        this.showCurrentAttributeList=true;
        this.showCurrentSpecAttributeList=true;
        this.showCurrentAttributeForm=false;
        this.showCurrentSpecAttributeForm=false;
        this.pictureId=0;
        this.imageUrl='';
        this.pictureDisplayOrder=0;
        this.addSpecAttributeMode=false;

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

        this.getCategory();
        this.getManufacturers();
        this.getStores();
        this.getVendors();
        this.getAttributes();
        this.getSpecAttributes();
    }

    // editProductAttribute(prod_id,attr_id){
    //   console.log(prod_id,attr_id);
    //   this.editAttributeMode=true;
    //   this.current_Id =prod_id;
    //   this.current_attribute_id =attr_id;
    //   this.current_attribute = this.product[prod_id].product_attributes[attr_id];
    //   this.current_attribute_description = this.product[prod_id].product_attributes[attr_id].description;
    //   console.log(this.current_attribute);
    // }
    // saveAttribute(){
    //   this.current_attribute_description = this.attributeForm.value.prod_attrib;
    //   this.product[+this.current_Id].product_attributes[this.current_attribute_id].description = this.current_attribute_description;
    //   localStorage.setItem("products",JSON.stringify(this.product));
    //   console.log("Up")
    //
    // }
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
       this.getPicture();
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
            this.searchProductMode=false;
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
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );

    }
    getCurrentSpecAttributes(){
        this.addSpecAttributeMode=false;
        this.loading=true;
        this.productService.getCurrentSpecAttributes(this.Id)
            .subscribe(
            (response) => {
                this.specAttributeFields = (response.json().Data);
                this.addSpecAttributeMode=true
                this.loading=false;
            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );
    }
    // addCurrentAttributeMode(){
    //     this.addAttributeMode=false;
    //     this.showCurrentAttributeList=false;
    //     this.showCurrentAttributeForm=true;
    //
    // }
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
                alert("Can't fetch data ! Please refresh or check your connnection !");
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
              alert('Can\'t fetch data ! Please refresh or check your connnection !')
            }
            );
        }
    }
    getPictureDetails(file){
        this.pictureId = file.serverResponse.json().pictureId;
        this.imageUrl = file.serverResponse.json().imageUrl;
    }
addPicture(){


     this.pictureDisplayOrder = this.pictureForm.value.DisplayOrder;
     this.currentPicture.push({
  "Id": 0,
  "CustomProperties": {
    "sample string 1": {},
    "sample string 3": {}
  },
  "ProductId": this.Id,
  "PictureId": this.pictureId,
  "PictureUrl": this.imageUrl,
  "DisplayOrder": this.DisplayOrder,
  "OverrideAltAttribute": "sample string 6",
  "OverrideTitleAttribute": "sample string 7"
});
     this.productService.addPicture(this.currentPicture)
     .subscribe(
     (response) => {
         alert("Added !");
         this.getPicture();
         this.pictureForm.reset();
         this.currentPicture=[];
     },
     (error) =>      {
             console.log(error);
             alert("Can't fetch data ! Please refresh or check your connnection !");
           }
     );
}
getPicture(){
    this.loading=true;
    this.productService.getPicture(this.Id)
    .subscribe(
    (response) => {

        this.pictureList = (response.json().Data);
        this.loading=false;
        console.log(this.pictureList);
    },
    (error) =>      {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
          }
    );
}
deletePicture(id:HTMLFormElement){
    var confirmation = confirm("Are you sure you want to delete ?");
    if (confirmation) {
      this.productService.deletePicture(+id.name)
        .subscribe(
        (data) => {

          alert('Deleted !');
          this.pictureForm.reset();
          this.getPicture();
        },
        (error) => {
          console.log(error)
          alert('Can\'t fetch data ! Please refresh or check your connnection !')
        }
        );
    }
}
addSpecAttribute(){
     this.current_spec_attribute_id = this.specAttributeForm.value.current_spec_attribute_id;
     this.Spec_Attribute_Id = this.getCurrentSpecAttributeName(this.current_spec_attribute_id)[0].Name;
     this.ValueRaw = this.specAttributeForm.value.ValueRaw;
     this.current_spec_attribute.push();
}
getCurrentSpecAttributeName(id:number){
    return this.specification_attributes.filter(
      function(attribute){ return attribute.Id == id }
  );
}
}
