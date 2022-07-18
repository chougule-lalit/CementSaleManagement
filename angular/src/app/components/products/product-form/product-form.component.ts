import {Component, Inject, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from "../../../shared/services/common.service";

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html'
})
export class ProductFormComponent implements OnInit {
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
    this.form = this.fb.group({
      id: [null],
      productName: ['', [Validators.required]],
      companyName: ['', [Validators.required]],
      price: ['', [Validators.required]],
    });

    if (this.data) {
      console.log('Edit Data : ', this.data);
      this.mode = 'Update';
      this.form.patchValue({
        id: this.data.id,
        productName: this.data.productName,
        companyName: this.data.companyName,
        price: this.data.price,
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
    this.commonService.postRequest('Product/createOrUpdate', this.form.value).subscribe((resp) => {
      console.log('Save Resp', resp);
      this.dialogRef.close(true);
    });
  }
}
