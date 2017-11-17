import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProductAttributesComponent } from './product-attributes.component';
// import { ProductAttributesComponent } from './product-tags/product-tags.component';

const routes: Routes = [
    { path: '', component: ProductAttributesComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductAttributesRoutingModule { }
