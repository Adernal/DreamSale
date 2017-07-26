import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductAttributesRoutingModule } from './product-attributes-routing.module';
import { ProductAttributesComponent } from './product-attributes.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { ProductAttributesService } from './product-attributes.service';
import { FilterPipe } from './filter.pipe';

@NgModule({
    imports: [
        CommonModule,
        ProductAttributesRoutingModule,
        PageHeaderModule,
        FormsModule,
        HttpModule
    ],
    providers:[ ProductAttributesService],
    declarations: [ProductAttributesComponent, FilterPipe]
})
export class ProductAttributesModule { }
