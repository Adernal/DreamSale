import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SpecificationAttributesRoutingModule } from './specification-attributes-routing.module';
import { SpecificationAttributesComponent } from './specification-attributes.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';
import { SpecificationAttributesService } from './specification-attributes.service';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
    imports: [
        CommonModule,
        SpecificationAttributesRoutingModule,
        PageHeaderModule,
        FormsModule,
        NgxPaginationModule,
    ],
    providers:[SpecificationAttributesService],
    declarations: [SpecificationAttributesComponent, FilterPipe]
})
export class SpecificationAttributesModule { }
