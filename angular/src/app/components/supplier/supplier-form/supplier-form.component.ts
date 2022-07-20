import {Component, Inject, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {CreateAndUpdateModalComponent} from '../../inquiry/create-and-update-modal/create-and-update-modal.component';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from '../../../shared/services/common.service';

@Component({
  selector: 'app-supplier-form',
  templateUrl: './supplier-form.component.html'
})
export class SupplierFormComponent implements OnInit {

  form!: FormGroup;
  mode = 'Create';
  isSubmitted = false;
  productHolder: any[] = [];
  selectedProduct!: number;
  constructor(
    public dialogRef: MatDialogRef<CreateAndUpdateModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private commonService: CommonService
  ) {
  }

  getProductList(){
    this.commonService.getRequest('').subscribe((result) => {
      this.productHolder = result;
    })
  }

  ngOnInit(): void {
    const emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    const phoneRegex = /^[0-9]{10}$/;
    this.form = this.fb.group({
      id: [null],
      supplier_Name: ['', [Validators.required]],
      supplier_Address: ['', [Validators.required]],
      product_Name: [''],
      email: ['', [Validators.required, Validators.pattern(emailRegex)]],
      phone: ['', [Validators.required, Validators.pattern(phoneRegex)]]
    });

    if (this.data) {
      this.mode = 'Update';
      this.form.patchValue({
        id: this.data.id,
        supplier_Name: this.data.supplier_Name,
        supplier_Address: this.data.supplier_Address,
        product_Name: this.data.product_Name,
        email: this.data.email,
        phone: this.data.phone,
      });
    }
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    this.isSubmitted = true;
    if (this.form.invalid) {
      return;
    }

    if(!this.selectedProduct){
      alert('Please Select Product');
      return;
    }else{
      this.form.patchValue({
        product_Name: this.selectedProduct
      })
    }
    this.commonService.postRequest('SupplierMaster/createOrUpdate', this.form.value).subscribe((resp) => {
      this.dialogRef.close(true);
    });
  }

}
