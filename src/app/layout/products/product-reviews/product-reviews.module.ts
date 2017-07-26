import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductReviewsRoutingModule } from './product-reviews-routing.module';
import { ProductReviewsComponent } from './product-reviews.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule } from '@angular/forms';

@NgModule({
    imports: [
        CommonModule,
        ProductReviewsRoutingModule,
        PageHeaderModule,
        FormsModule
    ],
    declarations: [ProductReviewsComponent]
})
export class ProductReviewsModule { }
