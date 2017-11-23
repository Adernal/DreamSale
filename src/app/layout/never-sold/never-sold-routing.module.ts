import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NeverSoldComponent } from './never-sold.component';

const routes: Routes = [
    { path: '', component: NeverSoldComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class NeverSoldRoutingModule { }
