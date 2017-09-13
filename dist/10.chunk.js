webpackJsonp([10],{

/***/ "../../../../../src/app/layout/vendors/filter.pipe.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return FilterPipe; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var FilterPipe = (function () {
    function FilterPipe() {
    }
    FilterPipe.prototype.transform = function (value, filteredVendor, propName, propName2) {
        // if(value.length===0 || filteredVendor===''){
        //   return value;
        // }
        // // this.prop1 = propName[0];
        // // this.prop2 =propName[1];
        // const resultArray=[];
        // for(const item of value){
        //     if((item[propName].toLowerCase().indexOf(filteredVendor.toLowerCase())>=0) || (item[propName2].toLowerCase().indexOf(filteredVendor.toLowerCase())>=0)){
        //       resultArray.push(item);
        //     }
        //
        // }
        // return resultArray;
    };
    return FilterPipe;
}());
FilterPipe = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Pipe"])({
        name: 'filter',
        pure: false,
    })
], FilterPipe);

//# sourceMappingURL=filter.pipe.js.map

/***/ }),

/***/ "../../../../../src/app/layout/vendors/vendor.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return VendorService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var VendorService = (function () {
    function VendorService(http) {
        this.http = http;
    }
    VendorService.prototype.storeVendor = function (Vendor) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        this.temp = Vendor[Vendor.length - 1];
        console.log(this.temp);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/vendors/CreateVendor', this.temp, { headers: headers });
    };
    VendorService.prototype.getVendor = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors');
    };
    VendorService.prototype.updateVendor = function (vendor) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log(vendor);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/vendors/EditVendor', vendor, { headers: headers });
    };
    VendorService.prototype.deleteVendor = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors/Delete?id=' + id, null, { headers: headers });
    };
    return VendorService;
}());
VendorService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], VendorService);

var _a;
//# sourceMappingURL=vendor.service.js.map

/***/ }),

/***/ "../../../../../src/app/layout/vendors/vendors-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__vendors_component__ = __webpack_require__("../../../../../src/app/layout/vendors/vendors.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return VendorsRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



// import { ProductTagsComponent } from './product-tags/product-tags.component';
var routes = [
    { path: '', component: __WEBPACK_IMPORTED_MODULE_2__vendors_component__["a" /* VendorsComponent */] },
];
var VendorsRoutingModule = (function () {
    function VendorsRoutingModule() {
    }
    return VendorsRoutingModule;
}());
VendorsRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */].forChild(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */]]
    })
], VendorsRoutingModule);

