webpackJsonp([12],{

/***/ "../../../../../src/app/layout/products/specification-attributes/filter.pipe.ts":
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
    FilterPipe.prototype.transform = function (value, filteredSpecificationAttribute, propName) {
        if (value.length === 0 || filteredSpecificationAttribute === '') {
            return value;
        }
        var resultArray = [];
        for (var _i = 0, value_1 = value; _i < value_1.length; _i++) {
            var item = value_1[_i];
            if (item[propName].toLowerCase().indexOf(filteredSpecificationAttribute.toLowerCase()) >= 0) {
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
        pure: false,
    })
], FilterPipe);

//# sourceMappingURL=filter.pipe.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/specification-attributes/specification-attributes-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__specification_attributes_component__ = __webpack_require__("../../../../../src/app/layout/products/specification-attributes/specification-attributes.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SpecificationAttributesRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



// import { SpecificationAttributesComponent } from './Specification-tags/Specification-tags.component';
var routes = [
    { path: '', component: __WEBPACK_IMPORTED_MODULE_2__specification_attributes_component__["a" /* SpecificationAttributesComponent */] },
];
var SpecificationAttributesRoutingModule = (function () {
    function SpecificationAttributesRoutingModule() {
    }
    return SpecificationAttributesRoutingModule;
}());
SpecificationAttributesRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */].forChild(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */]]
    })
], SpecificationAttributesRoutingModule);

//# sourceMappingURL=specification-attributes-routing.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/specification-attributes/specification-attributes.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\n<h1>  Specification Attributes Management </h1>\n     <hr>\n    <div class=\"row\">\n\n\n      <form role=\"form\" (ngSubmit)=\"addAttribute(f)\" #f=\"ngForm\" class=\"container\">\n        <div class=\"row\">\n          <div class=\"col-lg-6\">\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Specification Attribute</label>\n                <input class=\"form-control\" ngModel name=\"Name\" required placeholder=\"Enter Attribute Name\" *ngIf=\"!editMode\">\n                <input class=\"form-control\" [(ngModel)]=\"Name\" name=\"Name\" required value=\"{{Name}}\" *ngIf=\"editMode\">\n                <!-- <p class=\"help-block\">Example block-level help text here.</p> -->\n            </fieldset>\n\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"!editMode\">Add Attribute</button>\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"editMode\">Edit Attribute</button>\n            \n\n        </div>\n\n\n  </div>\n</form>\n  </div>\n <hr>\n    <div class=\"row\">\n\n\n            <div class=\"card mb-3\">\n                <div class=\"card-header\">\n                    <h2>Attribute List</h2>\n                    <input type=\"text\" placeholder=\"Search Attribute\" [(ngModel)]=\"filteredSpecificationAttribute\" style=\"width:100%;\">\n                </div>\n                <div class=\"card-block table-responsive\" style=\"width:100%;\">\n                    <table class=\"table table-bordered\" style=\"table-layout:fixed\">\n                        <thead class=\"thead-inverse\">\n                        <tr>\n                            <th>Attribute Id</th>\n                            <th>Attribute Name</th>\n                            <th>Action</th>\n\n\n                        </tr>\n                        </thead>\n                        <tbody>\n                          <tr *ngFor =\" let i = index;let attribute of attribute | filter: filteredSpecificationAttribute : 'Name'\">\n\n                          <td>{{attribute.Id}}</td>\n                          <td>{{attribute.Name}}</td>\n\n                          <td><button type=\"button\" name=\"{{i}}\" class=\"btn btn-primary\" (click)=\"editAttributeMode(c)\" #c><i class=\"fa fa-edit\"></i></button>\n                          <button type=\"button\" name=\"{{i}}\" class=\"btn btn-danger\" (click)=\"deleteAttribute(d)\" #d><i class=\"fa fa-times\"></i></button></td>\n                        </tr>\n\n\n                        </tbody>\n                    </table>\n                </div>\n            </div>\n\n\n\n    </div>\n</div>\n"

/***/ }),

