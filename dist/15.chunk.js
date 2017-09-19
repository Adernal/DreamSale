webpackJsonp([15],{

/***/ "../../../../../src/app/layout/customer-roles/customer-roles-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__customer_roles_component__ = __webpack_require__("../../../../../src/app/layout/customer-roles/customer-roles.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CustomerRolesRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var routes = [
    { path: '', component: __WEBPACK_IMPORTED_MODULE_2__customer_roles_component__["a" /* CustomerRolesComponent */] },
];
var CustomerRolesRoutingModule = (function () {
    function CustomerRolesRoutingModule() {
    }
    return CustomerRolesRoutingModule;
}());
CustomerRolesRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */].forChild(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */]]
    })
], CustomerRolesRoutingModule);

//# sourceMappingURL=customer-roles-routing.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/customer-roles/customer-roles.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\n<h1> Customer Role Management </h1>\n     <hr>\n    <div class=\"row\">\n\n\n      <form role=\"form\" (ngSubmit)=\"addCustomerRole(f)\" #f=\"ngForm\" class=\"container\">\n        <div class=\"row\">\n          <div class=\"col-lg-6\">\n            <fieldset class=\"form-group card mb-3\">\n                <label class=\"card-header\">Customer Role</label>\n                <input class=\"form-control\" ngModel name=\"Name\" required placeholder=\"Enter Customer Role Name\" *ngIf=\"!editMode\">\n                <input class=\"form-control\" [(ngModel)]=\"Name\" name=\"Name\" required value=\"{{Name}}\" *ngIf=\"editMode\">\n                <!-- <p class=\"help-block\">Example block-level help text here.</p> -->\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Free Shipping</label>\n              <div *ngIf=\"!editMode\" class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                <input type=\"checkbox\" class=\"form-control\" ngModel name=\"FreeShipping\">\n              </div>\n\n              <div *ngIf=\"editMode\"  class=\"checkbox\" style=\"float:left; margin:5px;\">\n                <input type=\"checkbox\" class=\"form-control\" [(ngModel)]=\"FreeShipping\" name=\"Active\">\n              </div>\n\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Tax Exempt</label>\n              <div *ngIf=\"!editMode\" class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                <input type=\"checkbox\" class=\"form-control\" ngModel name=\"TaxExempt\">\n              </div>\n\n              <div *ngIf=\"editMode\"  class=\"checkbox\" style=\"float:left; margin:5px;\">\n                <input type=\"checkbox\" class=\"form-control\" [(ngModel)]=\"TaxExempt\" name=\"Active\">\n              </div>\n\n            </fieldset>\n\n\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"!editMode\">Add Customer Role</button>\n            <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!f.valid\" *ngIf=\"editMode\">Edit Customer Role</button>\n            \n\n        </div>\n        <div class=\"col-lg-6\">\n\n            <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Active</label>\n              <div *ngIf=\"!editMode\" class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                <input type=\"checkbox\" class=\"form-control\" ngModel name=\"Active\">\n              </div>\n\n              <div *ngIf=\"editMode\"  class=\"checkbox\" style=\"float:left; margin:5px;\">\n                <input type=\"checkbox\" class=\"form-control\" [(ngModel)]=\"Active\" name=\"Active\">\n              </div>\n\n            </fieldset>\n            <fieldset class=\"form-group card mb-3\">\n            <label class=\"card-header\">Is System Role</label>\n              <div *ngIf=\"!editMode\" class=\"checkbox\" style=\"float:left; margin:5px;\">\n\n                <input type=\"checkbox\" class=\"form-control\" ngModel name=\"IsSystemRole\">\n              </div>\n\n              <div *ngIf=\"editMode\"  class=\"checkbox\" style=\"float:left; margin:5px;\">\n                <input type=\"checkbox\" class=\"form-control\" [(ngModel)]=\"IsSystemRole\" name=\"Active\">\n              </div>\n\n            </fieldset>\n\n\n\n        </div>\n\n\n  </div>\n\n\n</form>\n  </div>\n  </div>\n <hr>\n    <div class=\"row\">\n\n\n            <div class=\"card mb-3\">\n                <div class=\"card-header\">\n                  <h2>Customer Role List</h2>\n                  <!-- <input type=\"text\" placeholder=\"Search By Name or Email\" [(ngModel)]=\"filteredCustomer Role\" style=\"width:100%;\"> -->\n                </div>\n                <div class=\"card-block\" style=\"width:100%;\">\n                    <table class=\"table table-bordered\" style=\"table-layout:fixed\">\n                        <thead class=\"thead-inverse\">\n                        <tr>\n\n                            <th>Customer Role</th>\n                            <th>Free Shipping</th>\n                            <th>Tax Exempt</th>\n                            <th>Active</th>\n                            <th>Is System Role</th>\n                            <th>Action</th>\n\n\n                        </tr>\n                        </thead>\n                        <tbody>\n                          <tr *ngFor =\"let customer_role of customer_role \" >\n\n\n                          <td>{{customer_role.Name}}</td>\n                          <td>{{customer_role.FreeShipping}}</td>\n                          <td>{{customer_role.TaxExempt}}</td>\n                          <td>{{customer_role.Active}}</td>\n                          <td>{{customer_role.IsSystemRole}}</td>\n\n\n\n\n                          <td><button type=\"button\" name=\"{{customer_role.Id}}\" class=\"btn btn-primary\" (click)=\"editCustomerRoleMode(c)\" #c><i class=\"fa fa-edit\"></i></button>\n                          <button type=\"button\" name=\"{{customer_role.Id}}\" class=\"btn btn-danger\" (click)=\"deleteCustomerRole(d)\" #d><i class=\"fa fa-times\"></i></button></td>\n                        </tr>\n\n                        </tbody>\n                    </table>\n                  \n                </div>\n            </div>\n\n\n\n    </div>\n"

