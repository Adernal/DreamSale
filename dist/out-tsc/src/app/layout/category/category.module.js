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
var category_routing_module_1 = require("./category-routing.module");
var category_component_1 = require("./category.component");
var forms_1 = require("@angular/forms");
var filter_pipe_1 = require("./filter.pipe");
var category_service_1 = require("./category.service");
var CategoryModule = (function () {
    function CategoryModule() {
    }
    return CategoryModule;
}());
CategoryModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            category_routing_module_1.CategoryRoutingModule,
            forms_1.FormsModule
        ],
        providers: [category_service_1.CategoryService],
        declarations: [category_component_1.CategoryComponent, filter_pipe_1.FilterPipe]
    })
], CategoryModule);
exports.CategoryModule = CategoryModule;
//# sourceMappingURL=category.module.js.map