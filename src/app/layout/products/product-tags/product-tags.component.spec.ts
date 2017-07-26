import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductTagsComponent } from './product-tags.component';

describe('ProductTagsComponent', () => {
  let component: ProductTagsComponent;
  let fixture: ComponentFixture<ProductTagsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductTagsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductTagsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
