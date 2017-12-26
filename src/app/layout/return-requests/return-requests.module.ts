import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReturnRequestsRoutingModule } from './return-requests-routing.module';
import { ReturnRequestsComponent } from './return-requests.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { ReturnRequestService } from './return-requests.service';
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
    ReturnRequestsRoutingModule,
    PageHeaderModule,
    FormsModule,
    NgxPaginationModule
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  providers:[ReturnRequestService],
  declarations: [ReturnRequestsComponent]
})
export class ReturnRequestsModule { }
