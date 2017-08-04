"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var core_2 = require("@ngx-translate/core");
var layout_routing_module_1 = require("./layout-routing.module");
var layout_component_1 = require("./layout.component");
var shared_1 = require("../shared");
// import { VendorsComponent } from './vendors/vendors.component';
var LayoutModule = (function () {
    function LayoutModule() {
    }
    return LayoutModule;
}());
LayoutModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            ng_bootstrap_1.NgbDropdownModule.forRoot(),
            layout_routing_module_1.LayoutRoutingModule,
            core_2.TranslateModule
        ],
        declarations: [
            layout_component_1.LayoutComponent,
            shared_1.HeaderComponent,
            shared_1.SidebarComponent,
        ]
    })
], LayoutModule);
exports.LayoutModule = LayoutModule;
//# sourceMappingURL=layout.module.js.map