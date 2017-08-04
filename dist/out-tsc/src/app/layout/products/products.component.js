"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/http");
var product_service_1 = require("./product.service");
var ProductsComponent = (function () {
    function ProductsComponent(http, productService) {
        this.http = http;
        this.productService = productService;
        this.multiple = false;
        // @ViewChild('i') editFormMode :NgForm;
        // @ViewChild('e') editForm :NgForm;
        this.submitted = false;
        this.editMode = false;
        this.product = [];
        this.product_name = '';
        this.product_description = '';
        this.filteredProduct = '';
        this.editAttributeMode = false;
        this.editSpecAttributeMode = false;
    }
    ProductsComponent.prototype.ngOnInit = function () {
        //localStorage.removeItem("products");
        if (localStorage.getItem("products") != null) {
            this.product = JSON.parse(localStorage.getItem("products"));
        }
        //  this.categories = JSON.parse(localStorage.getItem("categories"));
        this.tags = JSON.parse(localStorage.getItem("tags"));
        this.getAllData();
        // this.product_attributes = JSON.parse(localStorage.getItem("attributes"));
        // this.specification_attributes = JSON.parse(localStorage.getItem("spec-attributes"));
    };
    ProductsComponent.prototype.addProduct = function () {
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
    };
    ProductsComponent.prototype.editProductMode = function (id) {
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
    };
    ProductsComponent.prototype.editProduct = function () {
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
    };
    ProductsComponent.prototype.deleteProduct = function (id) {
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.product_id = +this.product[+id.name].product_id;
            this.getAllData();
            this.product = JSON.parse(localStorage.getItem("products"));
            this.product.splice(+id.name, 1);
            localStorage.setItem("products", JSON.stringify(this.product));
            alert("Deleted !");
            if (this.editMode) {
                this.editMode = false;
            }
        }
    };
    ProductsComponent.prototype.getAttributes = function () {
        var _this = this;
        this.productService.getAttributes()
            .subscribe(function (response) {
            _this.product_attributes = (response.json());
            _this.product_attribute = _this.product_attributes.Data;
            console.log((_this.product_attribute));
            //  this.attribute =[this.attributes];
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.getSpecAttributes = function () {
        var _this = this;
        this.productService.getSpecAttributes()
            .subscribe(function (response) {
            _this.specification_attributes = (response.json());
            _this.specification_attribute = _this.specification_attributes.Data;
            console.log((_this.specification_attribute));
            //  this.attribute =[this.attributes];
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.getCategory = function () {
        var _this = this;
        this.productService.getCategory()
            .subscribe(function (response) {
            _this.categories = (response.json().Data);
            //this.specification_attribute = this.specification_attributes.Data;
            console.log((_this.categories));
            //  this.attribute =[this.attributes];
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.getAllData = function () {
        this.getAttributes();
        this.getSpecAttributes();
        this.getCategory();
    };
    return ProductsComponent;
}());
__decorate([
    core_1.ViewChild('f'),
    __metadata("design:type", forms_1.NgForm)
], ProductsComponent.prototype, "productForm", void 0);
__decorate([
    core_1.ViewChild('g'),
    __metadata("design:type", forms_1.NgForm)
], ProductsComponent.prototype, "attributeForm", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Boolean)
], ProductsComponent.prototype, "multiple", void 0);
ProductsComponent = __decorate([
    core_1.Component({
        selector: 'app-products',
        templateUrl: './products.component.html',
        styleUrls: ['./products.component.scss']
    })
    /* This is still in development ! Has bugs ! */
    ,
    __metadata("design:paramtypes", [http_1.Http, product_service_1.ProductService])
], ProductsComponent);
exports.ProductsComponent = ProductsComponent;
//# sourceMappingURL=products.component.js.map