import {Component, Inject, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from 'src/app/shared/services/common.service';

@Component({
  selector: 'app-create-and-update-user',
  templateUrl: './create-and-update-user.component.html'
})
export class CreateAndUpdateUserComponent implements OnInit {
  roles: any[] = [];
  selectedRole = 1;
  form!: FormGroup;
  mode = 'Create';
  isSubmitted = false;

  constructor(
    public dialogRef: MatDialogRef<CreateAndUpdateUserComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private commonService: CommonService
  ) {
  }

  getRoles(): void {
    this.commonService.getRequest('RoleMaster/getRoleDropdown').subscribe((result) => {
      this.roles = result;
      console.log('Roles : ', this.roles);
    });
  }

  ngOnInit(): void {
    this.getRoles();
    const emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    const phoneRegex = /^[0-9]{10}$/;
    this.form = this.fb.group({
      id: [null],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.pattern(emailRegex)]],
      phone: ['', [Validators.required, Validators.pattern(phoneRegex)]],
      roleId: [1],
      password: ['', [(this.data) ? Validators.nullValidator : Validators.required]]
    });

    if (this.data) {
      console.log('Edit Data : ', this.data);
      this.mode = 'Update';
      this.selectedRole = this.data.roleId;
      this.form.patchValue({
        id: this.data.id,
        firstName: this.data.firstName,
        lastName: this.data.lastName,
        email: this.data.email,
        phone: this.data.phone,
      });

      console.log('patchValue : ', this.form.value);

    }
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    this.form.patchValue({
      roleId: this.selectedRole
    });
    console.log('Form Data : ', this.form.value);
    this.isSubmitted = true;
    if (this.form.invalid) {
      return;
    }

    this.commonService.createOrUpdateUser(this.form.value).subscribe((resp) => {
      console.log('User Save Resp', resp);
      this.dialogRef.close(true);
    });
  }

}