//# sourceMappingURL=vendors-routing.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/vendors/vendors.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\n<h1> Vendor Management </h1>\n     <hr>\n    <div class=\"row\">\n\n\n      <form role=\"form\" (ngSubmit)=\"addVendor(f)\" #f=\"ngForm\" class=\"container\" *ngIf=\"!editMode\">\n        <div class=\"row\">\n          <div class=\"col-lg-6\">\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Vendor</label>\n                <input class=\"form-control\" ngModel name=\"Name\" required placeholder=\"Enter Vendor Name\" >\n\n                <!-- <p class=\"help-block\">Example block-level help text here.</p> -->\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n              <label class=\"card-header\">Vendor Description</label>\n                <textarea class=\"form-control\" rows=\"3\"\n\n                ngModel\n                 name=\"Description\"\n\n\n                placeholder=\"Enter vendor Description\"\n\n                  required\n                ></textarea>\n\n          </fieldset>\n          <fieldset class=\"form-group card mb-3\">\n              <label class=\"card-header\">Picture</label>\n              <image-upload [max]=\"1\" [url]=\"'http://piyushdaftary-001-site1.ctempurl.com/api/Pictures/upload'\" [buttonCaption]=\"'Select Images!'\" [extensions]=\"['jpg','png','gif']\" (onFileUploadFinish)=\"getPictureDetails($event)\"></image-upload>\n          </fieldset>\n          <fieldset class=\"form-group card mb-3\">\n\n            <label class=\"card-header\">Display Order</label>\n            <input type=\"number\" class=\"form-control\" ngModel name=\"Display_Order\" required placeholder=\"Enter Display Order\" min=\"1\" step=\"1\"\n            >\n\n          </fieldset>\n          <fieldset class=\"form-group card mb-3\">\n  <label class=\"card-header\">Active</label>\n            <div *ngIf=\"!editMode\" class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n              <input type=\"checkbox\" class=\"form-control\" ngModel name=\"Active\">\n            </div>\n\n\n\n          </fieldset>\n\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"!editMode\">Add Vendor</button>\n\n        </div>\n        <div class=\"col-lg-6\">\n          <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Email</label>\n\n            <input type=\"email\" class=\"form-control\" ngModel name=\"Email\" required placeholder=\"Email\" min=\"1\" step=\"1\"\n            >\n\n          </fieldset>\n\n          <!-- <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Address</label>\n\n                        <input type=\"text\" class=\"form-control\" ngModel name=\"Address\" required placeholder=\"Address\" min=\"1\" step=\"1\"\n                        *ngIf=\"!editMode\">\n                        <input type=\"text\" class=\"form-control\" [(ngModel)]=\"Address\" name=\"Address\" required placeholder=\"Address\" min=\"1\" step=\"1\"\n                        *ngIf=\"editMode\">\n\n\n          </fieldset> -->\n          <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Admin Comment</label>\n            <textarea class=\"form-control\" ngModel name=\"AdminComment\" required placeholder=\"AdminComment\" rows=\"3\"\n            ></textarea>\n\n          </fieldset>\n\n\n\n        </div>\n\n\n  </div>\n\n\n</form>\n<form role=\"form\" (ngSubmit)=\"editVendor(f)\" #f=\"ngForm\" class=\"container\" *ngIf=\"editMode\">\n  <div class=\"row\">\n    <div class=\"col-lg-6\">\n      <fieldset class=\"form-group card mb-3\">\n          <label class=\"card-header\">Vendor</label>\n\n          <input class=\"form-control\" [(ngModel)]=\"Name\" name=\"Name\" required value=\"{{Name}}\">\n          <!-- <p class=\"help-block\">Example block-level help text here.</p> -->\n      </fieldset>\n      <fieldset class=\"form-group card mb-3\">\n        <label class=\"card-header\">Vendor Description</label>\n\n          <textarea class=\"form-control\" rows=\"3\"\n\n          [(ngModel)]=\"Description\"\n           name=\"Description\"\n\n          required\n\n          value=\"{{Description}}\" *ngIf=\"editMode\"></textarea>\n    </fieldset>\n    <fieldset class=\"form-group card mb-3\">\n\n      <label class=\"card-header\">Display Order</label>\n\n      <input type=\"number\" class=\"form-control\" [(ngModel)]=\"Display_Order\" name=\"Display_Order\" required placeholder=\"Enter Display Order\" min=\"1\" step=\"1\"\n      *ngIf=\"editMode\">\n    </fieldset>\n    <fieldset class=\"form-group card mb-3\">\n<label class=\"card-header\">Active</label>\n\n      <div  class=\"checkbox\" style=\"float:left; margin:5px;\">\n        <input type=\"checkbox\" class=\"form-control\" [(ngModel)]=\"Active\" name=\"Active\">\n      </div>\n\n    </fieldset>\n\n\n      <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\">Edit Vendor</button>\n      \n\n  </div>\n  <div class=\"col-lg-6\">\n    <fieldset class=\"form-group card mb-3\">\n      <label class=\"card-header\">Email</label>\n\n      <input type=\"email\" class=\"form-control\" [(ngModel)]=\"Email\" name=\"Email\" required placeholder=\"Email\" min=\"1\" step=\"1\"\n      >\n\n    </fieldset>\n\n    <!-- <fieldset class=\"form-group card mb-3\">\n      <label class=\"card-header\">Address</label>\n\n                  <input type=\"text\" class=\"form-control\" ngModel name=\"Address\" required placeholder=\"Address\" min=\"1\" step=\"1\"\n                  *ngIf=\"!editMode\">\n                  <input type=\"text\" class=\"form-control\" [(ngModel)]=\"Address\" name=\"Address\" required placeholder=\"Address\" min=\"1\" step=\"1\"\n                  *ngIf=\"editMode\">\n\n\n    </fieldset> -->\n    <fieldset class=\"form-group card mb-3\">\n      <label class=\"card-header\">Admin Comment</label>\n\n      <textarea class=\"form-control\" [(ngModel)]=\"AdminComment\" name=\"AdminComment\" required placeholder=\"AdminComment\"\n      >\n    </textarea>\n\n    </fieldset>\n\n\n\n  </div>\n\n\n</div>\n\n\n</form>\n  </div>\n  </div>\n <hr>\n    <div class=\"row\">\n\n\n            <div class=\"card mb-3\">\n                <div class=\"card-header\">\n                  <h2>Vendor List</h2>\n                  <!-- <input type=\"text\" placeholder=\"Search By Name or Email\" [(ngModel)]=\"filteredVendor\" style=\"width:100%;\"> -->\n                </div>\n                <div class=\"card-block\" style=\"width:100%;\">\n                    <table class=\"table table-bordered\" style=\"table-layout:fixed\">\n                        <thead class=\"thead-inverse\">\n                        <tr>\n                            <th>Vendor Id</th>\n                            <th>Vendor Name   </th>\n                            <th>Vendor Description</th>\n                            <th>Display Order</th>\n                            <th>Email</th>\n\n                            <th>Admin Comment</th>\n                            <th>Active</th>\n                            <th>Action</th>\n\n\n                        </tr>\n                        </thead>\n                        <tbody>\n                          <tr *ngFor =\" let i = index ;let vendor of vendors\" >\n\n                          <td>{{vendor.Id}}</td>\n                          <td>{{vendor.Name}}</td>\n                          <td>{{vendor.Description}}</td>\n                          <td>{{vendor.DisplayOrder}}</td>\n                          <td>{{vendor.Email}}</td>\n\n                          <td>{{vendor.AdminComment}}</td>\n                          <td *ngIf=\"vendor.Active\">Yes</td>\n                          <td *ngIf=\"!vendor.Active\">No</td>\n\n                          <td><button type=\"button\" name=\"{{vendor.Id}}\" class=\"btn btn-primary\" (click)=\"editVendorMode(c)\" #c><i class=\"fa fa-edit\"></i></button>\n                          <button type=\"button\" name=\"{{vendor.Id}}\" class=\"btn btn-danger\" (click)=\"deleteVendor(d)\" #d><i class=\"fa fa-times\"></i></button></td>\n                        </tr>\n\n                        </tbody>\n                    </table>\n                </div>\n            </div>\n\n\n\n    </div>\n"

