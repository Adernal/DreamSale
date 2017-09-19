webpackJsonp([14],{

/***/ "../../../../../src/app/layout/products/product-reviews/filter.pipe.ts":
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
    FilterPipe.prototype.transform = function (value, filteredReviews, propName, propName2, propName3) {
        // if(value.length===0 || filteredReviews===''){
        //   return value;
        // }
        // const resultArray=[];
        // for(const item of value){
        //   if(item[propName].toLowerCase().indexOf(filteredReviews.toLowerCase())>=0 ||
        //       item[propName2].toLowerCase().indexOf(filteredReviews.toLowerCase())>=0 ||
        //     item[propName3].toLowerCase().indexOf(filteredReviews.toLowerCase())>=0){
        //     resultArray.push(item);
        //   }
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

/***/ "../../../../../src/app/layout/products/product-reviews/product-reviews-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__product_reviews_component__ = __webpack_require__("../../../../../src/app/layout/products/product-reviews/product-reviews.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProductReviewsRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



// import { ProductReviewsComponent } from './product-tags/product-tags.component';
var routes = [
    { path: '', component: __WEBPACK_IMPORTED_MODULE_2__product_reviews_component__["a" /* ProductReviewsComponent */] },
];
var ProductReviewsRoutingModule = (function () {
    function ProductReviewsRoutingModule() {
    }
    return ProductReviewsRoutingModule;
}());
ProductReviewsRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */].forChild(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */]]
    })
], ProductReviewsRoutingModule);

//# sourceMappingURL=product-reviews-routing.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/product-reviews/product-reviews.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\n<h1>  Product Reviews Management </h1>\n     <hr>\n    <!-- <div class=\"row\" *ngIf=\"editMode\">\n\n\n      <form role=\"form\" (ngSubmit)=\"addAttribute(f)\" #f=\"ngForm\" class=\"container\">\n        <div class=\"row\">\n          <div class=\"col-lg-6\">\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Product Review</label>\n                <input class=\"form-control\" ngModel name=\"attribute_name\" required placeholder=\"Enter Attribute Name\" *ngIf=\"!editMode\">\n                <input class=\"form-control\" [(ngModel)]=\"attribute_name\" name=\"attribute_name\" required value=\"{{attribute_name}}\" *ngIf=\"editMode\">\n\n            </fieldset>\n\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"!editMode\">Add Attribute</button>\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"editMode\">Edit Attribute</button>\n            \n\n        </div>\n\n\n  </div>\n</form>\n  </div> -->\n <hr>\n </div>\n    <div class=\"row\">\n\n\n            <div class=\"card mb-3\">\n                <div class=\"card-header\">\n                    <h2>Review List</h2>\n                    <!-- <input type=\"text\" placeholder=\"Search Reviews by Product or Customer or Approved\" [(ngModel)]=\"filteredReviews\" style=\"width:100%\"> -->\n                </div>\n                <div class=\"card-block\" style=\"width:100%;\">\n                    <table class=\"table table-bordered\" style=\"table-layout:fixed\">\n                        <thead class=\"thead-inverse\">\n                        <tr>\n                            <th>Store</th>\n                            <th>Product</th>\n                            <th>Customer</th>\n                            <th>Title</th>\n                            <th>Description</th>\n                            <th>Rating</th>\n                            <th>Approved</th>\n                            <th>Created On</th>\n                            <th>Action</th>\n\n                        </tr>\n                        </thead>\n                        <tbody>\n                            <tr *ngFor=\"let i= inex;let review of review\">\n                                <td>{{review.StoreName}}</td>\n                                <td>{{review.ProductName}}</td>\n                                <td>{{review.CustomerInfo}}</td>\n                                <td>{{review.Title}}</td>\n                                <td>{{review.ReviewText}}</td>\n                                <td>{{review.Rating}}</td>\n                                <td>{{review.IsApproved}}</td>\n                                <td>{{review.CreatedOn}}</td>\n                                <td><button type=\"button\" name=\"{{i}}\" class=\"btn btn-primary\" (click)=\"editReviewMode(c)\" #c><i class=\"fa fa-edit\"></i></button>\n      <button type=\"button\" name=\"{{i}}\" class=\"btn btn-danger\" (click)=\"deleteReview(d)\" #d><i class=\"fa fa-times\"></i></button></td>\n\n                            </tr>\n\n\n                        </tbody>\n                    </table>\n                </div>\n            </div>\n\n\n\n    </div>\n"

/***/ }),

