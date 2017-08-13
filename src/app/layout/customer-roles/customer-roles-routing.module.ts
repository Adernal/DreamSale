import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomerRolesComponent } from './customer-roles.component';


const routes: Routes = [
    { path: '', component: CustomerRolesComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CustomerRolesRoutingModule { }