/***/ }),

/***/ "../../../../../src/app/layout/vendors/vendors.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/layout/vendors/vendors.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__vendor_service__ = __webpack_require__("../../../../../src/app/layout/vendors/vendor.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return VendorsComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var VendorsComponent = (function () {
    // filteredEmail='';
    function VendorsComponent(vendorService) {
        this.vendorService = vendorService;
        this.submitted = false;
        this.vendor = [];
        this.Name = '';
        this.editMode = false;
        this.filteredVendor = '';
        this.PictureId = 0;
        this.imageUrl = '';
    }
    VendorsComponent.prototype.ngOnInit = function () {
        //   if(localStorage.getItem("vendors")!=null){
        //   this.vendor = JSON.parse(localStorage.getItem("vendors"));
        //
        // }
        this.getVendor();
    };
    VendorsComponent.prototype.addVendor = function () {
        var _this = this;
        this.submitted = true;
        this.Name = this.vendorForm.value.Name;
        this.Description = this.vendorForm.value.Description;
        this.Email = this.vendorForm.value.Email;
        this.AdminComment = this.vendorForm.value.AdminComment;
        //  this.Address = this.vendorForm.value.Address;
        this.Display_Order = this.vendorForm.value.Display_Order;
        this.Active = this.vendorForm.value.Active;
        console.log(this.Active);
        // this.Active = this.vendorForm.value.Active;
        this.vendor.push({
            "Id": 0,
            "Name": this.Name,
            "Email": this.Email,
            "Description": this.Description,
            "PictureId": this.PictureId,
            "AdminComment": this.AdminComment,
            "Address": {
                "Id": 1,
                "CustomProperties": {
                    "sample string 1": {},
                    "sample string 3": {}
                },
                "Active": this.Active,
                "DisplayOrder": this.Display_Order,
            }
        });
        //  localStorage.setItem("vendors", JSON.stringify(this.vendor));
        this.vendorService.storeVendor(this.vendor)
            .subscribe(function (data) {
            console.log(data);
            alert("Added !");
            _this.vendorForm.reset();
            _this.getVendor();
        }, function (error) {
            alert("Failed to add !");
            console.log(error);
        });
        //    alert("Added !");
        // console.log(this.vendor);
        // this.vendorForm.reset();
    };
    VendorsComponent.prototype.editVendor = function () {
        var _this = this;
        this.editMode = false;
        this.Name = this.vendorForm.value.Name;
        this.Description = this.vendorForm.value.Description;
        this.Email = this.vendorForm.value.Email;
        this.AdminComment = this.vendorForm.value.AdminComment;
        this.Display_Order = this.vendorForm.value.Display_Order;
        this.Active = this.vendorForm.value.Active;
        this.vendor["Name"] = this.Name;
        this.vendor["Description"] = this.Description;
        this.vendor["Email"] = this.Email;
        this.vendor["AdminComment"] = this.AdminComment;
        this.vendor["DisplayOrder"] = this.Display_Order;
        this.vendor["Active"] = this.Active;
        this.vendorService.updateVendor(this.vendor)
            .subscribe(function (data) {
            console.log(data);
            alert("Edited !");
            _this.vendorForm.reset();
            _this.getVendor();
        }, function (error) {
            alert("Failed to edit !");
            console.log(error);
        });
    };
    VendorsComponent.prototype.editVendorMode = function (id) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        this.vendor = this.getCurrentVendor(this.Id)[0];
        // console.log(this.vendor[1].Name);
        this.Name = this.vendor["Name"];
        this.Description = this.vendor["Description"];
        this.Email = this.vendor["Email"];
        this.AdminComment = this.vendor["AdminComment"];
        this.Display_Order = this.vendor["DisplayOrder"];
        this.Active = this.vendor["Active"];
    };
    VendorsComponent.prototype.deleteVendor = function (id) {
        var _this = this;
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.vendorService.deleteVendor(+id.name)
                .subscribe(function (data) {
                alert('Deleted !');
                _this.getVendor();
            }, function (error) {
                console.log(error);
                alert('Can\'t fetch data ! Please refresh or check your connnection !');
            });
        }
    };
    VendorsComponent.prototype.getVendor = function () {
        var _this = this;
        this.vendorService.getVendor()
            .subscribe(function (response) {
            _this.vendors = (response.json().Data);
            //this.vendor = this.vendors.Data;
            console.log((_this.vendors));
            //  this.attribute =[this.attributes];
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    VendorsComponent.prototype.getCurrentVendor = function (id) {
        return this.vendors.filter(function (vendor) { return vendor.Id == id; });
    };
    VendorsComponent.prototype.getPictureDetails = function (file) {
        this.PictureId = file.serverResponse.json().pictureId;
        this.imageUrl = file.serverResponse.json().imageUrl;
    };
    return VendorsComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('f'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _a || Object)
], VendorsComponent.prototype, "vendorForm", void 0);
VendorsComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app-vendors',
        template: __webpack_require__("../../../../../src/app/layout/vendors/vendors.component.html"),
        styles: [__webpack_require__("../../../../../src/app/layout/vendors/vendors.component.scss")]
    })
    /* This is still in development ! Has a lot bugs ! */
    ,
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__vendor_service__["a" /* VendorService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__vendor_service__["a" /* VendorService */]) === "function" && _b || Object])
], VendorsComponent);

var _a, _b;
//# sourceMappingURL=vendors.component.js.map

/***/ }),

/***/ "../../../../../src/app/layout/vendors/vendors.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__vendors_routing_module__ = __webpack_require__("../../../../../src/app/layout/vendors/vendors-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__vendors_component__ = __webpack_require__("../../../../../src/app/layout/vendors/vendors.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared__ = __webpack_require__("../../../../../src/app/shared/index.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__filter_pipe__ = __webpack_require__("../../../../../src/app/layout/vendors/filter.pipe.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__vendor_service__ = __webpack_require__("../../../../../src/app/layout/vendors/vendor.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__node_modules_angular2_image_upload_src_image_upload_module__ = __webpack_require__("../../../../angular2-image-upload/src/image-upload.module.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VendorsModule", function() { return VendorsModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};









//import { FilterPipe } from './filter.pipe';
// import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
//import { Vendorservice } from './product.service';
// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';
var VendorsModule = (function () {
    function VendorsModule() {
    }
    return VendorsModule;
}());
VendorsModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["h" /* CommonModule */],
            __WEBPACK_IMPORTED_MODULE_2__vendors_routing_module__["a" /* VendorsRoutingModule */],
            __WEBPACK_IMPORTED_MODULE_4__shared__["b" /* PageHeaderModule */],
            __WEBPACK_IMPORTED_MODULE_5__angular_forms__["a" /* FormsModule */],
            __WEBPACK_IMPORTED_MODULE_8__node_modules_angular2_image_upload_src_image_upload_module__["a" /* ImageUploadModule */].forRoot()
            // ImageUploadModule.forRoot()
            // ProductTagsModule
        ],
        providers: [__WEBPACK_IMPORTED_MODULE_7__vendor_service__["a" /* VendorService */]],
        declarations: [__WEBPACK_IMPORTED_MODULE_3__vendors_component__["a" /* VendorsComponent */], __WEBPACK_IMPORTED_MODULE_6__filter_pipe__["a" /* FilterPipe */]]
    })
], VendorsModule);

//# sourceMappingURL=vendors.module.js.map

/***/ })

});
//# sourceMappingURL=10.chunk.js.map