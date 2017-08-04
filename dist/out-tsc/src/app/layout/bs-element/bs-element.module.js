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
var bs_element_routing_module_1 = require("./bs-element-routing.module");
var bs_element_component_1 = require("./bs-element.component");
var shared_1 = require("./../../shared");
var BsElementModule = (function () {
    function BsElementModule() {
    }
    return BsElementModule;
}());
BsElementModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            bs_element_routing_module_1.BsElementRoutingModule,
            shared_1.PageHeaderModule
        ],
        declarations: [bs_element_component_1.BsElementComponent]
    })
], BsElementModule);
exports.BsElementModule = BsElementModule;
//# sourceMappingURL=bs-element.module.js.map