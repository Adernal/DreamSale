import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoresRoutingModule } from './stores-routing.module';
import { StoresComponent } from './stores.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';
import { StoresService } from './stores.service';


@NgModule({
  imports: [
    CommonModule,
    StoresRoutingModule,
    PageHeaderModule,
    FormsModule,
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  providers:[StoresService],
  declarations: [StoresComponent, FilterPipe]
})
export class StoresModule { }
