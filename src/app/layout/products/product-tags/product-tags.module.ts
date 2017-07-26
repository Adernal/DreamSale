import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductTagsRoutingModule } from './product-tags-routing.module';
import { ProductTagsComponent } from './product-tags.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';

@NgModule({
    imports: [
        CommonModule,
        ProductTagsRoutingModule,
        PageHeaderModule,
        FormsModule
    ],
    declarations: [ProductTagsComponent, FilterPipe]
})
export class ProductTagsModule { }
