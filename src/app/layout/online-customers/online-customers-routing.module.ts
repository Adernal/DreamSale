import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { OnlineCustomersComponent } from './online-customers.component';


const routes: Routes = [
    { path: '', component: OnlineCustomersComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class OnlineCustomersRoutingModule { }
