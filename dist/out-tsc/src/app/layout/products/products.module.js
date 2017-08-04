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
var products_routing_module_1 = require("./products-routing.module");
var products_component_1 = require("./products.component");
var shared_1 = require("./../../shared");
var forms_1 = require("@angular/forms");
var filter_pipe_1 = require("./filter.pipe");
var image_upload_module_1 = require("../../../../node_modules/angular2-image-upload/src/image-upload.module");
var product_service_1 = require("./product.service");
// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';
var ProductsModule = (function () {
    function ProductsModule() {
    }
    return ProductsModule;
}());
ProductsModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            products_routing_module_1.ProductsRoutingModule,
            shared_1.PageHeaderModule,
            forms_1.FormsModule,
            image_upload_module_1.ImageUploadModule.forRoot()
            // ProductTagsModule
        ],
        providers: [product_service_1.ProductService],
        declarations: [products_component_1.ProductsComponent, filter_pipe_1.FilterPipe]
    })
], ProductsModule);
exports.ProductsModule = ProductsModule;
//# sourceMappingURL=products.module.js.map