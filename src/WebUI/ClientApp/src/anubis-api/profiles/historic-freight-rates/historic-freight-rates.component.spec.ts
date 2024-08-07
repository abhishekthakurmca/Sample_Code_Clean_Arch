import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricFreightRatesComponent } from './historic-freight-rates.component';

describe('HistoricFreightRatesComponent', () => {
  let component: HistoricFreightRatesComponent;
  let fixture: ComponentFixture<HistoricFreightRatesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoricFreightRatesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoricFreightRatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
