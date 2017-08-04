"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var not_found_component_1 = require("./not-found.component");
var not_found_routing_module_1 = require("./not-found-routing.module");
var NotFoundModule = (function () {
    function NotFoundModule() {
    }
    return NotFoundModule;
}());
NotFoundModule = __decorate([
    core_1.NgModule({
        imports: [
            not_found_routing_module_1.NotFoundRoutingModule,
            router_1.RouterModule
        ],
        declarations: [not_found_component_1.NotFoundComponent]
    })
], NotFoundModule);
exports.NotFoundModule = NotFoundModule;
//# sourceMappingURL=not-found.module.js.map