/***/ }),

/***/ "../../../../../src/app/layout/customer-roles/customer-roles.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/layout/customer-roles/customer-roles.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__customer_roles_service__ = __webpack_require__("../../../../../src/app/layout/customer-roles/customer-roles.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CustomerRolesComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var CustomerRolesComponent = (function () {
    function CustomerRolesComponent(customer_roleService) {
        this.customer_roleService = customer_roleService;
        this.currentPageNumber = 1;
        this.editMode = false;
        this.customer_role = [];
        this.currentCustomerRole = [];
    }
    CustomerRolesComponent.prototype.ngOnInit = function () {
        //localStorage.removeItem("customer_roles");
        this.getCustomerRoles();
    };
    CustomerRolesComponent.prototype.addCustomerRole = function () {
        this.submitted = true;
        if (this.editMode) {
            this.editCustomerRole();
        }
        else {
            if (this.customer_role.length == 0) {
                this.Id = 1;
            }
            else {
                this.Id = +this.customer_role[this.customer_role.length - 1].Id + 1;
            }
            this.Name = this.customer_roleForm.value.Name;
            this.FreeShipping = this.customer_roleForm.value.FreeShipping;
            if (!this.FreeShipping) {
                this.FreeShipping = false;
            }
            this.TaxExempt = this.customer_roleForm.value.TaxExempt;
            if (!this.TaxExempt) {
                this.TaxExempt = false;
            }
            this.Active = this.customer_roleForm.value.Active;
            if (!this.Active) {
                this.Active = false;
            }
            this.IsSystemRole = this.customer_roleForm.value.IsSystemRole;
            if (!this.IsSystemRole) {
                this.IsSystemRole = false;
            }
            this.customer_role.push({
                "Id": this.Id,
                "Name": this.Name,
                "FreeShipping": this.FreeShipping,
                "TaxExempt": this.TaxExempt,
                "Active": this.Active,
                "IsSystemRole": this.IsSystemRole,
                "SystemName": "sample string 7",
                "EnablePasswordLifetime": true,
                "PurchasedWithProductId": 9,
                "PurchasedWithProductName": "sample string 10",
                "CustomProperties": {
                    "sample string 1": {},
                    "sample string 3": {}
                }
            });
            //  localStorage.setItem("customer_roles",JSON.stringify(this.customer_role));
            this.customer_roleService.storeCustomerRole(this.customer_role)
                .subscribe(function (data) {
                alert('Added !');
            }, function (error) {
                alert('Failed to add !');
                console.log(error);
            });
            console.log(this.customer_role);
            this.customer_roleForm.reset();
        }
    };
    CustomerRolesComponent.prototype.getCustomerRoles = function () {
        var _this = this;
        this.customer_roleService.getCustomerRole()
            .subscribe(function (response) {
            _this.customer_roles = (response.json());
            _this.customer_role = _this.customer_roles.Data;
        }, function (error) {
            console.log(error);
            alert('Can\'t fetch data ! Please refresh or check your connnection !');
        });
    };
    CustomerRolesComponent.prototype.editCustomerRoleMode = function (id) {
        this.editMode = true;
        this.Id = +id.name;
        this.currentCustomerRole = this.getCustomerRoleIndex(this.Id)[0];
        this.Name = this.currentCustomerRole["Name"];
        this.FreeShipping = this.currentCustomerRole["FreeShipping"];
        this.TaxExempt = this.currentCustomerRole["TaxExempt"];
        this.Active = this.currentCustomerRole["Active"];
        this.IsSystemRole = this.currentCustomerRole["IsSystemRole"];
    };
    CustomerRolesComponent.prototype.editCustomerRole = function () {
        var _this = this;
        this.editMode = false;
        this.Name = this.customer_roleForm.value.Name;
        this.FreeShipping = this.customer_roleForm.value.FreeShipping;
        if (!this.FreeShipping) {
            this.FreeShipping = false;
        }
        this.TaxExempt = this.customer_roleForm.value.TaxExempt;
        if (!this.TaxExempt) {
            this.TaxExempt = false;
        }
        this.Active = this.customer_roleForm.value.Active;
        if (!this.Active) {
            this.Active = false;
        }
        this.IsSystemRole = this.customer_roleForm.value.IsSystemRole;
        if (!this.IsSystemRole) {
            this.IsSystemRole = false;
        }
        this.currentCustomerRole["Name"] = this.Name;
        this.currentCustomerRole["FreeShipping"] = this.FreeShipping;
        this.currentCustomerRole["TaxExempt"] = this.TaxExempt;
        this.currentCustomerRole["Active"] = this.Active;
        this.currentCustomerRole["IsSystemRole"] = this.IsSystemRole;
        this.customer_roleService.updateCustomerRole(this.currentCustomerRole)
            .subscribe(function (data) {
            console.log(data.json());
            _this.getCustomerRoles();
            alert('Edited !');
        }, function (error) {
            console.log(error);
            alert('Can\'t fetch data ! Please refresh or check your connnection !');
        });
    };
    CustomerRolesComponent.prototype.deleteCustomerRole = function (id) {
        var _this = this;
        var confirmation = confirm('Are you sure you want to delete ?');
        if (confirmation) {
            this.customer_roleService.deleteCustomerRole(+id.name)
                .subscribe(function (data) {
                console.log(data);
                _this.getCustomerRoles();
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
    CustomerRolesComponent.prototype.getCustomerRoleIndex = function (id) {
        return this.customer_role.filter(function (customer_role) { return customer_role.Id == id; });
    };
    return CustomerRolesComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('f'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["b" /* NgForm */]) === "function" && _a || Object)
], CustomerRolesComponent.prototype, "customer_roleForm", void 0);
CustomerRolesComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app-customer-roles',
        template: __webpack_require__("../../../../../src/app/layout/customer-roles/customer-roles.component.html"),
        styles: [__webpack_require__("../../../../../src/app/layout/customer-roles/customer-roles.component.scss")]
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__customer_roles_service__["a" /* CustomerRoleService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__customer_roles_service__["a" /* CustomerRoleService */]) === "function" && _b || Object])
], CustomerRolesComponent);

