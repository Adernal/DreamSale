import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BestSellerRoutingModule } from './best-seller-routing.module';
import { BestSellerComponent } from './best-seller.component';
import { PageHeaderModule } from './../../shared';
import { FormsModule } from '@angular/forms';

import { BestSellerService } from './best-seller.service';


@NgModule({
  imports: [
    CommonModule,
    BestSellerRoutingModule,
    PageHeaderModule,
    FormsModule,
    // ImageUploadModule.forRoot()
    // ProductTagsModule
  ],
  providers:[BestSellerService],
  declarations: [BestSellerComponent]
})
export class BestSellerModule { }
