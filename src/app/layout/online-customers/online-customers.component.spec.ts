import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OnlineCustomersComponent } from './online-customers.component';

describe('OnlineCustomersComponent', () => {
  let component: OnlineCustomersComponent;
  let fixture: ComponentFixture<OnlineCustomersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnlineCustomersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnlineCustomersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
