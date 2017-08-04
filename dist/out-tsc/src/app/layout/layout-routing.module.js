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
var layout_component_1 = require("./layout.component");
var routes = [
    {
        path: '', component: layout_component_1.LayoutComponent,
        children: [
            { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
            { path: 'charts', loadChildren: './charts/charts.module#ChartsModule' },
            { path: 'tables', loadChildren: './tables/tables.module#TablesModule' },
            { path: 'forms', loadChildren: './form/form.module#FormModule' },
            { path: 'bs-element', loadChildren: './bs-element/bs-element.module#BsElementModule' },
            { path: 'grid', loadChildren: './grid/grid.module#GridModule' },
            { path: 'components', loadChildren: './bs-component/bs-component.module#BsComponentModule' },
            { path: 'blank-page', loadChildren: './blank-page/blank-page.module#BlankPageModule' },
            { path: 'category', loadChildren: './category/category.module#CategoryModule' },
            { path: 'manufacturers', loadChildren: './manufacturers/manufacturers.module#ManufacturersModule' },
            { path: 'products', loadChildren: './products/products.module#ProductsModule' },
            { path: 'product-tags', loadChildren: './products/product-tags/product-tags.module#ProductTagsModule' },
            { path: 'product-attributes', loadChildren: './products/product-attributes/product-attributes.module#ProductAttributesModule' },
            { path: 'product-reviews', loadChildren: './products/product-reviews/product-reviews.module#ProductReviewsModule' },
            { path: 'vendors', loadChildren: './vendors/vendors.module#VendorsModule' },
            { path: 'customers', loadChildren: './customers/customers.module#CustomersModule' },
            { path: 'specification-attributes', loadChildren: './products/specification-attributes/specification-attributes.module#SpecificationAttributesModule' }
        ]
    }
];
var LayoutRoutingModule = (function () {
    function LayoutRoutingModule() {
    }
    return LayoutRoutingModule;
}());
LayoutRoutingModule = __decorate([
    core_1.NgModule({
        imports: [router_1.RouterModule.forChild(routes)],
        exports: [router_1.RouterModule]
    })
], LayoutRoutingModule);
exports.LayoutRoutingModule = LayoutRoutingModule;
//# sourceMappingURL=layout-routing.module.js.map