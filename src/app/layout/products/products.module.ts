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
import { ProductAttributesService} from './product-attributes/product-attributes.service';
import { ProductPicturesComponent } from './product-pictures/product-pictures.component';
import { ProductPicturesService } from './product-pictures/product-pictures.service';

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

    // ProductTagsModule
  ],
  providers:[ProductService,ProductAttributesService,ProductPicturesService],
  declarations: [ProductsComponent, FilterPipe, ProductPicturesComponent]
})
export class ProductsModule { }
