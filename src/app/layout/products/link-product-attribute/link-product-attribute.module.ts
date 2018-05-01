import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LinkProductAttributeRoutingModule } from './link-product-attribute-routing.module';
import { LinkProductAttributeComponent } from './link-product-attribute.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { FilterPipe } from './filter.pipe';
import { LinkProductAttributeService } from './link-product-attribute.service';
import { URLService } from '../../../shared/services';

@NgModule({
    imports: [
        CommonModule,
        LinkProductAttributeRoutingModule,
        PageHeaderModule,
        FormsModule,
        HttpModule
    ],
    providers:[LinkProductAttributeService,URLService],
    declarations: [LinkProductAttributeComponent, FilterPipe]
})
export class LinkProductAttributeModule { }
