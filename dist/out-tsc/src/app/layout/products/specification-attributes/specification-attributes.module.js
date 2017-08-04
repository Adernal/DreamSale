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
var specification_attributes_routing_module_1 = require("./specification-attributes-routing.module");
var specification_attributes_component_1 = require("./specification-attributes.component");
var shared_1 = require("../../../shared");
var forms_1 = require("@angular/forms");
var filter_pipe_1 = require("./filter.pipe");
var specification_attributes_service_1 = require("./specification-attributes.service");
var SpecificationAttributesModule = (function () {
    function SpecificationAttributesModule() {
    }
    return SpecificationAttributesModule;
}());
SpecificationAttributesModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            specification_attributes_routing_module_1.SpecificationAttributesRoutingModule,
            shared_1.PageHeaderModule,
            forms_1.FormsModule
        ],
        providers: [specification_attributes_service_1.SpecificationAttributesService],
        declarations: [specification_attributes_component_1.SpecificationAttributesComponent, filter_pipe_1.FilterPipe]
    })
], SpecificationAttributesModule);
exports.SpecificationAttributesModule = SpecificationAttributesModule;
//# sourceMappingURL=specification-attributes.module.js.map