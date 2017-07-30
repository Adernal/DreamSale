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
    @ViewChild('f') productForm: NgForm;
    @ViewChild('g') attributeForm: NgForm;
    @Input() multiple: boolean = false;
    // @ViewChild('i') editFormMode :NgForm;
    // @ViewChild('e') editForm :NgForm;
    submitted = false;
    editMode = false;
    product = [];
    product_name = '';
    product_description = '';
    product_price: Number;
    product_id: Number;
    filteredProduct = '';
    tag;
    category;
    categories;
    tags;
    product_attribute;
    product_attributes;
    current_product_id;
    current_attribute_id;
    current_attribute;
    current_attribute_description;
    specification_attribute;
    specification_attributes;
    current_spec_attribute;
    current_spec_attribute_description;
    products;

    editAttributeMode = false;
    editSpecAttributeMode = false


    constructor(private http: Http, private productService: ProductService) { }

    ngOnInit() {
        //localStorage.removeItem("products");
        if (localStorage.getItem("products") != null) {
            this.product = JSON.parse(localStorage.getItem("products"));

        }
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
                this.product_id = 1;
            }
            else {
                this.product_id = +this.product[this.product.length - 1].product_id + 1;
            }
            this.product_name = this.productForm.value.product_name;
            this.product_description = this.productForm.value.product_description;
            this.product_price = this.productForm.value.product_price;
            this.category = this.productForm.value.category;
            this.tag = (this.productForm.value.tag_name);
            this.product_attribute = this.productForm.value.prod_attributes;
            this.specification_attribute = this.productForm.value.spec_attributes;
            this.product.push({
                'product_id': this.product_id,
                'product_name': this.product_name,
                'product_description': this.product_description,
                'product_price': this.product_price,
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
        this.product_id = +id.name;
        console.log(this.product_id);
        // console.log(this.product[1].product_name);p
        this.product_name = this.product[+this.product_id].product_name;
        this.product_description = this.product[+this.product_id].product_description;
        this.product_price = this.product[+this.product_id].product_price;
        this.category = this.product[+this.product_id].category;
        this.tag = (this.product[+this.product_id].tag);
        // this.product_attribute = (this.product[+this.product_id].product_attributes);
        // this.specification_attribute = (this.product[+this.product_id].specification_attributes);

    }
    editProduct() {
        this.editMode = false;
        this.product_name = this.productForm.value.product_name;
        this.product_description = this.productForm.value.product_description;
        this.product_price = this.productForm.value.product_price;
        this.category = this.productForm.value.category;
        this.tag = this.productForm.value.tag_name;
        this.product_attribute = this.productForm.value.prod_attributes;
        this.specification_attribute = this.productForm.value.spec_attributes;

        this.product[+this.product_id].product_name = this.product_name;
        this.product[+this.product_id].product_description = this.product_description;
        this.product[+this.product_id].product_price = this.product_price;
        this.product[+this.product_id].category = this.category;
        this.product[+this.product_id].tag = (this.tag);
        this.product[+this.product_id].product_attributes = (this.product_attribute);
        this.product[+this.product_id].specification_attributes = (this.specification_attribute);
        this.getAllData();

        localStorage.setItem("products", JSON.stringify(this.product));
        alert("Edited !");


    }
    deleteProduct(id: HTMLFormElement) {
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.product_id = +this.product[+id.name].product_id;
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
        this.getAttributes();
        this.getSpecAttributes();
        this.getCategory();
    }
    // editProductAttribute(prod_id,attr_id){
    //   console.log(prod_id,attr_id);
    //   this.editAttributeMode=true;
    //   this.current_product_id =prod_id;
    //   this.current_attribute_id =attr_id;
    //   this.current_attribute = this.product[+prod_id].product_attributes[attr_id];
    //   this.current_attribute_description = this.product[+prod_id].product_attributes[attr_id].description;
    //   console.log(this.current_attribute);
    // }
    // saveAttribute(){
    //   this.current_attribute_description = this.attributeForm.value.prod_attrib;
    //   this.product[+this.current_product_id].product_attributes[this.current_attribute_id].description = this.current_attribute_description;
    //   localStorage.setItem("products",JSON.stringify(this.product));
    //   console.log("Up")
    //
    // }


}
