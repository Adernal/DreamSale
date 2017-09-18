webpackJsonp([11],{

/***/ "../../../../../src/app/layout/stores/filter.pipe.ts":
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
    FilterPipe.prototype.transform = function (value, filteredStores, propName) {
        if (value.length === 0 || filteredStores === '') {
            return value;
        }
        var resultArray = [];
        for (var _i = 0, value_1 = value; _i < value_1.length; _i++) {
            var item = value_1[_i];
            if (item[propName].toLowerCase().indexOf(filteredStores.toLowerCase()) >= 0) {
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

/***/ "../../../../../src/app/layout/stores/stores-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__stores_component__ = __webpack_require__("../../../../../src/app/layout/stores/stores.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return StoresRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var routes = [
    { path: '', component: __WEBPACK_IMPORTED_MODULE_2__stores_component__["a" /* StoresComponent */] },
];
var StoresRoutingModule = (function () {
    function StoresRoutingModule() {
    }
    return StoresRoutingModule;
}());
StoresRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */].forChild(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* RouterModule */]]
    })
], StoresRoutingModule);

//# sourceMappingURL=stores-routing.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/stores/stores.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\n<h1>  Store Management </h1>\n     <hr>\n    <div class=\"row\">\n\n\n      <form role=\"form\" (ngSubmit)=\"addStores(f)\" #f=\"ngForm\" class=\"container\">\n        <div class=\"row\">\n          <div class=\"col-lg-6\">\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Store Name</label>\n                <input class=\"form-control\" ngModel name=\"Name\" required placeholder=\"Enter Store Name\" *ngIf=\"!editMode\">\n                <input class=\"form-control\" [(ngModel)]=\"Name\" name=\"Name\" required value=\"{{Name}}\" *ngIf=\"editMode\">\n                <!-- <p class=\"help-block\">Example block-level help text here.</p> -->\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Company Name</label>\n                <input class=\"form-control\" ngModel name=\"CompanyName\" required placeholder=\"Enter Store Name\" *ngIf=\"!editMode\">\n                <input class=\"form-control\" [(ngModel)]=\"CompanyName\" name=\"CompanyName\" required value=\"{{CompanyName}}\" *ngIf=\"editMode\">\n                <!-- <p class=\"help-block\">Example block-level help text here.</p> -->\n            </fieldset>\n\n\n            <fieldset class=\"form-group card mb-3\">\n              <label class=\"card-header\">Company Address</label>\n              <textarea class=\"form-control\" rows=\"3\"\n\n              ngModel\n               name=\"CompanyAddress\"\n\n              required\n\n              placeholder=\"Company Address\"\n                *ngIf=\"!editMode\"\n              ></textarea>\n              <textarea class=\"form-control\" rows=\"3\"\n\n              [(ngModel)]=\"CompanyAddress\"\n               name=\"CompanyAddress\"\n\n              required\n\n              value=\"{{CompanyAddress}}\" *ngIf=\"editMode\"></textarea>\n          </fieldset>\n\n\n\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"!editMode\">Add Store</button>\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"editMode\">Edit Store</button>\n            \n\n        </div>\n        <div class=\"col-lg-6\">\n          <fieldset class=\"form-group card mb-3\">\n              <label class=\"card-header\">Company Phone</label>\n              <input class=\"form-control\" ngModel name=\"CompanyPhoneNumber\" required placeholder=\"Company Phone Number\" *ngIf=\"!editMode\">\n              <input class=\"form-control\" [(ngModel)]=\"CompanyPhoneNumber\" name=\"CompanyPhoneNumber\" required value=\"{{CompanyPhoneNumber}}\" *ngIf=\"editMode\">\n              <!-- <p class=\"help-block\">Example block-level help text here.</p> -->\n          </fieldset>\n          <fieldset class=\"form-group card mb-3\">\n              <label class=\"card-header\">Company Vat</label>\n              <input class=\"form-control\" ngModel name=\"CompanyVat\" required placeholder=\"Enter Vat\" *ngIf=\"!editMode\">\n              <input class=\"form-control\" [(ngModel)]=\"CompanyVat\" name=\"CompanyVat\" required value=\"{{CompanyVat}}\" *ngIf=\"editMode\">\n              <!-- <p class=\"help-block\">Example block-level help text here.</p> -->\n          </fieldset>\n          <fieldset class=\"form-group card mb-3\">\n              <label class=\"card-header\">Url</label>\n              <input class=\"form-control\" ngModel name=\"Url\" required placeholder=\"Url\" *ngIf=\"!editMode\">\n              <input class=\"form-control\" [(ngModel)]=\"Url\" name=\"Url\" required value=\"{{Url}}\" *ngIf=\"editMode\">\n              <!-- <p class=\"help-block\">Example block-level help text here.</p> -->\n          </fieldset>\n\n          <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Display Order</label>\n            <input type=\"number\" class=\"form-control\" ngModel name=\"DisplayOrder\" required placeholder=\"Enter Display Order\" min=\"1\" step=\"1\"\n            *ngIf=\"!editMode\">\n            <input type=\"number\" class=\"form-control\" [(ngModel)]=\"DisplayOrder\" name=\"DisplayOrder\" required placeholder=\"Enter Display Order\" min=\"1\" step=\"1\"\n            *ngIf=\"editMode\">\n        </fieldset>\n\n        </div>\n\n\n  </div>\n</form>\n  </div>\n</div>\n <hr>\n    <div class=\"row\">\n\n\n            <div class=\"card mb-3\">\n                <div class=\"card-header\">\n                    <h2>Store List</h2>\n                    <input type=\"text\" placeholder=\"Search Store\" [(ngModel)]=\"filteredStores\" style=\"width:100%\">\n                </div>\n                <div class=\"card-block table-responsive\" style=\"width:100%;\">\n                    <table class=\"table table-bordered\" style=\"table-layout:fixed\">\n                        <thead class=\"thead-inverse\">\n                        <tr>\n                          \n                            <th>Store Name</th>\n                            <th>Company Name</th>\n                            <th>Company Phone Number</th>\n                            <th>Url</th>\n                            <th>Display Order</th>\n                            <th>Action</th>\n\n\n\n                        </tr>\n                        </thead>\n                        <tbody>\n                          <tr *ngFor =\"let store of store | filter : filteredStores : 'Name'\">\n\n\n                          <td>{{store.Name}}</td>\n                          <td>{{store.CompanyName}}</td>\n                          <td>{{store.CompanyPhoneNumber}}</td>\n                          <td>{{store.Url}}</td>\n                          <td>{{store.DisplayOrder}}</td>\n                          <td><button type=\"button\" name=\"{{store.Id}}\" class=\"btn btn-primary\" (click)=\"editStoresMode(c)\" #c><i class=\"fa fa-edit\"></i></button>\n                          <button type=\"button\" name=\"{{store.Id}}\" class=\"btn btn-danger\" (click)=\"deleteStores(d)\" #d><i class=\"fa fa-times\"></i></button></td>\n\n                        </tr>\n\n                        </tbody>\n                    </table>\n                </div>\n            </div>\n\n\n\n    </div>\n"

