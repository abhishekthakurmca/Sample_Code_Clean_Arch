import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FTLComponent } from './ftl.component';

describe('FTLComponent', () => {
  let component: FTLComponent;
  let fixture: ComponentFixture<FTLComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FTLComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FTLComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
