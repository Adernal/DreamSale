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
var ProductAttributesService = (function () {
    function ProductAttributesService(http) {
        this.http = http;
    }
    ProductAttributesService.prototype.storeAttributes = function (attributes) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        this.temp = attributes[attributes.length - 1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Add?continueEditing=true', this.temp, { headers: headers });
    };
    ProductAttributesService.prototype.getAttributes = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/0/2147483647');
    };
    ProductAttributesService.prototype.updateAttributes = function (attributes, id) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log(attributes);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Update?continueEditing=true', attributes[id], { headers: headers });
    };
    ProductAttributesService.prototype.deleteAttributes = function (attributes, id) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(attributes);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Delete/' + id, null, { headers: headers });
    };
    return ProductAttributesService;
}());
ProductAttributesService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], ProductAttributesService);
exports.ProductAttributesService = ProductAttributesService;
//# sourceMappingURL=product-attributes.service.js.map