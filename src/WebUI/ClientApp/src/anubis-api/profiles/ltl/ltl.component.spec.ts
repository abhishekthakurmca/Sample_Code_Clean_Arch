import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LTLComponent } from './ltl.component';

describe('LTLComponent', () => {
  let component: LTLComponent;
  let fixture: ComponentFixture<LTLComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LTLComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LTLComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
