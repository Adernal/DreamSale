import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManufacturersRoutingModule } from './manufacturers-routing.module';
import { ManufacturersComponent } from './manufacturers.component';
import { ManufacturersService } from './manufacturers.service';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    ManufacturersRoutingModule,
    FormsModule,
    NgxPaginationModule
  ],
  providers:[ManufacturersService],
  declarations: [ManufacturersComponent, FilterPipe]
})
export class ManufacturersModule { }
