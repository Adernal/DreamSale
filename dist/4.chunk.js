webpackJsonp([4],{

/***/ "../../../../../src/app/layout/products/filter.pipe.ts":
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
    FilterPipe.prototype.transform = function (value, filteredProduct, propName, propName2) {
        if (value.length === 0 || filteredProduct === '') {
            return value;
        }
        var resultArray = [];
        for (var _i = 0, value_1 = value; _i < value_1.length; _i++) {
            var item = value_1[_i];
            if (item[propName].toLowerCase().indexOf(filteredProduct.toLowerCase()) >= 0 ||
                item[propName2].toLowerCase().indexOf(filteredProduct.toLowerCase()) >= 0) {
                resultArray.push(item);
            }
        }
        return resultArray;
    };
    return FilterPipe;
}());
FilterPipe = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Pipe"])({
        name: 'filter',
        pure: false
    })
], FilterPipe);

//# sourceMappingURL=filter.pipe.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/link-product-spec-attributes/link-product-spec-attributes.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"container-fluid\">\n    <h2>Specification Attributes</h2>\n    <hr>\n\n    <div class=\"row\">\n        <div class=\"col-lg-6 center-block\" style=\"float: none; margin: 0 auto;\">\n            <form role=\"form\" (ngSubmit)=\"addSpecAttribute(s)\" #s=\"ngForm\">\n                <fieldset class=\"form-group card mb-3\">\n                    <label class=\"card-header\">Attribute</label>\n                    <select class=\"form-control\" ngModel name=\"current_attribute_id\">\n                    <option *ngFor=\"let attribute of attributeList\" value=\"{{attribute.Id}}\">\n                      {{attribute.ProductAttribute}}\n                    <hr>\n                  </option>\n  </select>\n                </fieldset>\n                <fieldset class=\"form-group card mb-3\">\n                    <label class=\"card-header\">Specification</label>\n                    <select class=\"form-control\" ngModel name=\"current_spec_attribute_id\">\n      <option *ngFor=\"let spec_attribute of specAttributeList\" value=\"{{spec_attribute.Id}}\">\n        {{spec_attribute.Name}}\n      <hr></option>\n  </select>\n                </fieldset>\n\n                <fieldset class=\"form-group card mb-3\">\n                    <label class=\"card-header\">Value</label>\n                    <input type=\"text\" class=\"form-control\" rows=\"3\" ngModel name=\"ValueRaw\" required>\n                </fieldset>\n\n\n\n\n                <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!s.valid\">Add Attribute</button>\n            </form>\n        </div>\n    </div>\n    <img [src]=\"loadingImagePath\" *ngIf=\"loadingSpecAttributes\" alt=\"\">\n    <div class=\"row\">\n\n        <div class=\"card-block\" style=\"width:100%;\">\n            <table class=\"table table-bordered table-hover\" style=\"table-layout:fixed\">\n                <thead class=\"thead-inverse\">\n                    <tr>\n                        <th>Attribute Type</th>\n                        <th>Attribute</th>\n                        <th>Value</th>\n                        <th>Action</th>\n\n\n                    </tr>\n                </thead>\n                <tbody>\n\n                    <tr *ngFor=\"let i = index;let specAttribute of currentSpecs\">\n                        <td>{{specAttribute.AttributeTypeName}} </td>\n                        <td>{{specAttribute.AttributeName}}</td>\n                        <td>{{specAttribute.ValueRaw}}</td>\n                        <td>\n\n                            <button type=\"button\" name=\"{{specAttribute.Id}}\" class=\"btn btn-danger\" (click)=\"deleteCurrentSpecAttribute(d)\" #d><i class=\"fa fa-times\"></i></button>\n                        </td>\n                    </tr>\n                </tbody>\n            </table>\n\n        </div>\n    </div>\n\n</div>\n"

/***/ }),

/***/ "../../../../../src/app/layout/products/link-product-spec-attributes/link-product-spec-attributes.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/layout/products/link-product-spec-attributes/link-product-spec-attributes.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__link_product_spec_attributes_service__ = __webpack_require__("../../../../../src/app/layout/products/link-product-spec-attributes/link-product-spec-attributes.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LinkProductSpecAttributesComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var LinkProductSpecAttributesComponent = (function () {
    function LinkProductSpecAttributesComponent(http, specAttributeService) {
        this.http = http;
        this.specAttributeService = specAttributeService;
        this.specAttributeList = [];
        this.currentSpecs = [];
        this.attributeList = [];
        this.loadingSpecAttributes = false;
        this.specAttributeId = 0;
        this.ValueRaw = '';
        this.specAttributeName = '';
        this.loadingImagePath = '../../../assets/images/ajax-loader.gif';
        this.AttributeId = 0;
    }
    LinkProductSpecAttributesComponent.prototype.ngOnInit = function () {
        this.getAttributes();
        this.getCurrentSpecAttributes();
        this.getSpecAttributes();
    };
    LinkProductSpecAttributesComponent.prototype.getCurrentSpecAttributes = function () {
        var _this = this;
        this.loadingSpecAttributes = true;
        this.specAttributeService.getCurrentSpecAttributes(this.Id)
            .subscribe(function (response) {
            _this.currentSpecs = (response.json().Data);
            console.log(_this.currentSpecs);
            _this.loadingSpecAttributes = false;
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    LinkProductSpecAttributesComponent.prototype.getSpecAttributes = function () {
        var _this = this;
        this.loadingSpecAttributes = true;
        this.specAttributeService.getSpecAttributes()
            .subscribe(function (response) {
            _this.specAttributeList = (response.json().Data);
            console.log(response.json());
            _this.loadingSpecAttributes = false;
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    LinkProductSpecAttributesComponent.prototype.addSpecAttribute = function () {
        var _this = this;
        this.loadingSpecAttributes = true;
        this.AttributeId = this.specAttributeForm.value.current_attribute_id;
        this.specAttributeId = this.specAttributeForm.value.current_spec_attribute_id;
        this.specAttributeName = this.getCurrentSpecAttributeName(this.specAttributeId)[0].Name;
        this.ValueRaw = this.specAttributeForm.value.ValueRaw;
        this.specAttributeService.addSpecAttribute(this.Id, this.AttributeId, this.specAttributeId, this.specAttributeName, this.ValueRaw)
            .subscribe(function (data) {
            alert('Added !');
            _this.specAttributeForm.reset();
            _this.loadingSpecAttributes = false;
            _this.getCurrentSpecAttributes();
        }, function (error) {
            console.log(error);
            alert('Can\'t fetch data ! Please refresh or check your connnection !');
        });
    };
    LinkProductSpecAttributesComponent.prototype.getCurrentSpecAttributeName = function (id) {
        return this.specAttributeList.filter(function (attribute) { return attribute.Id == id; });
    };
    LinkProductSpecAttributesComponent.prototype.deleteCurrentSpecAttribute = function (id) {
        var _this = this;
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.loadingSpecAttributes = true;
            this.specAttributeService.deleteSpecAttribute(+id.name)
                .subscribe(function (data) {
                alert('Deleted !');
                _this.specAttributeForm.reset();
                _this.loadingSpecAttributes = false;
                _this.getCurrentSpecAttributes();
            }, function (error) {
                console.log(error);
                alert('Can\'t fetch data ! Please refresh or check your connnection !');
            });
        }
    };
    LinkProductSpecAttributesComponent.prototype.getAttributes = function () {
        var _this = this;
        console.log("Product Id:" + this.Id);
        this.specAttributeService.getProductAttributes(this.Id)
            .subscribe(function (response) {
            _this.attributeList = (response.json().Data);
            console.log(_this.attributeList);
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    return LinkProductSpecAttributesComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])('ProductId'),
    __metadata("design:type", Object)
], LinkProductSpecAttributesComponent.prototype, "Id", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])('Attributes'),
    __metadata("design:type", Object)
], LinkProductSpecAttributesComponent.prototype, "productAttributeFields", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('s'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _a || Object)
], LinkProductSpecAttributesComponent.prototype, "specAttributeForm", void 0);
LinkProductSpecAttributesComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app-link-product-spec-attributes',
        template: __webpack_require__("../../../../../src/app/layout/products/link-product-spec-attributes/link-product-spec-attributes.component.html"),
        styles: [__webpack_require__("../../../../../src/app/layout/products/link-product-spec-attributes/link-product-spec-attributes.component.scss")]
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_http__["b" /* Http */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__link_product_spec_attributes_service__["a" /* LinkProductSpecAttributesService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__link_product_spec_attributes_service__["a" /* LinkProductSpecAttributesService */]) === "function" && _c || Object])
], LinkProductSpecAttributesComponent);

var _a, _b, _c;
//# sourceMappingURL=link-product-spec-attributes.component.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/link-product-spec-attributes/link-product-spec-attributes.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LinkProductSpecAttributesService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
/*Services for all product-related pictures .Each API call is applicable for it's associated product */


var LinkProductSpecAttributesService = (function () {
    function LinkProductSpecAttributesService(http) {
        this.http = http;
    }
    LinkProductSpecAttributesService.prototype.addSpecAttribute = function (prodId, attributeId, specId, specName, value) {
        console.log(prodId);
        console.log(attributeId);
        console.log(specId);
        console.log(specName);
        console.log(value);
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Products/' + prodId + '/ProductSpecificationAttributeAdd/' + attributeId + '/' + specId + '/' + value + '/sampleString/true/true/1');
    };
    LinkProductSpecAttributesService.prototype.getSpecAttributes = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/0/2147483647');
    };
    LinkProductSpecAttributesService.prototype.getCurrentSpecAttributes = function (id) {
        console.log("Get Current Spec Attributes called !");
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/' + id + '/ProductSpecAttrList', {
            "Page": 0,
            "PageSize": 20
        }, { headers: headers });
    };
    LinkProductSpecAttributesService.prototype.getProductAttributes = function (id) {
        console.log("Get Attributes called !");
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log(id);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/' + id + '/ProductAttributeMappingList', {
            "Page": 0,
            "PageSize": 200
        }, { headers: headers });
    };
    LinkProductSpecAttributesService.prototype.deleteSpecAttribute = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/ProductSpecAttr/Delete?id=' + id, null, { headers: headers });
    };
    return LinkProductSpecAttributesService;
}());
LinkProductSpecAttributesService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], LinkProductSpecAttributesService);

