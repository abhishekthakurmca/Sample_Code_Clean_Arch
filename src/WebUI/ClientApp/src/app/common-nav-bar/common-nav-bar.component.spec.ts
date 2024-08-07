import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonNavBarComponent } from './common-nav-bar.component';

describe('CommonNavBarComponent', () => {
  let component: CommonNavBarComponent;
  let fixture: ComponentFixture<CommonNavBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CommonNavBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CommonNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
