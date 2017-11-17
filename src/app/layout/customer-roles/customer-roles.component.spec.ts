import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerRolesComponent } from './customer-roles.component';

describe('CustomerRolesComponent', () => {
  let component: CustomerRolesComponent;
  let fixture: ComponentFixture<CustomerRolesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerRolesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerRolesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
