import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from '@angular/forms';

import { ImageUploadModule } from '../../../../../node_modules/angular2-image-upload/src/image-upload.module';

import { NgxPaginationModule } from 'ngx-pagination';
import {ProductPicturesComponent} from './product-pictures.component';
import {ProductPicturesService} from './product-pictures.service';



@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ImageUploadModule.forRoot(),
    NgxPaginationModule,

    // ProductTagsModule
  ],
  providers:[ProductPicturesService],
  declarations: [ProductPicturesComponent]
})
export class ProductsModule { }
