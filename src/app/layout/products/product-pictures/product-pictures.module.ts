import { NgModule , CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from '@angular/forms';


import { ImageUploadModule } from '../../../../../node_modules/angular2-image-upload/src/image-upload.module';

import { NgxPaginationModule } from 'ngx-pagination';

import {ProductPicturesService} from './product-pictures.service';
import { URLService } from '../../../shared/services';



@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ImageUploadModule.forRoot(),
    NgxPaginationModule,


    // ProductTagsModule
  ],
  providers:[ProductPicturesService,URLService],
  declarations: [],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class ProductPicturesModule { }
