import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRolesRoutingModule } from './customer-roles-routing.module';
import { CustomerRolesComponent } from './customer-roles.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { CustomerRoleService } from './customer-roles.service';
//import { FilterPipe } from './filter.pipe';
// import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
//import { customerservice } from './product.service';

// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';


@NgModule({
  imports: [
    CommonModule,
    CustomerRolesRoutingModule,
    PageHeaderModule,
    FormsModule,
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  providers:[CustomerRoleService],
  declarations: [CustomerRolesComponent]
})
export class CustomerRolesModule { }