/***/ "../../../../../src/app/layout/products/specification-attributes/specification-attributes.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/layout/products/specification-attributes/specification-attributes.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__specification_attributes_service__ = __webpack_require__("../../../../../src/app/layout/products/specification-attributes/specification-attributes.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SpecificationAttributesComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var SpecificationAttributesComponent = (function () {
    function SpecificationAttributesComponent(specificationAttributeService) {
        this.specificationAttributeService = specificationAttributeService;
        this.submitted = false;
        this.attribute = [];
        this.Name = '';
        this.editMode = false;
        this.filteredSpecificationAttribute = '';
    }
    SpecificationAttributesComponent.prototype.ngOnInit = function () {
        //localStorage.removeItem("spec-attributes");
        this.getAttributes();
    };
    SpecificationAttributesComponent.prototype.addAttribute = function () {
        var _this = this;
        this.submitted = true;
        if (this.editMode) {
            this.editAttribute();
        }
        else {
            if (this.attribute.length == 0) {
                this.Id = 1;
            }
            else {
                this.Id = +this.attribute[this.attribute.length - 1].Id + 1;
            }
            this.Name = this.attributeForm.value.Name;
            this.attribute.push({
                "Id": this.Id,
                "CustomProperties": {
                    "sample string 1": {},
                    "sample string 3": {}
                },
                "Name": this.Name,
                "DisplayOrder": 3,
                "Locales": [
                    {
                        "LanguageId": 1,
                        "Name": "sample string 2"
                    },
                    {
                        "LanguageId": 1,
                        "Name": "sample string 2"
                    }
                ]
            });
            //localStorage.setItem("spec-attributes", JSON.stringify(this.attribute));
            //  this.attributes.attribute=attribute;
            this.specificationAttributeService.storeAttributes(this.attribute)
                .subscribe(function (data) {
                console.log(data);
                alert("Added !");
                _this.getAttributes();
            }, function (error) {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
            });
            this.attributeForm.reset();
        }
    };
    SpecificationAttributesComponent.prototype.editAttribute = function () {
        var _this = this;
        this.editMode = false;
        this.Name = this.attributeForm.value.Name;
        this.attribute[+this.Id].Name = this.Name;
        // localStorage.setItem("spec-attributes", JSON.stringify(this.attribute));
        this.specificationAttributeService.updateAttributes(this.attribute, this.Id)
            .subscribe(function (data) {
            console.log(data);
            _this.getAttributes();
            alert("Edited !");
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    SpecificationAttributesComponent.prototype.editAttributeMode = function (id) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        // console.log(this.attribute[1].Name);
        this.Name = this.attribute[+this.Id].Name;
    };
    SpecificationAttributesComponent.prototype.getAttributes = function () {
        var _this = this;
        this.specificationAttributeService.getAttributes()
            .subscribe(function (response) {
            _this.attributes = (response.json());
            _this.attribute = _this.attributes.Data;
            console.log((_this.attribute));
            //  this.attribute =[this.attributes];
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    SpecificationAttributesComponent.prototype.deleteAttribute = function (id) {
        var _this = this;
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.Id = +this.attribute[+id.name].Id;
            this.attribute.splice(+id.name, 1);
            //localStorage.setItem("attributes",JSON.stringify(this.attribute));
            this.specificationAttributeService.deleteAttributes(this.attribute, this.Id)
                .subscribe(function (data) {
                console.log(data);
                _this.getAttributes();
                alert("Deleted");
            }, function (error) {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
            });
        }
        if (this.editMode) {
            this.editMode = false;
        }
    };
    return SpecificationAttributesComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('f'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _a || Object)
], SpecificationAttributesComponent.prototype, "attributeForm", void 0);
SpecificationAttributesComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app-specification-attributes',
        template: __webpack_require__("../../../../../src/app/layout/products/specification-attributes/specification-attributes.component.html"),
        styles: [__webpack_require__("../../../../../src/app/layout/products/specification-attributes/specification-attributes.component.scss")]
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__specification_attributes_service__["a" /* SpecificationAttributesService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__specification_attributes_service__["a" /* SpecificationAttributesService */]) === "function" && _b || Object])
], SpecificationAttributesComponent);

var _a, _b;
//# sourceMappingURL=specification-attributes.component.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/specification-attributes/specification-attributes.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__specification_attributes_routing_module__ = __webpack_require__("../../../../../src/app/layout/products/specification-attributes/specification-attributes-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__specification_attributes_component__ = __webpack_require__("../../../../../src/app/layout/products/specification-attributes/specification-attributes.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared__ = __webpack_require__("../../../../../src/app/shared/index.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__filter_pipe__ = __webpack_require__("../../../../../src/app/layout/products/specification-attributes/filter.pipe.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__specification_attributes_service__ = __webpack_require__("../../../../../src/app/layout/products/specification-attributes/specification-attributes.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SpecificationAttributesModule", function() { return SpecificationAttributesModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};








var SpecificationAttributesModule = (function () {
    function SpecificationAttributesModule() {
    }
    return SpecificationAttributesModule;
}());
SpecificationAttributesModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["h" /* CommonModule */],
            __WEBPACK_IMPORTED_MODULE_2__specification_attributes_routing_module__["a" /* SpecificationAttributesRoutingModule */],
            __WEBPACK_IMPORTED_MODULE_4__shared__["b" /* PageHeaderModule */],
            __WEBPACK_IMPORTED_MODULE_5__angular_forms__["a" /* FormsModule */]
        ],
        providers: [__WEBPACK_IMPORTED_MODULE_7__specification_attributes_service__["a" /* SpecificationAttributesService */]],
        declarations: [__WEBPACK_IMPORTED_MODULE_3__specification_attributes_component__["a" /* SpecificationAttributesComponent */], __WEBPACK_IMPORTED_MODULE_6__filter_pipe__["a" /* FilterPipe */]]
    })
], SpecificationAttributesModule);

//# sourceMappingURL=specification-attributes.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/products/specification-attributes/specification-attributes.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SpecificationAttributesService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var SpecificationAttributesService = (function () {
    function SpecificationAttributesService(http) {
        this.http = http;
    }
    SpecificationAttributesService.prototype.storeAttributes = function (attributes) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        this.temp = attributes[attributes.length - 1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/Add?continueEditing=true', this.temp, { headers: headers });
    };
    SpecificationAttributesService.prototype.getAttributes = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/0/2147483647');
    };
    SpecificationAttributesService.prototype.updateAttributes = function (attributes, id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log(attributes);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/Update?continueEditing=true', attributes[id], { headers: headers });
    };
    SpecificationAttributesService.prototype.deleteAttributes = function (attributes, id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(attributes);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/Delete/' + id, null, { headers: headers });
    };
    return SpecificationAttributesService;
}());
SpecificationAttributesService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], SpecificationAttributesService);

var _a;
//# sourceMappingURL=specification-attributes.service.js.map

/***/ })

});
//# sourceMappingURL=12.chunk.js.map