import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReturnRequestsComponent } from './return-requests.component';

describe('ReturnRequestsComponent', () => {
  let component: ReturnRequestsComponent;
  let fixture: ComponentFixture<ReturnRequestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReturnRequestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReturnRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
