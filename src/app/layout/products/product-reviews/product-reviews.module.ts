import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductReviewsRoutingModule } from './product-reviews-routing.module';
import { ProductReviewsComponent } from './product-reviews.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './filter.pipe';
import { ProductReviewsService } from './product-reviews.service';

@NgModule({
    imports: [
        CommonModule,
        ProductReviewsRoutingModule,
        PageHeaderModule,
        FormsModule
    ],
    providers:[ProductReviewsService],
    declarations: [ProductReviewsComponent, FilterPipe]
})
export class ProductReviewsModule { }
