webpackJsonp([20],{

/***/ "../../../../../src/app/layout/online-customers/online-customers-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__online_customers_component__ = __webpack_require__("../../../../../src/app/layout/online-customers/online-customers.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OnlineCustomersRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var routes = [
    { path: '', component: __WEBPACK_IMPORTED_MODULE_2__online_customers_component__["a" /* OnlineCustomersComponent */] },
];
var OnlineCustomersRoutingModule = (function () {
    function OnlineCustomersRoutingModule() {
    }
    return OnlineCustomersRoutingModule;
}());
OnlineCustomersRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */].forChild(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */]]
    })
], OnlineCustomersRoutingModule);

//# sourceMappingURL=online-customers-routing.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/online-customers/online-customers.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\n<h1> Online Customers Management </h1>\n     <hr>\n    <div class=\"row\">\n        <div class=\"card mb-3\">\n                <div class=\"card-header\">\n                  <h2>Online Customers</h2>\n                </div>\n                <div class=\"card-block\" style=\"width:100%;\">\n                    <table class=\"table table-bordered\" style=\"table-layout:fixed\">\n                        <thead class=\"thead-inverse\">\n                        <tr>\n\n                            <th>Customer Info</th>\n                            <th>IP Address</th>\n                            <th>Location</th>\n                            <th>Last Activity</th>\n                            <th>Last Visited Page</th>\n\n\n\n                        </tr>\n                        </thead>\n                        <tbody>\n                          <tr *ngFor =\" let i = index ;let online_customer of online_customer\" >\n\n\n                          <td>{{online_customer.CustomerInfo}}</td>\n                          <td>{{online_customer.IpAddress}}</td>\n                          <td>{{online_customer.Location}}</td>\n                          <td>{{online_customer.LastActivity}}</td>\n                          <td>{{online_customer.LastVisitedPage}}</td>\n                        </tr>\n\n                        </tbody>\n                    </table>\n                </div>\n            </div>\n\n\n\n    </div>\n  </div>\n"

/***/ }),

/***/ "../../../../../src/app/layout/online-customers/online-customers.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/layout/online-customers/online-customers.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__online_customers_service__ = __webpack_require__("../../../../../src/app/layout/online-customers/online-customers.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OnlineCustomersComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var OnlineCustomersComponent = (function () {
    function OnlineCustomersComponent(online_customersService) {
        this.online_customersService = online_customersService;
    }
    OnlineCustomersComponent.prototype.ngOnInit = function () {
        this.getOnlineCustomers();
    };
    OnlineCustomersComponent.prototype.getOnlineCustomers = function () {
        var _this = this;
        this.online_customersService.getOnlineCustomers()
            .subscribe(function (response) {
            _this.online_customers = (response.json());
            _this.online_customer = _this.online_customers.Data;
            console.log((_this.online_customer));
        }, function (error) {
            console.log(error);
            alert('Can\'t fetch data ! Please refresh or check your connnection !');
        });
    };
    return OnlineCustomersComponent;
}());
OnlineCustomersComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app-online-customers',
        template: __webpack_require__("../../../../../src/app/layout/online-customers/online-customers.component.html"),
        styles: [__webpack_require__("../../../../../src/app/layout/online-customers/online-customers.component.scss")]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__online_customers_service__["a" /* OnlineCustomersService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__online_customers_service__["a" /* OnlineCustomersService */]) === "function" && _a || Object])
], OnlineCustomersComponent);

var _a;
//# sourceMappingURL=online-customers.component.js.map

/***/ }),

/***/ "../../../../../src/app/layout/online-customers/online-customers.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__online_customers_routing_module__ = __webpack_require__("../../../../../src/app/layout/online-customers/online-customers-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__online_customers_component__ = __webpack_require__("../../../../../src/app/layout/online-customers/online-customers.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared__ = __webpack_require__("../../../../../src/app/shared/index.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__online_customers_service__ = __webpack_require__("../../../../../src/app/layout/online-customers/online-customers.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "OnlineCustomersModule", function() { return OnlineCustomersModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};







//import { FilterPipe } from './filter.pipe';
// import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
//import { customerservice } from './product.service';
// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';
var OnlineCustomersModule = (function () {
    function OnlineCustomersModule() {
    }
    return OnlineCustomersModule;
}());
OnlineCustomersModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["h" /* CommonModule */],
            __WEBPACK_IMPORTED_MODULE_2__online_customers_routing_module__["a" /* OnlineCustomersRoutingModule */],
            __WEBPACK_IMPORTED_MODULE_4__shared__["b" /* PageHeaderModule */],
            __WEBPACK_IMPORTED_MODULE_5__angular_forms__["a" /* FormsModule */],
        ],
        providers: [__WEBPACK_IMPORTED_MODULE_6__online_customers_service__["a" /* OnlineCustomersService */]],
        declarations: [__WEBPACK_IMPORTED_MODULE_3__online_customers_component__["a" /* OnlineCustomersComponent */]]
    })
], OnlineCustomersModule);

//# sourceMappingURL=online-customers.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/online-customers/online-customers.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OnlineCustomersService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var OnlineCustomersService = (function () {
    function OnlineCustomersService(http) {
        this.http = http;
    }
    OnlineCustomersService.prototype.getOnlineCustomers = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/onlineCustomers/0/2147483647');
    };
    return OnlineCustomersService;
}());
OnlineCustomersService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], OnlineCustomersService);

var _a;
//# sourceMappingURL=online-customers.service.js.map

/***/ })

});
//# sourceMappingURL=20.chunk.js.map