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
var blank_page_routing_module_1 = require("./blank-page-routing.module");
var blank_page_component_1 = require("./blank-page.component");
var BlankPageModule = (function () {
    function BlankPageModule() {
    }
    return BlankPageModule;
}());
BlankPageModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            blank_page_routing_module_1.BlankPageRoutingModule
        ],
        declarations: [blank_page_component_1.BlankPageComponent]
    })
], BlankPageModule);
exports.BlankPageModule = BlankPageModule;
//# sourceMappingURL=blank-page.module.js.map