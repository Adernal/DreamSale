import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProductReviewsComponent } from './product-reviews.component';
// import { ProductReviewsComponent } from './product-tags/product-tags.component';

const routes: Routes = [
    { path: '', component: ProductReviewsComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductReviewsRoutingModule { }
