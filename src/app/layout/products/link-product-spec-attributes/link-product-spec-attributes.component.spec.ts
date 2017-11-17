import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkProductSpecAttributesComponent } from './link-product-spec-attributes.component';

describe('LinkProductSpecAttributesComponent', () => {
  let component: LinkProductSpecAttributesComponent;
  let fixture: ComponentFixture<LinkProductSpecAttributesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LinkProductSpecAttributesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkProductSpecAttributesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
