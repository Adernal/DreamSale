import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductPicturesComponent } from './product-pictures.component';

describe('ProductPicturesComponent', () => {
  let component: ProductPicturesComponent;
  let fixture: ComponentFixture<ProductPicturesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductPicturesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductPicturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