/***/ }),

/***/ "../../../../../src/app/layout/stores/stores.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/layout/stores/stores.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__stores_service__ = __webpack_require__("../../../../../src/app/layout/stores/stores.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return StoresComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var StoresComponent = (function () {
    function StoresComponent(storeService) {
        this.storeService = storeService;
        this.submitted = false;
        this.store = [];
        this.Name = '';
        this.CompanyAddress = '';
        this.CompanyName = '';
        this.CompanyVat = '';
        this.CompanyPhoneNumber = '';
        this.Url = '';
        this.editMode = false;
        this.filteredStores = '';
        this.currentStore = [];
    }
    StoresComponent.prototype.ngOnInit = function () {
        localStorage.removeItem("stores");
        this.getStores();
    };
    StoresComponent.prototype.addStores = function () {
        this.submitted = true;
        if (this.editMode) {
            this.editStores();
        }
        else {
            if (this.store.length == 0) {
                this.Id = 1;
                console.log(this.Id);
            }
            else {
                this.Id = +this.store[this.store.length - 1].Id + 1;
            }
            this.Name = this.storeForm.value.Name;
            this.CompanyName = this.storeForm.value.CompanyName;
            this.CompanyAddress = this.storeForm.value.CompanyAddress;
            this.CompanyVat = this.storeForm.value.CompanyVat;
            this.CompanyPhoneNumber = this.storeForm.value.CompanyPhoneNumber;
            this.Url = this.storeForm.value.Url;
            this.DisplayOrder = this.storeForm.value.DisplayOrder;
            this.store.push({
                "Id": 0,
                "CustomProperties": {},
                "Name": this.Name,
                "Url": this.Url,
                "SslEnabled": false,
                "SecureUrl": null,
                "Hosts": "www.google.com",
                "DefaultLanguageId": 0,
                "AvailableLanguages": [
                    {
                        "Disabled": false,
                        "Group": null,
                        "Selected": false,
                        "Text": "---",
                        "Value": "0"
                    },
                    {
                        "Disabled": false,
                        "Group": null,
                        "Selected": false,
                        "Text": "English",
                        "Value": "1"
                    }
                ],
                "DisplayOrder": this.DisplayOrder,
                "CompanyName": this.CompanyName,
                "CompanyAddress": this.CompanyAddress,
                "CompanyPhoneNumber": this.CompanyAddress,
                "CompanyVat": this.CompanyVat,
                "Locales": []
            });
            //localStorage.setItem("stores",JSON.stringify(this.store));
            this.storeService.storeStores(this.store)
                .subscribe(function (data) {
                console.log(data);
                alert('Added !');
            }, function (error) {
                alert('Failed to add !');
                console.log(error);
            });
            //this.stores.store=store;
            console.log(this.store);
            this.storeForm.reset();
        }
    };
    StoresComponent.prototype.editStoresMode = function (id) {
        this.editMode = true;
        this.Id = +id.name;
        this.currentStore = this.getStoreIndex(this.Id)[0];
        //console.log(this.Id);
        // console.log(this.store[1].Name);
        this.Name = this.currentStore["Name"];
        this.CompanyName = this.currentStore["CompanyName"];
        this.CompanyAddress = this.currentStore["CompanyAddress"];
        this.CompanyPhoneNumber = this.currentStore["CompanyPhoneNumber"];
        this.CompanyVat = this.currentStore["CompanyVat"];
        this.Url = this.currentStore["Url"];
        this.DisplayOrder = this.currentStore["DisplayOrder"];
    };
    StoresComponent.prototype.editStores = function () {
        var _this = this;
        this.editMode = false;
        this.Name = this.storeForm.value.Name;
        this.CompanyName = this.storeForm.value.CompanyName;
        this.CompanyAddress = this.storeForm.value.CompanyAddress;
        this.CompanyPhoneNumber = this.storeForm.value.CompanyPhoneNumber;
        this.CompanyVat = this.storeForm.value.CompanyVat;
        this.Url = this.storeForm.value.Url;
        this.DisplayOrder = this.storeForm.value.DisplayOrder;
        this.currentStore["Name"] = this.Name;
        this.currentStore["CompanyName"] = this.CompanyName;
        this.currentStore["CompanyAddress"] = this.CompanyAddress;
        this.currentStore["CompanyPhoneNumber"] = this.CompanyPhoneNumber;
        this.currentStore["CompanyVat"] = this.CompanyVat;
        this.currentStore["Url"] = this.Url;
        this.currentStore["DisplayOrder"] = this.DisplayOrder;
        // localStorage.setItem("stores",JSON.stringify(this.store));
        this.storeService.updateStores(this.store)
            .subscribe(function (data) {
            console.log(data.json());
            _this.getStores();
            alert('Edited !');
        }, function (error) {
            console.log(error);
            alert('Can\'t fetch data ! Please refresh or check your connnection !');
        });
    };
    StoresComponent.prototype.getStores = function () {
        var _this = this;
        //  this.store = JSON.parse(localStorage.getItem("stores"));
        this.storeService.getStores()
            .subscribe(function (response) {
            _this.stores = (response.json());
            _this.store = _this.stores.Data;
            console.log((_this.store));
        }, function (error) {
            console.log(error);
            alert('Can\'t fetch data ! Please refresh or check your connnection !');
        });
    };
    StoresComponent.prototype.deleteStores = function (id) {
        var _this = this;
        var confirmation = confirm('Are you sure you want to delete ?');
        if (confirmation) {
            this.storeService.deleteStores(+id.name)
                .subscribe(function (data) {
                console.log(data);
                _this.getStores();
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
    StoresComponent.prototype.getStoreIndex = function (id) {
        return this.store.filter(function (store) { return store.Id == id; });
    };
    return StoresComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('f'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _a || Object)
], StoresComponent.prototype, "storeForm", void 0);
StoresComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app-stores',
        template: __webpack_require__("../../../../../src/app/layout/stores/stores.component.html"),
        styles: [__webpack_require__("../../../../../src/app/layout/stores/stores.component.scss")]
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__stores_service__["a" /* StoresService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__stores_service__["a" /* StoresService */]) === "function" && _b || Object])
], StoresComponent);

var _a, _b;
//# sourceMappingURL=stores.component.js.map

/***/ }),

/***/ "../../../../../src/app/layout/stores/stores.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__stores_routing_module__ = __webpack_require__("../../../../../src/app/layout/stores/stores-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__stores_component__ = __webpack_require__("../../../../../src/app/layout/stores/stores.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared__ = __webpack_require__("../../../../../src/app/shared/index.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__filter_pipe__ = __webpack_require__("../../../../../src/app/layout/stores/filter.pipe.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__stores_service__ = __webpack_require__("../../../../../src/app/layout/stores/stores.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StoresModule", function() { return StoresModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};








var StoresModule = (function () {
    function StoresModule() {
    }
    return StoresModule;
}());
StoresModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["h" /* CommonModule */],
            __WEBPACK_IMPORTED_MODULE_2__stores_routing_module__["a" /* StoresRoutingModule */],
            __WEBPACK_IMPORTED_MODULE_4__shared__["b" /* PageHeaderModule */],
            __WEBPACK_IMPORTED_MODULE_5__angular_forms__["a" /* FormsModule */],
        ],
        providers: [__WEBPACK_IMPORTED_MODULE_7__stores_service__["a" /* StoresService */]],
        declarations: [__WEBPACK_IMPORTED_MODULE_3__stores_component__["a" /* StoresComponent */], __WEBPACK_IMPORTED_MODULE_6__filter_pipe__["a" /* FilterPipe */]]
    })
], StoresModule);

//# sourceMappingURL=stores.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/stores/stores.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return StoresService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var StoresService = (function () {
    function StoresService(http) {
        this.http = http;
    }
    StoresService.prototype.storeStores = function (store) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        this.temp = store[store.length - 1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Stores/CreateStore?continueEditing=true', this.temp, { headers: headers });
    };
    StoresService.prototype.getStores = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Stores');
    };
    StoresService.prototype.updateStores = function (store) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log(store);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Stores/EditStore?continueEditing=true', store, { headers: headers });
    };
    StoresService.prototype.deleteStores = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        //console.log(store);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Stores/DeleteStore?id=' + id, null, { headers: headers });
    };
    return StoresService;
}());
StoresService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], StoresService);

var _a;
//# sourceMappingURL=stores.service.js.map

/***/ })

});
//# sourceMappingURL=11.chunk.js.map