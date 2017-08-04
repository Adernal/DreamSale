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
var customers_routing_module_1 = require("./customers-routing.module");
var customers_component_1 = require("./customers.component");
var shared_1 = require("./../../shared");
var forms_1 = require("@angular/forms");
//import { FilterPipe } from './filter.pipe';
// import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
//import { customerservice } from './product.service';
// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';
var CustomersModule = (function () {
    function CustomersModule() {
    }
    return CustomersModule;
}());
CustomersModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            customers_routing_module_1.CustomersRoutingModule,
            shared_1.PageHeaderModule,
            forms_1.FormsModule,
        ],
        //providers:[customerservice],
        declarations: [customers_component_1.CustomersComponent]
    })
], CustomersModule);
exports.CustomersModule = CustomersModule;
//# sourceMappingURL=customers.module.js.map