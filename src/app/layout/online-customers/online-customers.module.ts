import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OnlineCustomersRoutingModule } from './online-customers-routing.module';
import { OnlineCustomersComponent } from './online-customers.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { OnlineCustomersService } from './online-customers.service';
import { URLService } from '../../shared/services';
//import { FilterPipe } from './filter.pipe';
// import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
//import { customerservice } from './product.service';

// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';


@NgModule({
  imports: [
    CommonModule,
    OnlineCustomersRoutingModule,
    PageHeaderModule,
    FormsModule,
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  providers:[OnlineCustomersService,URLService],
  declarations: [OnlineCustomersComponent]
})
export class OnlineCustomersModule { }