/***/ "../../../../../src/app/layout/products/product-reviews/product-reviews.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/layout/products/product-reviews/product-reviews.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__product_reviews_service__ = __webpack_require__("../../../../../src/app/layout/products/product-reviews/product-reviews.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProductReviewsComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var ProductReviewsComponent = (function () {
    function ProductReviewsComponent(reviewService) {
        this.reviewService = reviewService;
        this.editMode = false;
        this.filteredReviews = '';
    }
    ProductReviewsComponent.prototype.ngOnInit = function () {
        this.getReviews();
    };
    ProductReviewsComponent.prototype.getReviews = function () {
        var _this = this;
        this.reviewService.getReviews()
            .subscribe(function (response) {
            _this.reviews = (response.json());
            _this.review = _this.reviews.Data;
            console.log(("Fetched Reviews"));
            //  this.attribute =[this.attributes];
        }, function (error) {
            console.log(error);
            alert('Can\'t fetch data ! Please refresh or check your connnection !');
        });
    };
    ProductReviewsComponent.prototype.deleteReview = function (id) {
        var _this = this;
        var confirmation = confirm('Are you sure you want to delete ?');
        if (confirmation) {
            this.Id = +this.review[+id.name].Id;
            this.deleteSelection[0] = this.Id;
            this.reviewService.deleteReviews(this.deleteSelection)
                .subscribe(function (data) {
                _this.getReviews();
                alert('Deleted !');
            }, function (error) {
                console.log(error);
                alert('Can\'t fetch data ! Please refresh or check your connnection !');
            });
        }
        if (this.editMode) {
            this.editMode = false;
        }
    };
    return ProductReviewsComponent;
}());
ProductReviewsComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app-product-reviews',
        template: __webpack_require__("../../../../../src/app/layout/products/product-reviews/product-reviews.component.html"),
        styles: [__webpack_require__("../../../../../src/app/layout/products/product-reviews/product-reviews.component.scss")]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__product_reviews_service__["a" /* ProductReviewsService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__product_reviews_service__["a" /* ProductReviewsService */]) === "function" && _a || Object])
], ProductReviewsComponent);

var _a;
//# sourceMappingURL=product-reviews.component.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/product-reviews/product-reviews.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__product_reviews_routing_module__ = __webpack_require__("../../../../../src/app/layout/products/product-reviews/product-reviews-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__product_reviews_component__ = __webpack_require__("../../../../../src/app/layout/products/product-reviews/product-reviews.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared__ = __webpack_require__("../../../../../src/app/shared/index.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__filter_pipe__ = __webpack_require__("../../../../../src/app/layout/products/product-reviews/filter.pipe.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__product_reviews_service__ = __webpack_require__("../../../../../src/app/layout/products/product-reviews/product-reviews.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProductReviewsModule", function() { return ProductReviewsModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};








var ProductReviewsModule = (function () {
    function ProductReviewsModule() {
    }
    return ProductReviewsModule;
}());
ProductReviewsModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */],
            __WEBPACK_IMPORTED_MODULE_2__product_reviews_routing_module__["a" /* ProductReviewsRoutingModule */],
            __WEBPACK_IMPORTED_MODULE_4__shared__["b" /* PageHeaderModule */],
            __WEBPACK_IMPORTED_MODULE_5__angular_forms__["a" /* FormsModule */]
        ],
        providers: [__WEBPACK_IMPORTED_MODULE_7__product_reviews_service__["a" /* ProductReviewsService */]],
        declarations: [__WEBPACK_IMPORTED_MODULE_3__product_reviews_component__["a" /* ProductReviewsComponent */], __WEBPACK_IMPORTED_MODULE_6__filter_pipe__["a" /* FilterPipe */]]
    })
], ProductReviewsModule);

//# sourceMappingURL=product-reviews.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/product-reviews/product-reviews.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProductReviewsService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var ProductReviewsService = (function () {
    function ProductReviewsService(http) {
        this.http = http;
    }
    ProductReviewsService.prototype.getReviews = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/ProductReviews');
    };
    ProductReviewsService.prototype.updateReviews = function (reviews, id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log(reviews);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Update?continueEditing=true', reviews[id], { headers: headers });
    };
    ProductReviewsService.prototype.deleteReviews = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        //console.log(reviews);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductReviews/DeleteSelected', id, { headers: headers });
    };
    return ProductReviewsService;
}());
ProductReviewsService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], ProductReviewsService);

var _a;
//# sourceMappingURL=product-reviews.service.js.map

/***/ })

});
//# sourceMappingURL=14.chunk.js.map