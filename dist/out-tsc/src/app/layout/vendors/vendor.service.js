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
var VendorService = (function () {
    function VendorService(http) {
        this.http = http;
    }
    VendorService.prototype.storeVendor = function (Vendor) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        this.temp = Vendor[Vendor.length - 1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Vendor/CreateVendor?continueEditing=true', this.temp, { headers: headers });
    };
    VendorService.prototype.getVendor = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors');
    };
    VendorService.prototype.updateVendor = function (Vendor, id) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log(Vendor);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors/Update?continueEditing=true', Vendor[id], { headers: headers });
    };
    VendorService.prototype.deleteVendor = function (Vendor, id) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(Vendor);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors/Delete?id=' + id, null, { headers: headers });
    };
    return VendorService;
}());
VendorService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], VendorService);
exports.VendorService = VendorService;
//# sourceMappingURL=vendor.service.js.map