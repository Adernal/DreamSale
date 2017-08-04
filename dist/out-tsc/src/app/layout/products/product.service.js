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
var http_1 = require("@angular/http");
var ProductService = (function () {
    function ProductService(http) {
        this.http = http;
    }
    // storeProduct(product) {
    //     const headers = new Headers({ 'Content-Type': 'application/json' });
    //     this.temp = product[product.length-1];
    //     return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Add?continueEditing=true', this.temp,
    //         { headers: headers });
    // }
    ProductService.prototype.getAttributes = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/0/2147483647');
    };
    ProductService.prototype.getSpecAttributes = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/0/2147483647');
    };
    ProductService.prototype.getCategory = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Categories');
    };
    return ProductService;
}());
ProductService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], ProductService);
exports.ProductService = ProductService;
//# sourceMappingURL=product.service.js.map