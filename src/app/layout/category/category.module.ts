import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './category.component';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';
import { CategoryService } from './category.service';

@NgModule({
  imports: [
    CommonModule,
    CategoryRoutingModule,
    FormsModule
  ],
  providers: [CategoryService],
  declarations: [CategoryComponent, FilterPipe]
})
export class CategoryModule { }
