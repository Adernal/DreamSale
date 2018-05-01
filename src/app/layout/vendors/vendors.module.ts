import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VendorsRoutingModule } from './vendors-routing.module';
import { VendorsComponent } from './vendors.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';

import { VendorService } from './vendor.service';
import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
import { URLService } from '../../shared/services';
//import { FilterPipe } from './filter.pipe';
// import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
//import { Vendorservice } from './product.service';

// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';


@NgModule({
  imports: [
    CommonModule,
    VendorsRoutingModule,
    PageHeaderModule,
    FormsModule,
      ImageUploadModule.forRoot()
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  providers:[VendorService,URLService],
  declarations: [VendorsComponent, FilterPipe ]
})
export class VendorsModule { }
