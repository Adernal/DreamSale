import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductsComponent } from './products.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';
import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
import { ProductService } from './product.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { MdButtonModule } from '@angular/material';

// import { ProductAttributesComponent } from './product-attributes/product-attributes.component';
// import { SpecificationAttributesComponent } from './specification-attributes/specification-attributes.component';
// import { ProductTagsModule } from './product-tags/product-tags.module';


@NgModule({
  imports: [
    CommonModule,
    ProductsRoutingModule,
    PageHeaderModule,
    FormsModule,
    ImageUploadModule.forRoot(),
    NgxPaginationModule,
    MdButtonModule
    // ProductTagsModule
  ],
  providers:[ProductService],
  declarations: [ProductsComponent, FilterPipe]
})
export class ProductsModule { }
