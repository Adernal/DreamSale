import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NeverSoldComponent } from './never-sold.component';

describe('NeverSoldComponent', () => {
  let component: NeverSoldComponent;
  let fixture: ComponentFixture<NeverSoldComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NeverSoldComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NeverSoldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
