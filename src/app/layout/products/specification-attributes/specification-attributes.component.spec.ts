import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecificationAttributesComponent } from './specification-attributes.component';

describe('SpecificationAttributesComponent', () => {
  let component: SpecificationAttributesComponent;
  let fixture: ComponentFixture<SpecificationAttributesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpecificationAttributesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpecificationAttributesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
