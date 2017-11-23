import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BestSellerComponent } from './best-seller.component';

const routes: Routes = [
    { path: '', component: BestSellerComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BestSellerRoutingModule { }