var _a;
//# sourceMappingURL=link-product-spec-attributes.service.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/product-attributes/product-attributes.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProductAttributesService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var ProductAttributesService = (function () {
    function ProductAttributesService(http) {
        this.http = http;
    }
    ProductAttributesService.prototype.storeAttributes = function (attributes) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        this.temp = attributes[attributes.length - 1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Add?continueEditing=true', this.temp, { headers: headers });
    };
    ProductAttributesService.prototype.getAttributes = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/0/2147483647');
    };
    ProductAttributesService.prototype.updateAttributes = function (attributes, id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log(attributes);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Update?continueEditing=true', attributes[id], { headers: headers });
    };
    ProductAttributesService.prototype.deleteAttributes = function (attributes, id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(attributes);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Delete/' + id, null, { headers: headers });
    };
    return ProductAttributesService;
}());
ProductAttributesService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], ProductAttributesService);

var _a;
//# sourceMappingURL=product-attributes.service.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/product-pictures/product-pictures.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"container-fluid\">\n    <h2>Product Pictures</h2>\n    <hr>\n    <div class=\"row\" >\n        <div class=\"col-lg-6 center-block\" style=\"float: none; margin: 0 auto;\">\n            <form role=\"form\" (ngSubmit)=\"addPicture(p)\" #p=\"ngForm\">\n                <fieldset class=\"form-group card mb-3\">\n                    <label class=\"card-header\">Picture</label>\n                    <image-upload [max]=\"1\" [url]=\"'http://piyushdaftary-001-site1.ctempurl.com/api/Pictures/upload'\" [buttonCaption]=\"'Select Images!'\" [extensions]=\"['jpg','png','gif']\" (onFileUploadFinish)=\"getPictureDetails($event)\"></image-upload>\n                </fieldset>\n                <fieldset class=\"form-group card mb-3\">\n                    <label class=\"card-header\">Display Order</label>\n                    <input type=\"number\" class=\"form-control\" ngModel name=\"DisplayOrder\" placeholder=\"Enter Display Order\" required>\n                </fieldset>\n\n\n\n\n                <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!p.valid\">Add Picture</button>\n            </form>\n        </div>\n    </div>\n    <img [src]=\"loadingImagePath\" *ngIf=\"loadingPicture\" alt=\"\">\n    <div class=\"row\">\n        <div class=\"card-block\" style=\"width:100%;\">\n                <table class=\"table table-bordered table-hover\" style=\"table-layout:fixed\">\n                    <thead class=\"thead-inverse\">\n                        <tr>\n                            <th>Picture</th>\n                            <th>Display Order</th>\n                            <th>Action</th>\n\n\n                        </tr>\n                    </thead>\n                    <tbody>\n\n                        <tr *ngFor=\"let i = index;let picture of pictureList\">\n                    <td><img [src]=\"picture.PictureUrl\" alt=\"\" height=\"70\" width=\"70\"></td>\n                        <td>{{picture.DisplayOrder}}</td>\n\n                        <td>\n\n                                <button type=\"button\" name=\"{{picture.Id}}\" class=\"btn btn-danger\" (click)=\"deletePicture(d)\" #d><i class=\"fa fa-times\"></i></button>\n                        </td>\n                        </tr>\n\n                    </tbody>\n                </table>\n\n        </div>\n    </div>\n</div>\n"

/***/ }),

/***/ "../../../../../src/app/layout/products/product-pictures/product-pictures.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/layout/products/product-pictures/product-pictures.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__product_pictures_service__ = __webpack_require__("../../../../../src/app/layout/products/product-pictures/product-pictures.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProductPicturesComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var ProductPicturesComponent = (function () {
    function ProductPicturesComponent(http, productPicturesService) {
        this.http = http;
        this.productPicturesService = productPicturesService;
        this.currentPicture = [];
        this.currentPageNumber = 1;
        this.loadingPicture = false;
        this.loadingImagePath = '../../../assets/images/ajax-loader.gif';
        this.pictureDisplayOrder = 0;
        this.pictureId = 1;
        this.imageUrl = '';
    }
    ProductPicturesComponent.prototype.ngOnInit = function () {
        console.log(this.Id);
        this.getPicture();
    };
    ProductPicturesComponent.prototype.addPicture = function () {
        var _this = this;
        this.loadingPicture = true;
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
            "DisplayOrder": this.pictureDisplayOrder,
            "OverrideAltAttribute": "sample string 6",
            "OverrideTitleAttribute": "sample string 7"
        });
        this.productPicturesService.addPicture(this.currentPicture)
            .subscribe(function (response) {
            _this.loadingPicture = false;
            alert("Added !");
            _this.getPicture();
            //this.getPicture();
            _this.pictureForm.reset();
            _this.currentPicture = [];
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductPicturesComponent.prototype.getPicture = function () {
        var _this = this;
        console.log("New get picture function called !");
        this.loadingPicture = true;
        this.productPicturesService.getPicture(this.Id)
            .subscribe(function (response) {
            _this.pictureList = (response.json().Data);
            _this.loadingPicture = false;
            console.log(_this.pictureList);
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductPicturesComponent.prototype.deletePicture = function (id) {
        var _this = this;
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.loadingPicture = true;
            this.productPicturesService.deletePicture(+id.name)
                .subscribe(function (data) {
                _this.loadingPicture = false;
                alert('Deleted !');
                _this.pictureForm.reset();
                _this.getPicture();
            }, function (error) {
                console.log(error);
                alert('Can\'t fetch data ! Please refresh or check your connnection !');
            });
        }
    };
    ProductPicturesComponent.prototype.getPictureDetails = function (file) {
        this.pictureId = file.serverResponse.json().pictureId;
        this.imageUrl = file.serverResponse.json().imageUrl;
    };
    return ProductPicturesComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])('ProductId'),
    __metadata("design:type", Object)
], ProductPicturesComponent.prototype, "Id", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('p'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _a || Object)
], ProductPicturesComponent.prototype, "pictureForm", void 0);
ProductPicturesComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app-product-pictures',
        template: __webpack_require__("../../../../../src/app/layout/products/product-pictures/product-pictures.component.html"),
        styles: [__webpack_require__("../../../../../src/app/layout/products/product-pictures/product-pictures.component.scss")]
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_http__["b" /* Http */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__product_pictures_service__["a" /* ProductPicturesService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__product_pictures_service__["a" /* ProductPicturesService */]) === "function" && _c || Object])
], ProductPicturesComponent);

var _a, _b, _c;
//# sourceMappingURL=product-pictures.component.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/product-pictures/product-pictures.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProductPicturesService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
/*Services for all product-related pictures .Each API call is applicable for it's associated product */


var ProductPicturesService = (function () {
    function ProductPicturesService(http) {
        this.http = http;
    }
    ProductPicturesService.prototype.getPicture = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/' + id + '/ProductPictureList', {
            "Page": 0,
            "PageSize": 200
        }, { headers: headers });
    };
    ProductPicturesService.prototype.addPicture = function (picture) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        this.temp = picture[0];
        console.log(picture[0]);
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Products/' + this.temp["ProductId"] + '/ProductPictureAdd/' + this.temp["PictureId"] + '/' + this.temp["DisplayOrder"] + '/sampleString/sampleString');
    };
    ProductPicturesService.prototype.deletePicture = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/ProductPicture/Delete?id=' + id, null, { headers: headers });
    };
    return ProductPicturesService;
}());
ProductPicturesService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], ProductPicturesService);

var _a;
//# sourceMappingURL=product-pictures.service.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/product.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProductService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var ProductService = (function () {
    function ProductService(http) {
        this.http = http;
    }
    ProductService.prototype.storeProduct = function (product) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        this.temp = product[0];
        console.log(this.temp);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/Add?continueEditing=true', this.temp, { headers: headers });
    };
    ProductService.prototype.getAllProducts = function (page) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products?pageIndex=' + page + '&pageSize=10', {}, { headers: headers });
        //  return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Products/DefaultPageLoad');
    };
    ProductService.prototype.searchProduct = function (searchProductParameters) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log(searchProductParameters[0]);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products?pageIndex=0&pageSize=25878', searchProductParameters[0], { headers: headers });
        //  return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Products/DefaultPageLoad');
    };
    // getAttributes() {
    //     return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/0/2147483647');
    // }
    ProductService.prototype.getCurrentAttributes = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/' + id + '/ProductAttributeMappingList', {
            "Page": 0,
            "PageSize": 200
        }, { headers: headers });
    };
    ProductService.prototype.addAttribute = function (attribute) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        this.temp = attribute[0];
        console.log(attribute[0]);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/ProductAttributeMapping/Add', this.temp, { headers: headers });
    };
    ProductService.prototype.deleteAttribute = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/ProductAttributeMapping/Delete?id=' + id, null, { headers: headers });
    };
    ProductService.prototype.addSpecAttribute = function (prodId, attributeId, specId, specName, value) {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Products/' + prodId + '/ProductSpecificationAttributeAdd/' + attributeId + '/' + specId + '/' + value + '/sampleString/true/true/1');
    };
    ProductService.prototype.getSpecAttributes = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/0/2147483647');
    };
    ProductService.prototype.getCurrentSpecAttributes = function (id) {
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/' + id + '/ProductSpecAttrList', {
            "Page": 0,
            "PageSize": 20
        });
    };
    ProductService.prototype.deleteSpecAttribute = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/ProductSpecAttr/Delete?id=' + id, null, { headers: headers });
    };
    ProductService.prototype.getCategory = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Categories');
    };
    ProductService.prototype.getManufacturers = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers');
    };
    ProductService.prototype.getStores = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Stores');
    };
    ProductService.prototype.getVendors = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors');
    };
    ProductService.prototype.updateProduct = function (product) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        console.log(product);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/Edit?continueEditing=true', product, { headers: headers });
    };
    ProductService.prototype.deleteProduct = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/Delete?id=' + id, null, { headers: headers });
    };
    return ProductService;
}());
ProductService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], ProductService);

