import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NeverSoldRoutingModule } from './never-sold-routing.module';
import { NeverSoldComponent } from './never-sold.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { NeverSoldService } from './never-sold.service';
import { MyDatePickerModule } from '../../../../node_modules/angular4-datepicker/src/my-date-picker';
import { URLService } from '../../shared/services';


@NgModule({
  imports: [
    CommonModule,
    NeverSoldRoutingModule,
    PageHeaderModule,
    FormsModule,
    NgxPaginationModule,
    MyDatePickerModule
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  providers:[NeverSoldService,URLService],
  declarations: [NeverSoldComponent]
})
export class NeverSoldModule { }
