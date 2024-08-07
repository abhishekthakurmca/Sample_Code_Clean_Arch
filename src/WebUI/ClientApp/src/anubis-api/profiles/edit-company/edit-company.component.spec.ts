import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCompany } from './edit-company.component';

describe('EditCompany', () => {
  let component: EditCompany;
  let fixture: ComponentFixture<EditCompany>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [EditCompany ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCompany);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
