import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReturnRequestsRoutingModule } from './return-requests-routing.module';
import { ReturnRequestsComponent } from './return-requests.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { ReturnRequestService } from './return-requests.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { MyDatePickerModule } from '../../../../node_modules/angular4-datepicker/src/my-date-picker';




@NgModule({
  imports: [
    CommonModule,
    ReturnRequestsRoutingModule,
    PageHeaderModule,
    FormsModule,
    NgxPaginationModule,
    MyDatePickerModule
  
  ],
  providers:[ReturnRequestService],
  declarations: [ReturnRequestsComponent]
})
export class ReturnRequestsModule { }
