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
var ManufacturersService = (function () {
    function ManufacturersService(http) {
        this.http = http;
    }
    ManufacturersService.prototype.storeManufacturers = function (manufacturers) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log(manufacturers);
        this.temp = manufacturers[manufacturers.length - 1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers/Add?continueEditing=true', this.temp, { headers: headers });
    };
    ManufacturersService.prototype.getManufacturers = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers');
    };
    ManufacturersService.prototype.updateManufacturers = function (manufacturers, id) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log("Id :" + id);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers/Update?continueEditing=true', manufacturers[id], { headers: headers });
    };
    ManufacturersService.prototype.deleteAttributes = function (manufacturers, id) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log(manufacturers);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers/Delete?id=' + id, null, { headers: headers });
    };
    return ManufacturersService;
}());
ManufacturersService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], ManufacturersService);
exports.ManufacturersService = ManufacturersService;
//# sourceMappingURL=manufacturers.service.js.map