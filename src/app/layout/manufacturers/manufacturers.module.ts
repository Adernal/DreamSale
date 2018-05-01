import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManufacturersRoutingModule } from './manufacturers-routing.module';
import { ManufacturersComponent } from './manufacturers.component';
import { ManufacturersService } from './manufacturers.service';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';
import { NgxPaginationModule } from 'ngx-pagination';
import { ImageUploadModule } from '../../../../node_modules/angular2-image-upload/src/image-upload.module';
import { URLService } from '../../shared/services';
@NgModule({
  imports: [
    CommonModule,
    ManufacturersRoutingModule,
    FormsModule,
    NgxPaginationModule,
    ImageUploadModule.forRoot()
  ],
  providers:[ManufacturersService,URLService],
  declarations: [ManufacturersComponent, FilterPipe]
})
export class ManufacturersModule { }
