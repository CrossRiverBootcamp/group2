import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccoutContainerComponent } from './accout-container.component';

describe('AccoutContainerComponent', () => {
  let component: AccoutContainerComponent;
  let fixture: ComponentFixture<AccoutContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccoutContainerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccoutContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
