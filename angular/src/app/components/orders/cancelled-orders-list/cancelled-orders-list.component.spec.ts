import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CancelledOrdersListComponent } from './cancelled-orders-list.component';

describe('CancelledOrdersListComponent', () => {
  let component: CancelledOrdersListComponent;
  let fixture: ComponentFixture<CancelledOrdersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CancelledOrdersListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CancelledOrdersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
