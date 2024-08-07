import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FreightCategoryComponent } from './freight-category.component';

describe('FreightCategoryComponent', () => {
  let component: FreightCategoryComponent;
  let fixture: ComponentFixture<FreightCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FreightCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FreightCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
