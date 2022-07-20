import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CancelledPurchaseListComponent } from './cancelled-purchase-list.component';

describe('CancelledPurchaseListComponent', () => {
  let component: CancelledPurchaseListComponent;
  let fixture: ComponentFixture<CancelledPurchaseListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CancelledPurchaseListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CancelledPurchaseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