var _a;
//# sourceMappingURL=product.service.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/products-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__products_component__ = __webpack_require__("../../../../../src/app/layout/products/products.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProductsRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



// import { ProductTagsComponent } from './product-tags/product-tags.component';
var routes = [
    { path: '', component: __WEBPACK_IMPORTED_MODULE_2__products_component__["a" /* ProductsComponent */] },
    { path: 'showList', component: __WEBPACK_IMPORTED_MODULE_2__products_component__["a" /* ProductsComponent */] }
];
var ProductsRoutingModule = (function () {
    function ProductsRoutingModule() {
    }
    return ProductsRoutingModule;
}());
ProductsRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */].forChild(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */]]
    })
], ProductsRoutingModule);

//# sourceMappingURL=products-routing.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/products.component.html":
/***/ (function(module, exports) {

module.exports = "<div align=\"center\">\n    <h1>Product Management</h1>\n\n    <div class=\"row\">\n        <!--Search Product Form -->\n\n        <div *ngIf=\"searchProductMode\" class=\"col-lg-6\" style=\"float: none; margin: 0 auto;\">\n            <form role=\"form\" (ngSubmit)=\"searchProduct(s)\" #s=\"ngForm\" style=\"width:100%\">\n                <div>\n                       <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Product Name</label>\n                        <input class=\"form-control\" ngModel name=\"Name\" placeholder=\"Enter Product Name\">\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Category</label>\n                        <select class=\"form-control\" ngModel name=\"CategoryId\" size=\"4\">\n                                  <option *ngFor=\"let category of categories\" value=\"{{category.Id}}\">\n                                    {{category.Name}}\n                                  <hr></option>\n                    </select>\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Manufacturers</label>\n                        <select class=\"form-control\" ngModel name=\"ManufacturerId\" size=\"4\">\n                                  <option *ngFor=\"let manufacturer of manufacturers\" value=\"{{manufacturer.Id}}\">\n                                    {{manufacturer.Name}}\n                                  <hr></option>\n                    </select>\n                    </fieldset></div>\n                <div >\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Vendors</label>\n                        <select class=\"form-control\" ngModel name=\"VendorId\" size=\"4\">\n                                  <option *ngFor=\"let vendor of vendors\" value=\"{{vendor.Id}}\">\n                                    {{vendor.Name}}\n                                  <hr></option>\n                    </select>\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Stores</label>\n                        <select class=\"form-control\" ngModel name=\"StoreId\" size=\"4\">\n                                  <option *ngFor=\"let store of stores\" value=\"{{store.Id}}\">\n                                    {{store.Name}}\n                                  <hr></option>\n                    </select>\n                    </fieldset>\n\n                    <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!s.valid\">Search Product</button>\n\n\n                </div>\n\n\n\n            </form>\n        <!--End of Search Product Form -->\n        <!-- ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// -->\n        </div>\n\n\n    <!--Add New Product -->\n        <div *ngIf=\"addNewProduct\">\n            <div align=\"center\">\n                <!-- <span><button class=\"btn btn-primary\" (click)=\"showToggle(1)\">Product Info</button>\n                        <button class=\"btn btn-success\" (click)=\"showToggle(2)\">Pictures</button>\n                        <button class=\"btn btn-danger\" (click)=\"showToggle(3)\">Product Attributes</button>\n                        <button class=\"btn btn-warning\" (click)=\"showToggle(4)\">Specification Attributes</button>\n                   </span> -->\n            </div>\n            <br>\n            <div>\n                <form role=\"form\" (ngSubmit)=\"addProduct(f)\" #f=\"ngForm\">\n\n\n\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Product Name (required)</label>\n                        <input class=\"form-control\" ngModel name=\"Name\" required placeholder=\"Enter Product Name\" *ngIf=\"!editMode\">\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Product FullDescription (required)</label>\n                        <textarea class=\"form-control\" rows=\"3\" ngModel name=\"FullDescription\" required placeholder=\"Enter Product FullDescription\" *ngIf=\"!editMode\"></textarea>\n\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Price (required)</label>\n                        <input type=\"number\" class=\"form-control\" ngModel name=\"Price\" required placeholder=\"Enter Price\" *ngIf=\"!editMode\">\n\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Stock Quantity (required)</label>\n                        <input type=\"number\" class=\"form-control\" ngModel name=\"StockQuantity\" required>\n                    </fieldset>\n\n\n\n\n\n\n\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Category</label>\n                        <select class=\"form-control\" ngModel name=\"CategoryId\" required multiple size=\"4\">\n                              <option *ngFor=\"let category of categories\" value=\"{{category.Id}}\">\n                                {{category.Name}}\n                              <hr></option>\n                </select>\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Manufacturers</label>\n                        <select class=\"form-control\" ngModel name=\"ManufacturerId\" required multiple size=\"4\">\n                              <option *ngFor=\"let manufacturer of manufacturers\" value=\"{{manufacturer.Id}}\">\n                                {{manufacturer.Name}}\n                              <hr></option>\n                </select>\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Vendors</label>\n                        <select class=\"form-control\" ngModel name=\"VendorId\" required size=\"4\">\n                              <option *ngFor=\"let vendor of vendors\" value=\"{{vendor.Id}}\">\n                                {{vendor.Name}}\n                              <hr></option>\n                </select>\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Stores</label>\n                        <select class=\"form-control\" ngModel name=\"StoreId\" required multiple size=\"4\">\n                              <option *ngFor=\"let store of stores\" value=\"{{store.Id}}\">\n                                {{store.Name}}\n                              <hr></option>\n                </select>\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">SKU</label>\n                        <input type=\"text\" class=\"form-control\" ngModel name=\"Sku\">\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Published <span>(required)</span></label>\n                        <div class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                            <input type=\"checkbox\" class=\"form-control\" ngModel required name=\"Published\">\n                        </div>\n\n\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">GTIN(Global Trade Item Number)</label>\n                        <input type=\"text\" class=\"form-control\" ngModel name=\"Gtin\">\n                    </fieldset>\n                    <fieldset class=\"form-group car mb-3\">\n                        <label class=\"card-header\">Manufacturer Part Number</label>\n                        <input type=\"text\" class=\"form-control\" ngModel name=\"ManufacturerPartNumber\">\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Show on Home Page <span>(required)</span></label>\n                        <div class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                            <input type=\"checkbox\" class=\"form-control\" ngModel required name=\"ShowOnHomePage\">\n                        </div>\n\n\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Mark as New <span>(required)</span></label>\n                        <div class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                            <input type=\"checkbox\" class=\"form-control\" ngModel required name=\"MarkAsNew\">\n                        </div>\n\n\n                    </fieldset>\n                    <fieldset class=\"form-group card mb-3\">\n                        <label class=\"card-header\">Display Order</label>\n                        <input type=\"Number\" class=\"form-control\" ngModel name=\"DisplayOrder\" required placeholder=\"Enter Display Order\">\n                    </fieldset>\n\n\n\n\n                    <img [src]=\"loadingImagePath\" *ngIf=\"loading\" alt=\"\">\n                    <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"!editMode\">Add Product</button>\n                    <button (click)=\"showList()\" class=\"btn btn-info\">Switch</button>\n\n\n\n                </form>\n            </div>\n            <!--End of Add New Product -->\n\n<!-- ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// -->\n        </div>\n    </div>\n\n<hr>\n<div class=\"row\" *ngIf=\"showSearchedProductList\">\n\n\n    <div class=\"card mb-3\">\n        <div class=\"card-header\">\n           <h2>Product List </h2>\n        </div>\n        <div class=\"card-block\" style=\"width:100%;\">\n            <table class=\"table table-bordered table-hover\" style=\"table-layout:fixed\">\n                <thead class=\"thead-inverse\">\n                    <tr>\n                        <th>Product Picture</th>\n                        <th>Product Name</th>\n                        <th>Price</th>\n                        <th>Stock Quantity</th>\n\n                        <th>Action</th>\n\n\n                    </tr>\n                </thead>\n                <tbody>\n                    <tr *ngFor=\"let product of searchedProducts\">\n                        <td><img [src]=\"product.PictureThumbnailUrl\" alt=\"\"></td>\n                        <td>{{product.Name}}</td>\n\n                        <td>{{product.Price}}</td>\n                        <td>{{product.StockQuantity}}</td>\n\n\n                        <td><button type=\"button\" name=\"{{product.Id}}\" class=\"btn btn-primary\" (click)=\"editProductMode(x)\" #x><i class=\"fa fa-edit\"></i></button>\n                            <button type=\"button\" name=\"{{product.Id}}\" class=\"btn btn-danger\" (click)=\"deleteProduct(d)\" #d><i class=\"fa fa-times\"></i></button>\n                        </td>\n                    </tr>\n\n                </tbody>\n\n            </table>\n\n            <button (click)=\"showList()\" class=\"btn btn-info\">Switch</button>\n\n        </div>\n    </div>\n\n\n\n</div>\n<!--Show Products -->\n<div class=\"row\" *ngIf=\"showProductList\">\n\n\n    <div class=\"card mb-3\">\n        <div class=\"card-header\">\n            <span>    <h2>Product List </h2> <button class=\"btn btn-primary\" (click)=\"addProductMode()\">Add Product</button></span>\n\n            <!-- <input type=\"text\" placeholder=\"Search by Product Name or Category\" [(ngModel)]=\"filteredProduct\" style=\"width:100%;\"> -->\n        </div>\n        <div class=\"card-block\" style=\"width:100%;\">\n            <table class=\"table table-bordered table-hover\" style=\"table-layout:fixed\">\n                <thead class=\"thead-inverse\">\n                    <tr>\n                        <th>Product Picture</th>\n                        <th>Product Name</th>\n                        <th>Price</th>\n                        <th>Stock Quantity</th>\n\n                        <th>Action</th>\n\n\n                    </tr>\n                </thead>\n                <tbody>\n                    <tr *ngFor=\"let product of products | paginate: { id:'Products', itemsPerPage: 5, currentPage: currentPageNumber , totalItems: totalProducts}\">\n                        <td><img [src]=\"product.PictureThumbnailUrl\" alt=\"\"></td>\n                        <td>{{product.Name}}</td>\n\n                        <td>{{product.Price}}</td>\n                        <td>{{product.StockQuantity}}</td>\n\n\n                        <td><button type=\"button\" name=\"{{product.Id}}\" class=\"btn btn-primary\" (click)=\"editProductMode(x)\" #x><i class=\"fa fa-edit\"></i></button>\n                            <button type=\"button\" name=\"{{product.Id}}\" class=\"btn btn-danger\" (click)=\"deleteProduct(d)\" #d><i class=\"fa fa-times\"></i></button>\n                        </td>\n                    </tr>\n\n                </tbody>\n            </table>\n            <img [src]=\"loadingImagePath\" *ngIf=\"loading\" alt=\"\">\n            <pagination-controls (pageChange)=\"getProducts($event)\" id=\"Products\"></pagination-controls>\n        </div>\n    </div>\n\n\n\n</div>\n<!--End of Product List-->\n<!-- ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// -->\n<!--Product Edit Form -->\n<div class=\"row\" *ngIf=\"editMode\">\n<div class=\"col-lg-6\" style=\"float: none; margin: 0 auto;\">\n  <span><button class=\"btn btn-primary\" (click)=\"showToggle(1)\">Product Info</button>\n    <button class=\"btn btn-success\" (click)=\"showToggle(2)\">Pictures</button>\n    <button class=\"btn btn-danger\" (click)=\"showToggle(3)\">Product Attributes</button>\n    <button class=\"btn btn-warning\" (click)=\"showToggle(4)\">Specification Attributes</button>\n    <button (click)=\"showList()\" class=\"btn btn-success\">Go Back</button>\n   </span>\n   </div>\n   <div style=\"height:50px; width:100%\"></div>\n\n\n\n    <div class=\"col-lg-6\" style=\"float: none; margin: 0 auto;\" *ngIf=\"showProductInfo\" align=\"center\">\n\n\n        <form role=\"form\" (ngSubmit)=\"editProduct(t)\" #t=\"ngForm\">\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Product Name</label>\n                <input class=\"form-control\" [(ngModel)]=\"Name\" name=\"Name\" required value=\"{{Name}}\">\n              </fieldset>\n              <fieldset class=\"form-group card mb-3\">\n                  <label class=\"card-header\">FullDescription</label>\n                  <textarea class=\"form-control\" rows=\"3\" [(ngModel)]=\"FullDescription\" name=\"FullDescription\" required value=\"{{FullDescription}}\"></textarea>\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Price</label>\n                <input type=\"number\" class=\"form-control\" [(ngModel)]=\"Price\" name=\"Price\" required value=\"{{Price}}\">\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Stock Quantity</label>\n                <input type=\"number\" class=\"form-control\" [(ngModel)]=\"StockQuantity\" name=\"StockQuantity\" value=\"{{StockQuantity}}\">\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Category</label>\n                <select class=\"form-control\" [(ngModel)]=\"CategoryId\" name=\"CategoryId\" multiple size=\"4\">\n              <option *ngFor=\"let category of categories\" value=\"{{category.Id}}\">\n                {{category.Name}}\n              <hr></option>\n</select>\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Manufacturers</label>\n                <select class=\"form-control\" [(ngModel)]=\"ManufacturerId\" name=\"ManufacturerId\" multiple size=\"4\">\n              <option *ngFor=\"let manufacturer of manufacturers\" value=\"manufacturer.Id\">\n                {{manufacturer.Name}}\n              <hr></option>\n</select>\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Vendors</label>\n                <select class=\"form-control\" [(ngModel)]=\"VendorId\" name=\"VendorId\" size=\"4\">\n              <option *ngFor=\"let vendor of vendors\" value=\"vendor.Id\">\n                {{vendor.Name}}\n              <hr></option>\n</select>\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Stores</label>\n                <select class=\"form-control\" [(ngModel)]=\"StoreId\" name=\"StoreId\" multiple size=\"4\">\n              <option *ngFor=\"let store of stores\" value=\"store.Id\">\n                {{store.Name}}\n              <hr></option>\n</select>\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">SKU</label>\n                <input type=\"text\" class=\"form-control\" [(ngModel)]=\"Sku\" name=\"Sku\" value=\"{{Sku}}\">\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Published <span>(required)</span></label>\n                <div class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                    <input type=\"checkbox\" class=\"form-control\" [(ngModel)]=\"Published\" name=\"Published\">\n                </div>\n\n\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">GTIN(Global Trade Item Number)</label>\n                <input type=\"text\" class=\"form-control\" [(ngModel)]=\"Gtin\" name=\"Gtin\" value=\"{{Gtin}}\">\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Manufacturer Part Number</label>\n                <input type=\"text\" class=\"form-control\" [(ngModel)]=\"ManufacturerPartNumber\" name=\"ManufacturerPartNumber\" value=\"{{ManufacturerPartNumber}}\">\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Show on Home Page <span>(required)</span></label>\n                <div class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                    <input type=\"checkbox\" class=\"form-control\" [(ngModel)]=\"ShowOnHomePage\" name=\"ShowOnHomePage\">\n                </div>\n\n\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Mark as New <span>(required)</span></label>\n                <div class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                    <input type=\"checkbox\" class=\"form-control\" [(ngModel)]=\"MarkAsNew\" name=\"MarkAsNew\">\n                </div>\n\n\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Display Order</label>\n                <input type=\"number\" class=\"form-control\" [(ngModel)]=\"DisplayOrder\" name=\"DisplayOrder\" placeholder=\"Enter Display Order\">\n            </fieldset>\n\n            <button (click)=\"showList()\" class=\"btn btn-success\">Go Back</button>\n\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!t.valid\">Edit Product</button>\n        </form>\n    </div>\n\n\n<!-- Attribute Form  -->\n<!-- //////////////////////////////////////////////////////////////////////////////// -->\n    <div class=\"col-lg-6\" style=\"float: none; margin: 0 auto;\" *ngIf=\"showProductAttributes\">\n        <h2>Attributes</h2>\n        <!-- <button class=\"btn btn-primary\" (click)=\"addCurrentAttributeMode()\" [disabled]=\"!addAttributeMode\">Add Current Attribute</button> -->\n    <hr>\n    <div *ngIf=\"showCurrentAttributeForm\">\n    <form role=\"form\" (ngSubmit)=\"addAttribute(q)\" #q=\"ngForm\">\n        <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Attribute</label>\n            <select class=\"form-control\" ngModel name=\"current_attribute_id\" >\n          <option *ngFor=\"let attribute of product_attributes\" value=\"{{attribute.Id}}\">\n            {{attribute.Name}}\n          <hr></option>\n      </select>\n        </fieldset>\n\n        <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Text Prompt</label>\n            <input type=\"text\" class=\"form-control\" rows=\"3\" ngModel name=\"TextPrompt\" required >\n        </fieldset>\n\n        <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Is Required </label>\n            <div class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                <input type=\"checkbox\" class=\"form-control\" ngModel name=\"IsRequired\">\n            </div>\n        </fieldset>\n\n        <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Display Order</label>\n            <input type=\"number\" class=\"form-control\" ngModel name=\"DisplayOrder\" placeholder=\"Enter Display Order\">\n        </fieldset>\n\n        <button (click)=\"showList()\" class=\"btn btn-success\">Go Back</button>\n\n        <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!q.valid\" *ngIf=\"addAttributeMode\">Add Attribute</button>\n    </form>\n</div>\n\n\n\n        <div class=\"card-block\" style=\"width:100%;\" *ngIf=\"showCurrentAttributeList\">\n            <table class=\"table table-bordered table-hover\" style=\"table-layout:fixed\">\n                <thead class=\"thead-inverse\">\n                    <tr>\n                        <th>Attribute</th>\n                        <th>Text Prompt</th>\n                        <th>Is Required</th>\n                        <th>Display Order</th>\n\n                        <th>Action</th>\n\n\n                    </tr>\n                </thead>\n                <tbody>\n                    <tr><img [src]=\"loadingImagePath\" *ngIf=\"loading\" alt=\"\"></tr>\n                    <tr *ngFor=\"let i = index;let attributeField of productAttributeFields\">\n                    <td>{{attributeField.ProductAttribute}} </td>\n                    <td>{{attributeField.TextPrompt}}</td>\n                    <td>{{attributeField.IsRequired}}</td>\n                    <td>{{attributeField.DisplayOrder}}</td>\n                    <td>\n                        <button type=\"button\" name=\"{{attributeField.Id}}\" class=\"btn btn-primary\" (click)=\"editCurrentAttributeMode(x)\" [disabled]=\"true\" #x><i class=\"fa fa-edit\"></i></button>\n                            <button type=\"button\" name=\"{{attributeField.Id}}\" class=\"btn btn-danger\" (click)=\"deleteCurrentAttribute(d)\" #d><i class=\"fa fa-times\"></i></button>\n                    </td>\n                    </tr>\n                </tbody>\n            </table>\n\n    </div>\n\n\n</div>\n<!-- End of Attribute Form -->\n<!-- /////////////////////////////////////////////////////////////////////////////////////////////// -->\n<app-link-product-spec-attributes [ProductId]=\"Id\"  [Attributes] =\"productAttributeFields\" *ngIf=\"showSpecificationAttributes\"></app-link-product-spec-attributes>\n<app-product-pictures [ProductId]=\"Id\" *ngIf=\"showPictures\"></app-product-pictures>\n\n\n</div>\n"

/***/ }),

