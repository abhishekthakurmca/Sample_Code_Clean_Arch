import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FreightcompanyComponent } from '../freight-company/freight-company.component';

describe('FreightcompanyComponent', () => {
  let component: FreightcompanyComponent;
  let fixture: ComponentFixture<FreightcompanyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [FreightcompanyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FreightcompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
