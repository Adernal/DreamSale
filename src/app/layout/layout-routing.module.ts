import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
            { path: 'charts', loadChildren: './charts/charts.module#ChartsModule' },
            { path: 'tables', loadChildren: './tables/tables.module#TablesModule' },
            { path: 'forms', loadChildren: './form/form.module#FormModule' },
            { path: 'bs-element', loadChildren: './bs-element/bs-element.module#BsElementModule' },
            { path: 'grid', loadChildren: './grid/grid.module#GridModule' },
            { path: 'blank-page', loadChildren: './blank-page/blank-page.module#BlankPageModule' },
            { path: 'category', loadChildren: './category/category.module#CategoryModule' },
            { path: 'manufacturers', loadChildren: './manufacturers/manufacturers.module#ManufacturersModule' },
            { path: 'products', loadChildren: './products/products.module#ProductsModule' },
            { path: 'product-tags', loadChildren: './products/product-tags/product-tags.module#ProductTagsModule' },
            { path: 'product-attributes', loadChildren: './products/product-attributes/product-attributes.module#ProductAttributesModule' },
            { path: 'customers', loadChildren: './customers/customers.module#CustomersModule' },
            { path: 'orders', loadChildren: './orders/orders.module#OrdersModule'},
            { path: 'product-reviews', loadChildren: './products/product-reviews/product-reviews.module#ProductReviewsModule' },
            { path: 'vendors', loadChildren: './vendors/vendors.module#VendorsModule' },
            { path: 'customers', loadChildren: './customers/customers.module#CustomersModule' },
            { path: 'online_customers', loadChildren: './online-customers/online-customers.module#OnlineCustomersModule' },
            { path: 'customer-roles', loadChildren: './customer-roles/customer-roles.module#CustomerRolesModule' },
            { path: 'return-requests', loadChildren: './return-requests/return-requests.module#ReturnRequestsModule' },
            { path: 'stores', loadChildren: './stores/stores.module#StoresModule' },
            { path: 'best-seller', loadChildren: './best-seller/best-seller.module#BestSellerModule' },
            { path: 'never-sold', loadChildren: './never-sold/never-sold.module#NeverSoldModule' },
            { path: 'specification-attributes', loadChildren: './products/specification-attributes/specification-attributes.module#SpecificationAttributesModule' }


        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule { }
