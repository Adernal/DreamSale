import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NeverSoldRoutingModule } from './never-sold-routing.module';
import { NeverSoldComponent } from './never-sold.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { NeverSoldService } from './never-sold.service';


@NgModule({
  imports: [
    CommonModule,
    NeverSoldRoutingModule,
    PageHeaderModule,
    FormsModule,
    NgxPaginationModule
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  providers:[NeverSoldService],
  declarations: [NeverSoldComponent]
})
export class NeverSoldModule { }
