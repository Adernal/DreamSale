import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SignupRoutingModule } from './signup-routing.module';
import { SignupComponent } from './signup.component';
import { SignupService } from './signup.service';
import { FormsModule } from '@angular/forms';
import { URLService } from '../shared/services';

@NgModule({
  imports: [
    CommonModule,
    SignupRoutingModule,
    FormsModule
  ],
  providers:[SignupService,URLService],
  declarations: [SignupComponent]
})
export class SignupModule { }
