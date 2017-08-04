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
var SpecificationAttributesService = (function () {
    function SpecificationAttributesService(http) {
        this.http = http;
    }
    SpecificationAttributesService.prototype.storeAttributes = function (attributes) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        this.temp = attributes[attributes.length - 1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/Add?continueEditing=true', this.temp, { headers: headers });
    };
    SpecificationAttributesService.prototype.getAttributes = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/0/2147483647');
    };
    SpecificationAttributesService.prototype.updateAttributes = function (attributes, id) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log(attributes);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/Update?continueEditing=true', attributes[id], { headers: headers });
    };
    SpecificationAttributesService.prototype.deleteAttributes = function (attributes, id) {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(attributes);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/Delete/' + id, null, { headers: headers });
    };
    return SpecificationAttributesService;
}());
SpecificationAttributesService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], SpecificationAttributesService);
exports.SpecificationAttributesService = SpecificationAttributesService;
//# sourceMappingURL=specification-attributes.service.js.map