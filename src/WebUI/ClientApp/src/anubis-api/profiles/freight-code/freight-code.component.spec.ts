import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FreightCodeComponent } from './freight-code.component';

describe('FreightCodeComponent', () => {
  let component: FreightCodeComponent;
  let fixture: ComponentFixture<FreightCodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FreightCodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FreightCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
