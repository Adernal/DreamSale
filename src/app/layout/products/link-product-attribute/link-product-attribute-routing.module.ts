import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LinkProductAttributeComponent } from './link-product-attribute.component';
// import { ProductAttributesComponent } from './product-tags/product-tags.component';

const routes: Routes = [
    { path: '', component: LinkProductAttributeComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LinkProductAttributeRoutingModule { }