var _a, _b;
//# sourceMappingURL=customer-roles.component.js.map

/***/ }),

/***/ "../../../../../src/app/layout/customer-roles/customer-roles.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__customer_roles_routing_module__ = __webpack_require__("../../../../../src/app/layout/customer-roles/customer-roles-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__customer_roles_component__ = __webpack_require__("../../../../../src/app/layout/customer-roles/customer-roles.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared__ = __webpack_require__("../../../../../src/app/shared/index.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__customer_roles_service__ = __webpack_require__("../../../../../src/app/layout/customer-roles/customer-roles.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_ngx_pagination__ = __webpack_require__("../../../../ngx-pagination/dist/ngx-pagination.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CustomerRolesModule", function() { return CustomerRolesModule; });
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
var CustomerRolesModule = (function () {
    function CustomerRolesModule() {
    }
    return CustomerRolesModule;
}());
CustomerRolesModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */],
            __WEBPACK_IMPORTED_MODULE_2__customer_roles_routing_module__["a" /* CustomerRolesRoutingModule */],
            __WEBPACK_IMPORTED_MODULE_4__shared__["b" /* PageHeaderModule */],
            __WEBPACK_IMPORTED_MODULE_5__angular_forms__["a" /* FormsModule */],
            __WEBPACK_IMPORTED_MODULE_7_ngx_pagination__["a" /* NgxPaginationModule */]
            // ImageUploadModule.forRoot()
            // ProductTagsModule
        ],
        providers: [__WEBPACK_IMPORTED_MODULE_6__customer_roles_service__["a" /* CustomerRoleService */]],
        declarations: [__WEBPACK_IMPORTED_MODULE_3__customer_roles_component__["a" /* CustomerRolesComponent */]]
    })
], CustomerRolesModule);

//# sourceMappingURL=customer-roles.module.js.map

/***/ }),

/***/ "../../../../../src/app/layout/customer-roles/customer-roles.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CustomerRoleService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var CustomerRoleService = (function () {
    function CustomerRoleService(http) {
        this.http = http;
    }
    CustomerRoleService.prototype.storeCustomerRole = function (customer_role) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        this.temp = customer_role[customer_role.length - 1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/AddCustomerRole?continueEditing=true', this.temp, { headers: headers });
    };
    CustomerRoleService.prototype.getCustomerRole = function () {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/Roles/0/2147483647?pageIdex=0');
    };
    CustomerRoleService.prototype.updateCustomerRole = function (customer_role) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        console.log(customer_role);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/EditCustomerRole?continueEditing=true', customer_role, { headers: headers });
    };
    CustomerRoleService.prototype.deleteCustomerRole = function (id) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Headers */]({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/DeleteCustomerRole?id=' + id, null, { headers: headers });
    };
    return CustomerRoleService;
}());
CustomerRoleService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Http */]) === "function" && _a || Object])
], CustomerRoleService);

var _a;
//# sourceMappingURL=customer-roles.service.js.map

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
                imports: [__WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */]],
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
//# sourceMappingURL=15.chunk.js.map