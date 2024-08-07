import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WhiteGloveComponent } from './white-glove.component';

describe('WhiteGloveComponent', () => {
  let component: WhiteGloveComponent;
  let fixture: ComponentFixture<WhiteGloveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WhiteGloveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WhiteGloveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
