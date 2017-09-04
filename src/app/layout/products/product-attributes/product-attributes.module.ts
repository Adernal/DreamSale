import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductAttributesRoutingModule } from './product-attributes-routing.module';
import { ProductAttributesComponent } from './product-attributes.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { ProductAttributesService } from './product-attributes.service';
import { FilterPipe } from './filter.pipe';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
    imports: [
        CommonModule,
        ProductAttributesRoutingModule,
        PageHeaderModule,
        FormsModule,
        HttpModule,
        NgxPaginationModule
    ],
    providers:[ ProductAttributesService],
    declarations: [ProductAttributesComponent, FilterPipe]
})
export class ProductAttributesModule { }
