import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SpecificationAttributesComponent } from './specification-attributes.component';
// import { SpecificationAttributesComponent } from './Specification-tags/Specification-tags.component';

const routes: Routes = [
    { path: '', component: SpecificationAttributesComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SpecificationAttributesRoutingModule { }
