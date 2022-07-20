import {Component, Inject, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from "../../../shared/services/common.service";

@Component({
  selector: 'app-workers-form',
  templateUrl: './workers-form.component.html'
})
export class WorkersFormComponent implements OnInit {

  form!: FormGroup;
  mode = 'Create';
  isSubmitted = false;

  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private commonService: CommonService
  ) {
  }

  ngOnInit(): void {
    const phoneRegex = /^[0-9]{10}$/;
    this.form = this.fb.group({
      id: [null],
      firstName: ['', [Validators.required]],
      middleName: [''],
      lastName: ['', [Validators.required]],
      phone: ['', [Validators.required, Validators.pattern(phoneRegex)]],
      DOB: ['', [Validators.required]],
      joiningDate: ['', [Validators.required]],
      experience: ['', [Validators.required]]
    });

    if (this.data) {
      console.log('Edit Data : ', this.data);
      this.mode = 'Update';
      this.form.patchValue({
        id: this.data.id,
        firstName: this.data.firstName,
        middleName: this.data.middleName,
        lastName: this.data.lastName,
        email: this.data.email,
        phone: this.data.phone,
        DOB: this.data.DOB,
        joiningDate: this.data.joiningDate,
        experience: this.data.experience
      });

      console.log('patchValue : ', this.form.value);

    }
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    console.log('Form Data : ', this.form.value);
    this.isSubmitted = true;
    if (this.form.invalid) {
      return;
    }
    this.commonService.postRequest('CreateOrUpdateWoker', this.form.value).subscribe((resp) => {
      console.log('Save Resp', resp);
      this.dialogRef.close(true);
    });

  }

}
