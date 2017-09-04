import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrdersRoutingModule } from './orders-routing.module';
import { OrdersComponent } from './orders.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';
import { NgxPaginationModule } from 'ngx-pagination';
//import { FilterPipe } from './filter.pipe';
// import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
//import { customerservice } from './product.service';

// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';


@NgModule({
  imports: [
    CommonModule,
    OrdersRoutingModule,
    PageHeaderModule,
    FormsModule,
    NgxPaginationModule
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  //providers:[customerservice],
  declarations: [OrdersComponent, FilterPipe]
})
export class OrdersModule { }
