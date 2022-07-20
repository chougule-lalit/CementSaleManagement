import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAndUpdateModalComponent } from './create-and-update-modal.component';

describe('CreateAndUpdateModalComponent', () => {
  let component: CreateAndUpdateModalComponent;
  let fixture: ComponentFixture<CreateAndUpdateModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateAndUpdateModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateAndUpdateModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
