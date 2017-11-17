import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkProductAttributeComponent } from './link-product-attribute.component';

describe('LinkProductAttributeComponent', () => {
  let component: LinkProductAttributeComponent;
  let fixture: ComponentFixture<LinkProductAttributeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LinkProductAttributeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkProductAttributeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
