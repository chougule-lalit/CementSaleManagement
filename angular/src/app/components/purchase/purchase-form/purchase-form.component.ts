import {Component, Inject, OnInit} from '@angular/core';
import {AbstractControl, FormArray, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from "../../../shared/services/common.service";

@Component({
  selector: 'app-purchase-form',
  templateUrl: './purchase-form.component.html'
})
export class PurchaseFormComponent implements OnInit {
  form!: FormGroup;
  mode = 'Create';
  isSubmitted = false;
  selectedProduct!: string;
  supplierHolder: any[] = [];
  productDDHolder: any[] = [];
  selectedSupplierId!: number;

  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private commonService: CommonService
  ) {
  }

  getSupplierList() {
    this.commonService.getRequest('').subscribe((result: any) => {
      this.supplierHolder = result;
    })
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      id: [null],
      itemCount: ['', [Validators.required]],
      amount: ['', [Validators.required]],
      userMasterId: [JSON.parse(localStorage.getItem('user-details')!).id],
      isActive: [true,],
      purchaseDetails: this.fb.array([])
    });

    if (this.data) {
      console.log('Edit Data : ', this.data);
      this.mode = 'Update';
      this.form.patchValue({
        id: this.data.id,
        itemCount: this.data.itemCount,
        amount: this.data.amount,
        userMasterId: this.data.userMasterId,
        isActive: this.data.isActive,
        purchaseDetails: this.data.purchaseDetails,
      });

      console.log('patchValue : ', this.form.value);

    }
    this.getProduct();
  }

  get purchaseDetailsControlArray(): FormArray {
    return this.form.get('purchaseDetails') as FormArray;
  }

  purchaseDetails(): FormGroup {
    return this.fb.group({
      id: [null],
      productMasterId: ['', [Validators.required]],
      count: [0, [Validators.required]],
      amount: [0, [Validators.required]],
    })
  }

  createPurchase() {
    this.purchaseDetailsControlArray.push(this.purchaseDetails());
  }

  removePurchase(i: number) {
    this.purchaseDetailsControlArray.removeAt(i);
  }


  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  getProduct() {
    this.commonService.getRequest('Product/getProductMasterDropdown').subscribe((result) => {
      console.log('Get Product : ', result);
      this.productDDHolder = result;
    })
  }

  updateAmt(event: any, i: number){
    let id = event.target.value;
    console.log('id : ', id);
    this.productDDHolder.filter((item) => {
      if(+item.id === +id){
        console.log('Product Price : ', this.purchaseDetailsControlArray.controls[i].value.amount);
      }
    })
  }

  onSubmit(): void {


    let totalCount = 0;
    let totalAmt = 0;
    this.form.value.purchaseDetails.forEach((item: any) => {
      totalAmt = (+item.amount * item.count) + totalAmt;
      totalCount = +item.count + totalCount;
    });

    this.form.patchValue({
      itemCount: totalCount,
      amount: totalAmt
    });
    console.log('Ta tc : ', totalAmt, totalCount);

    console.log('Form Data : ', this.form.value);
    this.isSubmitted = true;
    if (this.form.invalid) {
      return;
    }
    this.commonService.postRequest('Order/createOrUpdate', this.form.value).subscribe((resp: any) => {
      console.log('Save Resp', resp);
      this.dialogRef.close(true);
    });

  }
}
