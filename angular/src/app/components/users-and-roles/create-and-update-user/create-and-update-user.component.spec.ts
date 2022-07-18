import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAndUpdateUserComponent } from './create-and-update-user.component';

describe('CreateAndUpdateUserComponent', () => {
  let component: CreateAndUpdateUserComponent;
  let fixture: ComponentFixture<CreateAndUpdateUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateAndUpdateUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateAndUpdateUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