/***/ "../../../../../src/app/layout/products/products.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "input.ng-invalid.ng-touched {\n  border: 1px solid red; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/layout/products/products.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__product_service__ = __webpack_require__("../../../../../src/app/layout/products/product.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__product_attributes_product_attributes_service__ = __webpack_require__("../../../../../src/app/layout/products/product-attributes/product-attributes.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProductsComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var ProductsComponent = (function () {
    function ProductsComponent(http, productService, productAttributeService) {
        this.http = http;
        this.productService = productService;
        this.productAttributeService = productAttributeService;
        this.multiple = false;
        this.product = [];
        this.searchProductParameters = [];
        this.current_attribute = [];
        this.CategoryId = [];
        this.StoreId = [];
        this.ManufacturerId = [];
        this.currentProduct = [];
        this.loadingImagePath = '';
        this.productAttributeFields = [];
        this.specAttributeFields = [];
        this.Spec_Attribute_Id = '';
        this.ValueRaw = '';
    }
    ProductsComponent.prototype.ngOnInit = function () {
        this.Name = '';
        this.FullDescription = '';
        this.filteredProduct = '';
        this.totalProducts = 25878;
        this.loadingImagePath = '../../assets/images/ajax-loader.gif';
        this.tags = JSON.parse(localStorage.getItem("tags"));
        this.addNewProduct = false;
        this.submitted = false;
        this.editMode = false;
        this.currentPageNumber = 1;
        this.editAttributeMode = false;
        this.editSpecAttributeMode = false;
        this.showProductList = true;
        this.Sku = '';
        this.Gtin = '';
        this.Published = false;
        this.ManufacturerPartNumber = '';
        this.ShowOnHomePage = false;
        this.MarkAsNew = false;
        this.showProductInfo = false;
        this.showPictures = false;
        this.showProductAttributes = false;
        this.showSpecificationAttributes = false;
        this.DisplayOrder = 0;
        this.VendorId = 0;
        this.StockQuantity = 0;
        this.CreatedOn = '';
        this.loadingProduct = false;
        this.searchProductMode = true;
        this.showSearchedProductList = false;
        this.addAttributeMode = false;
        this.showCurrentAttributeList = true;
        this.showCurrentSpecAttributeList = true;
        this.showCurrentAttributeForm = false;
        this.showCurrentSpecAttributeForm = false;
        this.addSpecAttributeMode = false;
        this.getProducts(0);
        this.getAllData();
    };
    ProductsComponent.prototype.addProduct = function () {
        var _this = this;
        this.submitted = true;
        this.loadingProduct = true;
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
            this.CreatedOn = new Date(new Date().getTime()).toLocaleString();
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
                "FullDescription": this.FullDescription,
                "AdminComment": null,
                "ShowOnHomePage": this.ShowOnHomePage,
                "MetaKeywords": null,
                "MetaDescription": null,
                "MetaTitle": null,
                "SeName": null,
                "AllowCustomerReviews": true,
                "ProductTags": null,
                "Sku": this.Sku,
                "ManufacturerPartNumber": this.ManufacturerPartNumber,
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
                .subscribe(function (data) {
                _this.loadingProduct = false;
                alert("Product Added");
                _this.productForm.reset();
            }, function (error) {
                console.log(error);
                alert('Can\'t fetch data ! Please refresh or check your connnection !');
            });
        }
    };
    ProductsComponent.prototype.editProductMode = function (id) {
        this.getAllData();
        this.showProductList = false;
        this.searchProductMode = false;
        this.editMode = true;
        this.showProductInfo = true;
        this.showProductAttributes = false;
        this.showPictures = false;
        this.showSpecificationAttributes = false;
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
    };
    ProductsComponent.prototype.editProduct = function () {
        var _this = this;
        this.loadingProduct = true;
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
        this.currentProduct["Name"] = this.Name;
        this.currentProduct["FullDescription"] = this.FullDescription;
        this.currentProduct["Price"] = this.Price;
        this.currentProduct["CategoryId"] = this.CategoryId;
        this.currentProduct["StoreId"] = this.StoreId;
        this.currentProduct["ManufacturerId"] = this.ManufacturerId;
        this.currentProduct["Sku"] = this.Sku;
        this.currentProduct["Published"] = this.Published;
        this.currentProduct["Gtin"] = this.Gtin;
        this.currentProduct["ManufacturerPartNumber"] = this.ManufacturerPartNumber;
        this.currentProduct["ShowOnHomePage"] = this.ShowOnHomePage;
        this.currentProduct["MarkAsNew"] = this.MarkAsNew;
        this.currentProduct["DisplayOrder"] = this.DisplayOrder;
        this.currentProduct["StockQuantity"] = this.StockQuantity;
        console.log(this.currentProduct);
        this.productService.updateProduct(this.currentProduct)
            .subscribe(function (response) {
            _this.loadingProduct = false;
            alert("Product Updated !");
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.deleteProduct = function (id) {
        var _this = this;
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.productService.deleteProduct(+id.name)
                .subscribe(function (data) {
                alert('Deleted !');
                _this.getProducts(_this.currentPageNumber);
            }, function (error) {
                console.log(error);
                alert('Can\'t fetch data ! Please refresh or check your connnection !');
            });
        }
    };
    ProductsComponent.prototype.getProducts = function (page) {
        var _this = this;
        this.loading = true;
        this.productService.getAllProducts(page)
            .subscribe(function (response) {
            _this.loading = false;
            _this.currentPageNumber = page;
            _this.products = (response.json().Data);
            //this.product = JSON.parse(this.products);
            console.log((_this.products));
            //  this.attribute =[this.attributes];
        }, function (error) {
            console.log(error);
            alert("Can't fetch Product data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.searchProduct = function () {
        var _this = this;
        this.loading = true;
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
        });
        this.productService.searchProduct(this.searchProductParameters)
            .subscribe(function (response) {
            _this.loading = false;
            _this.showSearchedProductList = true;
            _this.showProductList = false;
            _this.searchedProducts = (response.json().Data);
        }, function (error) {
            console.log(error);
            alert("Can't fetch search data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.getAttributes = function () {
        var _this = this;
        this.productAttributeService.getAttributes()
            .subscribe(function (response) {
            _this.product_attributes = (response.json().Data);
        }, function (error) {
            console.log(error);
            alert("Can't fetch Product Attributes data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.getSpecAttributes = function () {
        var _this = this;
        this.productService.getSpecAttributes()
            .subscribe(function (response) {
            _this.specification_attributes = (response.json().Data);
        }, function (error) {
            console.log(error);
            alert("Can't fetch Spec Attributes data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.getCategory = function () {
        var _this = this;
        this.productService.getCategory()
            .subscribe(function (response) {
            _this.categories = (response.json().Data);
        }, function (error) {
            console.log(error);
            alert("Can't fetch Category data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.getAllData = function () {
        //this.getProducts(0);
        this.getCategory();
        this.getManufacturers();
        this.getStores();
        this.getVendors();
        this.getAttributes();
        this.getSpecAttributes();
    };
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
    ProductsComponent.prototype.showList = function () {
        this.editMode = false;
        this.showProductList = true;
        this.searchProductMode = true;
        this.showSearchedProductList = false;
    };
    ProductsComponent.prototype.showToggle = function (toggle) {
        switch (toggle) {
            case 1: {
                this.showProductInfo = true;
                this.showPictures = false;
                this.showProductAttributes = false;
                this.showSpecificationAttributes = false;
                break;
            }
            case 2: {
                this.showProductInfo = false;
                this.showPictures = true;
                this.showProductAttributes = false;
                this.showSpecificationAttributes = false;
                // this.getPicture();
                break;
            }
            case 3: {
                this.showProductInfo = false;
                this.showPictures = false;
                this.showProductAttributes = true;
                this.showCurrentAttributeForm = true;
                this.showSpecificationAttributes = false;
                this.getCurrentAttributes();
                break;
            }
            case 4: {
                this.showProductInfo = false;
                this.showPictures = false;
                this.showProductAttributes = false;
                this.showSpecificationAttributes = true;
                //this.getCurrentSpecAttributes();
                break;
            }
            default: {
                this.showProductInfo = true;
                this.showPictures = false;
                this.showProductAttributes = false;
                this.showSpecificationAttributes = false;
                break;
            }
        }
    };
    ProductsComponent.prototype.getManufacturers = function () {
        var _this = this;
        this.productService.getManufacturers()
            .subscribe(function (response) {
            _this.loading = false;
            _this.manufacturers = (response.json().Data);
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.getVendors = function () {
        var _this = this;
        this.productService.getVendors()
            .subscribe(function (response) {
            _this.loading = false;
            _this.vendors = (response.json().Data);
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.getStores = function () {
        var _this = this;
        this.productService.getStores()
            .subscribe(function (response) {
            _this.loading = false;
            _this.stores = (response.json().Data);
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductsComponent.prototype.addProductMode = function () {
        this.searchProductMode = false;
        this.showProductList = false;
        this.addNewProduct = true;
        this.showToggle(1);
        this.getAllData();
    };
    ProductsComponent.prototype.convertStringtoNumber = function () {
        for (var index in this.CategoryId) {
            this.CategoryId[index] = +this.CategoryId[index];
        }
        for (var index in this.ManufacturerId) {
            this.ManufacturerId[index] = +this.ManufacturerId[index];
        }
        for (var index in this.StoreId) {
            this.StoreId[index] = +this.StoreId[index];
        }
    };
    ProductsComponent.prototype.getCurrentProduct = function (id) {
        return this.products.filter(function (product) { return product.Id == id; });
    };
    ProductsComponent.prototype.getCurrentAttributeName = function (id) {
        return this.product_attributes.filter(function (attribute) { return attribute.Id == id; });
    };
    ProductsComponent.prototype.checkCurrentAttributeId = function (id) {
        return this.productAttributeFields.filter(function (attribute) { return attribute.ProductAttributeId == id; });
    };
    ProductsComponent.prototype.getCurrentAttributes = function () {
        var _this = this;
        this.addAttributeMode = false;
        this.productService.getCurrentAttributes(this.Id)
            .subscribe(function (response) {
            _this.productAttributeFields = (response.json().Data);
            console.log("Attributes Retrieved");
            console.log(_this.productAttributeFields);
            _this.addAttributeMode = true;
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    // getCurrentSpecAttributes(){
    //     this.addSpecAttributeMode=false;
    //     this.loading=true;
    //     this.productService.getCurrentSpecAttributes(this.Id)
    //         .subscribe(
    //         (response) => {
    //             this.specAttributeFields = (response.json().Data);
    //             console.log(response.json());
    //             console.log(this.specAttributeFields);
    //             this.addSpecAttributeMode=true
    //             this.loading=false;
    //         },
    //         (error) =>      {
    //                 console.log(error);
    //                 alert("Can't fetch data ! Please refresh or check your connnection !");
    //               }
    //         );
    // }
    // addCurrentAttributeMode(){
    //     this.addAttributeMode=false;
    //     this.showCurrentAttributeList=false;
    //     this.showCurrentAttributeForm=true;
    //
    // }
    ProductsComponent.prototype.addAttribute = function () {
        var _this = this;
        this.current_attribute_id = this.productAttributeForm.value.current_attribute_id;
        var checkId = this.checkCurrentAttributeId(this.current_attribute_id);
        console.log(checkId);
        if (checkId.length != 0) {
            alert("Attribute already present !");
        }
        else {
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
                .subscribe(function (response) {
                _this.getCurrentAttributes();
                _this.current_attribute = [];
                alert("Added !");
            }, function (error) {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
            });
        }
    };
    ProductsComponent.prototype.deleteCurrentAttribute = function (id) {
        var _this = this;
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.productService.deleteAttribute(+id.name)
                .subscribe(function (data) {
                alert('Deleted !');
                _this.getCurrentAttributes();
            }, function (error) {
                console.log(error);
                alert('Can\'t fetch data ! Please refresh or check your connnection !');
            });
        }
    };
    return ProductsComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('f'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _a || Object)
], ProductsComponent.prototype, "productForm", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('t'),
    __metadata("design:type", typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _b || Object)
], ProductsComponent.prototype, "productEditForm", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('s'),
    __metadata("design:type", typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _c || Object)
], ProductsComponent.prototype, "productSearchForm", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('q'),
    __metadata("design:type", typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _d || Object)
], ProductsComponent.prototype, "productAttributeForm", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('u'),
    __metadata("design:type", typeof (_e = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _e || Object)
], ProductsComponent.prototype, "specAttributeForm", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('g'),
    __metadata("design:type", typeof (_f = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _f || Object)
], ProductsComponent.prototype, "attributeForm", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Boolean)
], ProductsComponent.prototype, "multiple", void 0);
ProductsComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app-products',
        template: __webpack_require__("../../../../../src/app/layout/products/products.component.html"),
        styles: [__webpack_require__("../../../../../src/app/layout/products/products.component.scss")]
    })
    /* This is still in development ! Has bugs ! */
    ,
    __metadata("design:paramtypes", [typeof (_g = typeof __WEBPACK_IMPORTED_MODULE_2__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_http__["b" /* Http */]) === "function" && _g || Object, typeof (_h = typeof __WEBPACK_IMPORTED_MODULE_3__product_service__["a" /* ProductService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__product_service__["a" /* ProductService */]) === "function" && _h || Object, typeof (_j = typeof __WEBPACK_IMPORTED_MODULE_4__product_attributes_product_attributes_service__["a" /* ProductAttributesService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__product_attributes_product_attributes_service__["a" /* ProductAttributesService */]) === "function" && _j || Object])
], ProductsComponent);

var _a, _b, _c, _d, _e, _f, _g, _h, _j;
//# sourceMappingURL=products.component.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/products.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__products_routing_module__ = __webpack_require__("../../../../../src/app/layout/products/products-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__products_component__ = __webpack_require__("../../../../../src/app/layout/products/products.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared__ = __webpack_require__("../../../../../src/app/shared/index.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__filter_pipe__ = __webpack_require__("../../../../../src/app/layout/products/filter.pipe.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__node_modules_angular2_image_upload_src_image_upload_module__ = __webpack_require__("../../../../angular2-image-upload/src/image-upload.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__product_service__ = __webpack_require__("../../../../../src/app/layout/products/product.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9_ngx_pagination__ = __webpack_require__("../../../../ngx-pagination/dist/ngx-pagination.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__product_attributes_product_attributes_service__ = __webpack_require__("../../../../../src/app/layout/products/product-attributes/product-attributes.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__product_pictures_product_pictures_component__ = __webpack_require__("../../../../../src/app/layout/products/product-pictures/product-pictures.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__product_pictures_product_pictures_service__ = __webpack_require__("../../../../../src/app/layout/products/product-pictures/product-pictures.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__link_product_spec_attributes_link_product_spec_attributes_component__ = __webpack_require__("../../../../../src/app/layout/products/link-product-spec-attributes/link-product-spec-attributes.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__link_product_spec_attributes_link_product_spec_attributes_service__ = __webpack_require__("../../../../../src/app/layout/products/link-product-spec-attributes/link-product-spec-attributes.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProductsModule", function() { return ProductsModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};















// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';
var ProductsModule = (function () {
    function ProductsModule() {
    }
    return ProductsModule;
}());
ProductsModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["h" /* CommonModule */],
            __WEBPACK_IMPORTED_MODULE_2__products_routing_module__["a" /* ProductsRoutingModule */],
            __WEBPACK_IMPORTED_MODULE_4__shared__["b" /* PageHeaderModule */],
            __WEBPACK_IMPORTED_MODULE_5__angular_forms__["a" /* FormsModule */],
            __WEBPACK_IMPORTED_MODULE_7__node_modules_angular2_image_upload_src_image_upload_module__["a" /* ImageUploadModule */].forRoot(),
            __WEBPACK_IMPORTED_MODULE_9_ngx_pagination__["a" /* NgxPaginationModule */],
        ],
        providers: [__WEBPACK_IMPORTED_MODULE_8__product_service__["a" /* ProductService */], __WEBPACK_IMPORTED_MODULE_10__product_attributes_product_attributes_service__["a" /* ProductAttributesService */], __WEBPACK_IMPORTED_MODULE_12__product_pictures_product_pictures_service__["a" /* ProductPicturesService */], __WEBPACK_IMPORTED_MODULE_14__link_product_spec_attributes_link_product_spec_attributes_service__["a" /* LinkProductSpecAttributesService */]],
        declarations: [__WEBPACK_IMPORTED_MODULE_3__products_component__["a" /* ProductsComponent */], __WEBPACK_IMPORTED_MODULE_6__filter_pipe__["a" /* FilterPipe */], __WEBPACK_IMPORTED_MODULE_11__product_pictures_product_pictures_component__["a" /* ProductPicturesComponent */], __WEBPACK_IMPORTED_MODULE_13__link_product_spec_attributes_link_product_spec_attributes_component__["a" /* LinkProductSpecAttributesComponent */]]
    })
], ProductsModule);

//# sourceMappingURL=products.module.js.map

/***/ }),

/***/ "../../../../ngx-pagination/dist/ngx-pagination.js":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* unused harmony export ɵb */
/* unused harmony export ɵa */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return NgxPaginationModule; });
/* unused harmony export PaginationService */
/* unused harmony export PaginationControlsComponent */
/* unused harmony export PaginationControlsDirective */
/* unused harmony export PaginatePipe */



var PaginationService = (function () {
    function PaginationService() {
        this.change = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
        this.instances = {};
        this.DEFAULT_ID = 'DEFAULT_PAGINATION_ID';
    }
    PaginationService.prototype.defaultId = function () { return this.DEFAULT_ID; };
    PaginationService.prototype.register = function (instance) {
        if (!instance.id) {
            instance.id = this.DEFAULT_ID;
        }
        if (!this.instances[instance.id]) {
            this.instances[instance.id] = instance;
            this.change.emit(instance.id);
        }
        else {
            var changed = this.updateInstance(instance);
            if (changed) {
                this.change.emit(instance.id);
            }
        }
    };
    /**
     * Check each property of the instance and update any that have changed. Return
     * true if any changes were made, else return false.
     */
    PaginationService.prototype.updateInstance = function (instance) {
        var changed = false;
        for (var prop in this.instances[instance.id]) {
            if (instance[prop] !== this.instances[instance.id][prop]) {
                this.instances[instance.id][prop] = instance[prop];
                changed = true;
            }
        }
        return changed;
    };
    /**
     * Returns the current page number.
     */
    PaginationService.prototype.getCurrentPage = function (id) {
        if (this.instances[id]) {
            return this.instances[id].currentPage;
        }
    };
    /**
     * Sets the current page number.
     */
    PaginationService.prototype.setCurrentPage = function (id, page) {
        if (this.instances[id]) {
            var instance = this.instances[id];
            var maxPage = Math.ceil(instance.totalItems / instance.itemsPerPage);
            if (page <= maxPage && 1 <= page) {
                this.instances[id].currentPage = page;
                this.change.emit(id);
            }
        }
    };
    /**
     * Sets the value of instance.totalItems
     */
    PaginationService.prototype.setTotalItems = function (id, totalItems) {
        if (this.instances[id] && 0 <= totalItems) {
            this.instances[id].totalItems = totalItems;
            this.change.emit(id);
        }
    };
    /**
     * Sets the value of instance.itemsPerPage.
     */
    PaginationService.prototype.setItemsPerPage = function (id, itemsPerPage) {
        if (this.instances[id]) {
            this.instances[id].itemsPerPage = itemsPerPage;
            this.change.emit(id);
        }
    };
    /**
     * Returns a clone of the pagination instance object matching the id. If no
     * id specified, returns the instance corresponding to the default id.
     */
    PaginationService.prototype.getInstance = function (id) {
        if (id === void 0) { id = this.DEFAULT_ID; }
        if (this.instances[id]) {
            return this.clone(this.instances[id]);
        }
        return {};
    };
    /**
     * Perform a shallow clone of an object.
     */
    PaginationService.prototype.clone = function (obj) {
        var target = {};
        for (var i in obj) {
            if (obj.hasOwnProperty(i)) {
                target[i] = obj[i];
            }
        }
        return target;
    };
    return PaginationService;
}());

var LARGE_NUMBER = Number.MAX_SAFE_INTEGER;
var PaginatePipe = (function () {
    function PaginatePipe(service) {
        this.service = service;
        // store the values from the last time the pipe was invoked
        this.state = {};
    }
    PaginatePipe.prototype.transform = function (collection, args) {
        // When an observable is passed through the AsyncPipe, it will output
        // `null` until the subscription resolves. In this case, we want to
        // use the cached data from the `state` object to prevent the NgFor
        // from flashing empty until the real values arrive.
        if (args instanceof Array) {
            // compatible with angular2 before beta16
            args = args[0];
        }
        if (!(collection instanceof Array)) {
            var _id = args.id || this.service.defaultId;
            if (this.state[_id]) {
                return this.state[_id].slice;
            }
            else {
                return collection;
            }
        }
        var serverSideMode = args.totalItems && args.totalItems !== collection.length;
        var instance = this.createInstance(collection, args);
        var id = instance.id;
        var start, end;
        var perPage = instance.itemsPerPage;
        this.service.register(instance);
        if (!serverSideMode && collection instanceof Array) {
            perPage = +perPage || LARGE_NUMBER;
            start = (instance.currentPage - 1) * perPage;
            end = start + perPage;
            var isIdentical = this.stateIsIdentical(id, collection, start, end);
            if (isIdentical) {
                return this.state[id].slice;
            }
            else {
                var slice = collection.slice(start, end);
                this.saveState(id, collection, slice, start, end);
                this.service.change.emit(id);
                return slice;
            }
        }
        // save the state for server-side collection to avoid null
        // flash as new data loads.
        this.saveState(id, collection, collection, start, end);
        return collection;
    };
    /**
     * Create an PaginationInstance object, using defaults for any optional properties not supplied.
     */
    PaginatePipe.prototype.createInstance = function (collection, args) {
        var config = args;
        this.checkConfig(config);
        return {
            id: config.id || this.service.defaultId(),
            itemsPerPage: config.itemsPerPage || 0,
            currentPage: config.currentPage || 1,
            totalItems: config.totalItems || collection.length
        };
    };
    /**
     * Ensure the argument passed to the filter contains the required properties.
     */
    PaginatePipe.prototype.checkConfig = function (config) {
        var required = ['itemsPerPage', 'currentPage'];
        var missing = required.filter(function (prop) { return !(prop in config); });
        if (0 < missing.length) {
            throw new Error("PaginatePipe: Argument is missing the following required properties: " + missing.join(', '));
        }
    };
    /**
     * To avoid returning a brand new array each time the pipe is run, we store the state of the sliced
     * array for a given id. This means that the next time the pipe is run on this collection & id, we just
     * need to check that the collection, start and end points are all identical, and if so, return the
     * last sliced array.
     */
    PaginatePipe.prototype.saveState = function (id, collection, slice, start, end) {
        this.state[id] = {
            collection: collection,
            size: collection.length,
            slice: slice,
            start: start,
            end: end
        };
    };
    /**
     * For a given id, returns true if the collection, size, start and end values are identical.
     */
    PaginatePipe.prototype.stateIsIdentical = function (id, collection, start, end) {
        var state = this.state[id];
        if (!state) {
            return false;
        }
        var isMetaDataIdentical = state.size === collection.length &&
            state.start === start &&
            state.end === end;
        if (!isMetaDataIdentical) {
            return false;
        }
        return state.slice.every(function (element, index) { return element === collection[start + index]; });
    };
    return PaginatePipe;
}());
PaginatePipe.decorators = [
    { type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Pipe"], args: [{
                name: 'paginate',
                pure: false
            },] },
];
/** @nocollapse */
PaginatePipe.ctorParameters = function () { return [
    { type: PaginationService, },
]; };

/**
 * The default template and styles for the pagination links are borrowed directly
 * from Zurb Foundation 6: http://foundation.zurb.com/sites/docs/pagination.html
 */
/**
 * The default template and styles for the pagination links are borrowed directly
 * from Zurb Foundation 6: http://foundation.zurb.com/sites/docs/pagination.html
 */ var DEFAULT_TEMPLATE = "\n    <pagination-template  #p=\"paginationApi\"\n                         [id]=\"id\"\n                         [maxSize]=\"maxSize\"\n                         (pageChange)=\"pageChange.emit($event)\">\n    <ul class=\"ngx-pagination\" \n        role=\"navigation\" \n        [attr.aria-label]=\"screenReaderPaginationLabel\" \n        *ngIf=\"!(autoHide && p.pages.length <= 1)\">\n\n        <li class=\"pagination-previous\" [class.disabled]=\"p.isFirstPage()\" *ngIf=\"directionLinks\"> \n            <a *ngIf=\"1 < p.getCurrent()\" (click)=\"p.previous()\" [attr.aria-label]=\"previousLabel + ' ' + screenReaderPageLabel\">\n                {{ previousLabel }} <span class=\"show-for-sr\">{{ screenReaderPageLabel }}</span>\n            </a>\n            <span *ngIf=\"p.isFirstPage()\">\n                {{ previousLabel }} <span class=\"show-for-sr\">{{ screenReaderPageLabel }}</span>\n            </span>\n        </li>\n\n        <li [class.current]=\"p.getCurrent() === page.value\" *ngFor=\"let page of p.pages\">\n            <a (click)=\"p.setCurrent(page.value)\" *ngIf=\"p.getCurrent() !== page.value\">\n                <span class=\"show-for-sr\">{{ screenReaderPageLabel }} </span>\n                <span>{{ page.label }}</span>\n            </a>\n            <div *ngIf=\"p.getCurrent() === page.value\">\n                <span class=\"show-for-sr\">{{ screenReaderCurrentLabel }} </span>\n                <span>{{ page.label }}</span> \n            </div>\n        </li>\n\n        <li class=\"pagination-next\" [class.disabled]=\"p.isLastPage()\" *ngIf=\"directionLinks\">\n            <a *ngIf=\"!p.isLastPage()\" (click)=\"p.next()\" [attr.aria-label]=\"nextLabel + ' ' + screenReaderPageLabel\">\n                 {{ nextLabel }} <span class=\"show-for-sr\">{{ screenReaderPageLabel }}</span>\n            </a>\n            <span *ngIf=\"p.isLastPage()\">\n                 {{ nextLabel }} <span class=\"show-for-sr\">{{ screenReaderPageLabel }}</span>\n            </span>\n        </li>\n\n    </ul>\n    </pagination-template>\n    ";
var DEFAULT_STYLES = "\n.ngx-pagination {\n  margin-left: 0;\n  margin-bottom: 1rem; }\n  .ngx-pagination::before, .ngx-pagination::after {\n    content: ' ';\n    display: table; }\n  .ngx-pagination::after {\n    clear: both; }\n  .ngx-pagination li {\n    -moz-user-select: none;\n    -webkit-user-select: none;\n    -ms-user-select: none;\n    margin-right: 0.0625rem;\n    border-radius: 0; }\n  .ngx-pagination li {\n    display: inline-block; }\n  .ngx-pagination a,\n  .ngx-pagination button {\n    color: #0a0a0a; \n    display: block;\n    padding: 0.1875rem 0.625rem;\n    border-radius: 0; }\n    .ngx-pagination a:hover,\n    .ngx-pagination button:hover {\n      background: #e6e6e6; }\n  .ngx-pagination .current {\n    padding: 0.1875rem 0.625rem;\n    background: #2199e8;\n    color: #fefefe;\n    cursor: default; }\n  .ngx-pagination .disabled {\n    padding: 0.1875rem 0.625rem;\n    color: #cacaca;\n    cursor: default; } \n    .ngx-pagination .disabled:hover {\n      background: transparent; }\n  .ngx-pagination .ellipsis::after {\n    content: '\u2026';\n    padding: 0.1875rem 0.625rem;\n    color: #0a0a0a; }\n  .ngx-pagination a, .ngx-pagination button {\n    cursor: pointer; }\n\n.ngx-pagination .pagination-previous a::before,\n.ngx-pagination .pagination-previous.disabled::before { \n  content: '\u00AB';\n  display: inline-block;\n  margin-right: 0.5rem; }\n\n.ngx-pagination .pagination-next a::after,\n.ngx-pagination .pagination-next.disabled::after {\n  content: '\u00BB';\n  display: inline-block;\n  margin-left: 0.5rem; }\n\n.ngx-pagination .show-for-sr {\n  position: absolute !important;\n  width: 1px;\n  height: 1px;\n  overflow: hidden;\n  clip: rect(0, 0, 0, 0); }";

/**
 * The default pagination controls component. Actually just a default implementation of a custom template.
 */
var PaginationControlsComponent = (function () {
    function PaginationControlsComponent() {
        this.maxSize = 7;
        this.previousLabel = 'Previous';
        this.nextLabel = 'Next';
        this.screenReaderPaginationLabel = 'Pagination';
        this.screenReaderPageLabel = 'page';
        this.screenReaderCurrentLabel = "You're on page";
        this.pageChange = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
        this._directionLinks = true;
        this._autoHide = false;
    }
    Object.defineProperty(PaginationControlsComponent.prototype, "directionLinks", {
        get: function () {
            return this._directionLinks;
        },
        set: function (value) {
            this._directionLinks = !!value && value !== 'false';
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(PaginationControlsComponent.prototype, "autoHide", {
        get: function () {
            return this._autoHide;
        },
        set: function (value) {
            this._autoHide = !!value && value !== 'false';
        },
        enumerable: true,
        configurable: true
    });
    return PaginationControlsComponent;
}());
PaginationControlsComponent.decorators = [
    { type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"], args: [{
                selector: 'pagination-controls',
                template: DEFAULT_TEMPLATE,
                styles: [DEFAULT_STYLES],
                changeDetection: __WEBPACK_IMPORTED_MODULE_0__angular_core__["ChangeDetectionStrategy"].OnPush,
                encapsulation: __WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewEncapsulation"].None
            },] },
];
/** @nocollapse */
PaginationControlsComponent.ctorParameters = function () { return []; };
PaginationControlsComponent.propDecorators = {
    'id': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'maxSize': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'directionLinks': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'autoHide': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'previousLabel': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'nextLabel': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'screenReaderPaginationLabel': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'screenReaderPageLabel': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'screenReaderCurrentLabel': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'pageChange': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"] },],
};

/**
 * This directive is what powers all pagination controls components, including the default one.
 * It exposes an API which is hooked up to the PaginationService to keep the PaginatePipe in sync
 * with the pagination controls.
 */
var PaginationControlsDirective = (function () {
    function PaginationControlsDirective(service, changeDetectorRef) {
        var _this = this;
        this.service = service;
        this.changeDetectorRef = changeDetectorRef;
        this.maxSize = 7;
        this.pageChange = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
        this.pages = [];
        this.changeSub = this.service.change
            .subscribe(function (id) {
            if (_this.id === id) {
                _this.updatePageLinks();
                _this.changeDetectorRef.markForCheck();
                _this.changeDetectorRef.detectChanges();
            }
        });
    }
    PaginationControlsDirective.prototype.ngOnInit = function () {
        if (this.id === undefined) {
            this.id = this.service.defaultId();
        }
        this.updatePageLinks();
    };
    PaginationControlsDirective.prototype.ngOnChanges = function (changes) {
        this.updatePageLinks();
    };
    PaginationControlsDirective.prototype.ngOnDestroy = function () {
        this.changeSub.unsubscribe();
    };
    /**
     * Go to the previous page
     */
    PaginationControlsDirective.prototype.previous = function () {
        this.checkValidId();
        this.setCurrent(this.getCurrent() - 1);
    };
    /**
     * Go to the next page
     */
    PaginationControlsDirective.prototype.next = function () {
        this.checkValidId();
        this.setCurrent(this.getCurrent() + 1);
    };
    /**
     * Returns true if current page is first page
     */
    PaginationControlsDirective.prototype.isFirstPage = function () {
        return this.getCurrent() === 1;
    };
    /**
     * Returns true if current page is last page
     */
    PaginationControlsDirective.prototype.isLastPage = function () {
        return this.getLastPage() === this.getCurrent();
    };
    /**
     * Set the current page number.
     */
    PaginationControlsDirective.prototype.setCurrent = function (page) {
        this.pageChange.emit(page);
    };
    /**
     * Get the current page number.
     */
    PaginationControlsDirective.prototype.getCurrent = function () {
        return this.service.getCurrentPage(this.id);
    };
    /**
     * Returns the last page number
     */
    PaginationControlsDirective.prototype.getLastPage = function () {
        var inst = this.service.getInstance(this.id);
        if (inst.totalItems < 1) {
            // when there are 0 or fewer (an error case) items, there are no "pages" as such,
            // but it makes sense to consider a single, empty page as the last page.
            return 1;
        }
        return Math.ceil(inst.totalItems / inst.itemsPerPage);
    };
    PaginationControlsDirective.prototype.checkValidId = function () {
        if (!this.service.getInstance(this.id).id) {
            console.warn("PaginationControlsDirective: the specified id \"" + this.id + "\" does not match any registered PaginationInstance");
        }
    };
    /**
     * Updates the page links and checks that the current page is valid. Should run whenever the
     * PaginationService.change stream emits a value matching the current ID, or when any of the
     * input values changes.
     */
    PaginationControlsDirective.prototype.updatePageLinks = function () {
        var _this = this;
        var inst = this.service.getInstance(this.id);
        var correctedCurrentPage = this.outOfBoundCorrection(inst);
        if (correctedCurrentPage !== inst.currentPage) {
            setTimeout(function () {
                _this.setCurrent(correctedCurrentPage);
                _this.pages = _this.createPageArray(inst.currentPage, inst.itemsPerPage, inst.totalItems, _this.maxSize);
            });
        }
        else {
            this.pages = this.createPageArray(inst.currentPage, inst.itemsPerPage, inst.totalItems, this.maxSize);
        }
    };
    /**
     * Checks that the instance.currentPage property is within bounds for the current page range.
     * If not, return a correct value for currentPage, or the current value if OK.
     */
    PaginationControlsDirective.prototype.outOfBoundCorrection = function (instance) {
        var totalPages = Math.ceil(instance.totalItems / instance.itemsPerPage);
        if (totalPages < instance.currentPage && 0 < totalPages) {
            return totalPages;
        }
        else if (instance.currentPage < 1) {
            return 1;
        }
        return instance.currentPage;
    };
    /**
     * Returns an array of Page objects to use in the pagination controls.
     */
    PaginationControlsDirective.prototype.createPageArray = function (currentPage, itemsPerPage, totalItems, paginationRange) {
        // paginationRange could be a string if passed from attribute, so cast to number.
        paginationRange = +paginationRange;
        var pages = [];
        var totalPages = Math.ceil(totalItems / itemsPerPage);
        var halfWay = Math.ceil(paginationRange / 2);
        var isStart = currentPage <= halfWay;
        var isEnd = totalPages - halfWay < currentPage;
        var isMiddle = !isStart && !isEnd;
        var ellipsesNeeded = paginationRange < totalPages;
        var i = 1;
        while (i <= totalPages && i <= paginationRange) {
            var label = void 0;
            var pageNumber = this.calculatePageNumber(i, currentPage, paginationRange, totalPages);
            var openingEllipsesNeeded = (i === 2 && (isMiddle || isEnd));
            var closingEllipsesNeeded = (i === paginationRange - 1 && (isMiddle || isStart));
            if (ellipsesNeeded && (openingEllipsesNeeded || closingEllipsesNeeded)) {
                label = '...';
            }
            else {
                label = pageNumber;
            }
            pages.push({
                label: label,
                value: pageNumber
            });
            i++;
        }
        return pages;
    };
    /**
     * Given the position in the sequence of pagination links [i],
     * figure out what page number corresponds to that position.
     */
    PaginationControlsDirective.prototype.calculatePageNumber = function (i, currentPage, paginationRange, totalPages) {
        var halfWay = Math.ceil(paginationRange / 2);
        if (i === paginationRange) {
            return totalPages;
        }
        else if (i === 1) {
            return i;
        }
        else if (paginationRange < totalPages) {
            if (totalPages - halfWay < currentPage) {
                return totalPages - paginationRange + i;
            }
            else if (halfWay < currentPage) {
                return currentPage - halfWay + i;
            }
            else {
                return i;
            }
        }
        else {
            return i;
        }
    };
    return PaginationControlsDirective;
}());
PaginationControlsDirective.decorators = [
    { type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Directive"], args: [{
                selector: 'pagination-template,[pagination-template]',
                exportAs: 'paginationApi'
            },] },
];
/** @nocollapse */
PaginationControlsDirective.ctorParameters = function () { return [
    { type: PaginationService, },
    { type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["ChangeDetectorRef"], },
]; };
PaginationControlsDirective.propDecorators = {
    'id': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'maxSize': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"] },],
    'pageChange': [{ type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"] },],
};

var NgxPaginationModule = (function () {
    function NgxPaginationModule() {
    }
    return NgxPaginationModule;
}());
NgxPaginationModule.decorators = [
    { type: __WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"], args: [{
                imports: [__WEBPACK_IMPORTED_MODULE_1__angular_common__["h" /* CommonModule */]],
                declarations: [
                    PaginatePipe,
                    PaginationControlsComponent,
                    PaginationControlsDirective
                ],
                providers: [PaginationService],
                exports: [PaginatePipe, PaginationControlsComponent, PaginationControlsDirective]
            },] },
];
/** @nocollapse */
NgxPaginationModule.ctorParameters = function () { return []; };

/**
 * Generated bundle index. Do not edit.
 */




/***/ })

});
//# sourceMappingURL=4.chunk.js.map