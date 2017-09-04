import { Component, OnInit, ViewChild, Input, ElementRef } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { CategoryComponent } from '../category/category.component';
import { Http } from '@angular/http';
import { ProductService } from './product.service';
@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss']
})

/* This is still in development ! Has bugs ! */
export class ProductsComponent implements OnInit {
    //@Input() categories :CategoryComponent;
    currentPageNumber:number=1;
    @ViewChild('f') productForm: NgForm;
    @ViewChild('g') attributeForm: NgForm;
    @Input() multiple: boolean = false;
    // @ViewChild('i') editFormMode :NgForm;
    // @ViewChild('e') editForm :NgForm;
    submitted = false;
    editMode = false;
    product = [];
    Name = '';
    product_description = '';
    Price: Number;
    Id: Number;
    filteredProduct = '';
    tag;
    category;
    categories;
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
    product_vm=[];

    editAttributeMode = false;
    editSpecAttributeMode = false


    constructor(private http: Http, private productService: ProductService) { }

    ngOnInit() {
    //    localStorage.removeItem("products");
        // if (localStorage.getItem("products") != null) {
        //     this.product = JSON.parse(localStorage.getItem("products"));
        //
        // }
        //  this.categories = JSON.parse(localStorage.getItem("categories"));
        this.tags = JSON.parse(localStorage.getItem("tags"));
        this.getAllData();
        // this.product_attributes = JSON.parse(localStorage.getItem("attributes"));
        // this.specification_attributes = JSON.parse(localStorage.getItem("spec-attributes"));

    }
    addProduct() {
        this.submitted = true;
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
            this.product_description = this.productForm.value.product_description;
            this.Price = this.productForm.value.Price;
            this.category = this.productForm.value.category;
            this.tag = (this.productForm.value.tag_name);
            this.product_attribute = this.productForm.value.prod_attributes;
            this.specification_attribute = this.productForm.value.spec_attributes;
            this.product.push({
                'Id': this.Id,
                'Name': this.Name,
                'product_description': this.product_description,
                'Price': this.Price,
                'category': this.category,
                'tag': this.tag,
                'product_attributes': this.product_attribute,
                'specification_attributes': this.specification_attribute
            });
            localStorage.setItem("products", JSON.stringify(this.product));
            alert("Added !");
            //  this.products.product=product;
            console.log(this.product);
            this.productForm.reset();
            this.getAllData();

        }
    }
    editProductMode(id: HTMLFormElement) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        // console.log(this.product[1].Name);p
        this.Name = this.product[+this.Id].Name;
        this.product_description = this.product[+this.Id].product_description;
        this.Price = this.product[+this.Id].Price;
        this.category = this.product[+this.Id].category;
        this.tag = (this.product[+this.Id].tag);
        // this.product_attribute = (this.product[+this.Id].product_attributes);
        // this.specification_attribute = (this.product[+this.Id].specification_attributes);

    }
    editProduct() {
        this.editMode = false;
        this.Name = this.productForm.value.Name;
        this.product_description = this.productForm.value.product_description;
        this.Price = this.productForm.value.Price;
        this.category = this.productForm.value.category;
        this.tag = this.productForm.value.tag_name;
        this.product_attribute = this.productForm.value.prod_attributes;
        this.specification_attribute = this.productForm.value.spec_attributes;

        this.product[+this.Id].Name = this.Name;
        this.product[+this.Id].product_description = this.product_description;
        this.product[+this.Id].Price = this.Price;
        this.product[+this.Id].category = this.category;
        this.product[+this.Id].tag = (this.tag);
        this.product[+this.Id].product_attributes = (this.product_attribute);
        this.product[+this.Id].specification_attributes = (this.specification_attribute);
        this.getAllData();

        localStorage.setItem("products", JSON.stringify(this.product));
        alert("Edited !");


    }
    deleteProduct(id: HTMLFormElement) {
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.Id = +this.product[+id.name].Id;
            this.getAllData();

            this.product = JSON.parse(localStorage.getItem("products"));
            this.product.splice(+id.name, 1);
            localStorage.setItem("products", JSON.stringify(this.product));
            alert("Deleted !");
            if(this.editMode){
                this.editMode=false;

            }

        }
    }
    getProducts(){
      this.product_vm=[{
  "Id": 1,
  "CustomProperties": {
    "sample string 1": {},
    "sample string 3": {}
  },
  "SearchProductName": "sample string 2",
  "SearchCategoryId": 3,
  "SearchIncludeSubCategories": true,
  "SearchManufacturerId": 5,
  "SearchStoreId": 6,
  "SearchVendorId": 7,
  "SearchWarehouseId": 8,
  "SearchProductTypeId": 9,
  "SearchPublishedId": 10,
  "GoDirectlyToSku": "sample string 11",
  "IsLoggedInAsVendor": true,
  "AllowVendorsToImportProducts": true,
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
}];
        this.productService.getAllProducts(this.product_vm)
            .subscribe(
            (response) => {
                this.products = (response.json());
                console.log(this.products.Name);
                //this.product = JSON.parse(this.products);
                console.log("Products:"+(this.product));
                //  this.attribute =[this.attributes];
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
                this.product_attributes = (response.json());
                this.product_attribute = this.product_attributes.Data;
                console.log((this.product_attribute));
                //  this.attribute =[this.attributes];
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
                this.specification_attributes = (response.json());
                this.specification_attribute = this.specification_attributes.Data;
                console.log((this.specification_attribute));
                //  this.attribute =[this.attributes];
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
                //this.specification_attribute = this.specification_attributes.Data;
                console.log((this.categories));
                //  this.attribute =[this.attributes];
            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );
    }
    getAllData() {
        this.getProducts();
        this.getAttributes();
        this.getSpecAttributes();
        this.getCategory();

    }
    // showAttributeDescription(){
    //     alert("HI");
    //   this.editAttributeMode=true;
    //
    // }
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


}
