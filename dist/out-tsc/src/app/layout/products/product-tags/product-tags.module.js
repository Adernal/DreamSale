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
var product_tags_routing_module_1 = require("./product-tags-routing.module");
var product_tags_component_1 = require("./product-tags.component");
var shared_1 = require("../../../shared");
var forms_1 = require("@angular/forms");
var filter_pipe_1 = require("./filter.pipe");
var ProductTagsModule = (function () {
    function ProductTagsModule() {
    }
    return ProductTagsModule;
}());
ProductTagsModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            product_tags_routing_module_1.ProductTagsRoutingModule,
            shared_1.PageHeaderModule,
            forms_1.FormsModule
        ],
        declarations: [product_tags_component_1.ProductTagsComponent, filter_pipe_1.FilterPipe]
    })
], ProductTagsModule);
exports.ProductTagsModule = ProductTagsModule;
//# sourceMappingURL=product-tags.module.js.map