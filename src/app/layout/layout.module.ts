import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';

import { LayoutRoutingModule } from './layout-routing.module';
import { LayoutComponent } from './layout.component';
import { HeaderComponent, SidebarComponent } from '../shared';
import { CustomerComponent } from './customer/customer.component';
import { OrdersComponent } from './orders/orders.component';
import { StoresComponent } from './stores/stores.component';
import { LogComponent } from './log/log.component';
import { ReturnRequestsComponent } from './return-requests/return-requests.component';

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
        CustomerComponent,
        OrdersComponent,
        StoresComponent,
        LogComponent,
        ReturnRequestsComponent,
    



    ]
})
export class LayoutModule { }
