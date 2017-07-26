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
            { path: 'components', loadChildren: './bs-component/bs-component.module#BsComponentModule' },
            { path: 'blank-page', loadChildren: './blank-page/blank-page.module#BlankPageModule' },
            { path: 'category', loadChildren: './category/category.module#CategoryModule' },
            { path: 'manufacturers', loadChildren: './manufacturers/manufacturers.module#ManufacturersModule' },
            { path: 'products', loadChildren: './products/products.module#ProductsModule' },
            { path: 'product-tags', loadChildren: './products/product-tags/product-tags.module#ProductTagsModule' },
            { path: 'product-attributes', loadChildren: './products/product-attributes/product-attributes.module#ProductAttributesModule' },
            { path: 'specification-attributes', loadChildren: './products/specification-attributes/specification-attributes.module#SpecificationAttributesModule' }


        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule { }
