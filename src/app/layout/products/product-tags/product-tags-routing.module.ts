import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProductTagsComponent } from './product-tags.component';
// import { ProductTagsComponent } from './product-tags/product-tags.component';

const routes: Routes = [
    { path: '', component: ProductTagsComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductTagsRoutingModule { }
