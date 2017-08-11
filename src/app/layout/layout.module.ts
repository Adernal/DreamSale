import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';

import { LayoutRoutingModule } from './layout-routing.module';
import { LayoutComponent } from './layout.component';
import { HeaderComponent, SidebarComponent } from '../shared';
import { ShipmentsComponent } from './shipments/shipments.component';
import { GiftCardsComponent } from './gift-cards/gift-cards.component';
import { CustomerRolesComponent } from './customer-roles/customer-roles.component';
import { RecurringPaymentsComponent } from './recurring-payments/recurring-payments.component';


// import { VendorsComponent } from './vendors/vendors.component';


@NgModule({
    imports: [
        CommonModule,
        NgbDropdownModule.forRoot(),
        LayoutRoutingModule,
        TranslateModule
    ],
    declarations: [
        LayoutComponent,
        HeaderComponent,
        SidebarComponent,
        ShipmentsComponent,
        GiftCardsComponent,
        CustomerRolesComponent,
        RecurringPaymentsComponent,
      ]
})
export class LayoutModule { }
