import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VendorsComponent } from './vendors.component';
// import { ProductTagsComponent } from './product-tags/product-tags.component';

const routes: Routes = [
    { path: '', component: VendorsComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class VendorsRoutingModule { }
