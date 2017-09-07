import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from '@angular/forms';

import { ImageUploadModule } from '../../../../../node_modules/angular2-image-upload/src/image-upload.module';

import { NgxPaginationModule } from 'ngx-pagination';
import {LinkProductSpecAttributesComponent} from './link-product-spec-attributes.component';
import {LinkProductSpecAttributesService} from './link-product-spec-attributes.service';
import { ProductAttributesService } from '../product-attributes/product-attributes.service';



@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ImageUploadModule.forRoot(),
    NgxPaginationModule,

    // ProductTagsModule
  ],
  providers:[LinkProductSpecAttributesService,ProductAttributesService],
  declarations: [LinkProductSpecAttributesComponent]
})
export class LinkProductSpecAttributesModule { }
