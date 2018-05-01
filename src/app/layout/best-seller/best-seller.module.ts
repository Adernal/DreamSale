import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BestSellerRoutingModule } from './best-seller-routing.module';
import { BestSellerComponent } from './best-seller.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { BestSellerService } from './best-seller.service';
import { MyDatePickerModule } from '../../../../node_modules/angular4-datepicker/src/my-date-picker';
import { URLService } from '../../shared/services';


@NgModule({
  imports: [
    CommonModule,
    BestSellerRoutingModule,
    PageHeaderModule,
    FormsModule,
    NgxPaginationModule,
    MyDatePickerModule
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  providers:[BestSellerService,URLService],
  declarations: [BestSellerComponent]
})
export class BestSellerModule { }
