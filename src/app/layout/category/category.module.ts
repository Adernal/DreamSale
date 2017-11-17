import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './category.component';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';
import { CategoryService } from './category.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';


@NgModule({
  imports: [
    CommonModule,
    CategoryRoutingModule,
    FormsModule,
    NgxPaginationModule,
    ImageUploadModule.forRoot()
  ],
  providers: [CategoryService],
  declarations: [CategoryComponent, FilterPipe]
})
export class CategoryModule { }
