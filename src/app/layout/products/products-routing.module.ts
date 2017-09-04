import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProductsComponent } from './products.component';
// import { ProductTagsComponent } from './product-tags/product-tags.component';

const routes: Routes = [
    { path: '', component: ProductsComponent },
    { path: 'showList', component: ProductsComponent}

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductsRoutingModule { }