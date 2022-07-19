import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from "../../../shared/services/common.service";

@Component({
  selector: 'app-orders-form',
  templateUrl: './orders-form.component.html'
})
export class OrdersFormComponent implements OnInit {
  // form!: FormGroup;
  mode = 'Create';
  // isSubmitted = false;
  selectedProduct!: string;
  supplierHolder: any[] = [];
  productDDHolder: any[] = [];
  selectedSupplierId!: number;
  ordersHolder!: any;
  orderDetailsHolder: any[] = [];
  totalAmount = 0;
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
    // this.form = this.fb.group({
    //   id: [null],
    //   itemCount: ['', [Validators.required]],
    //   amount: ['', [Validators.required]],
    //   userMasterId: [JSON.parse(localStorage.getItem('user-details')!).id],
    //   isActive: [true,],
    //   orderDetails: this.fb.array([])
    // });

    if (this.data) {
      console.log('Edit Data : ', this.data);
      this.mode = 'Update';
      // this.form.patchValue({
      //   id: this.data.id,
      //   itemCount: this.data.itemCount,
      //   amount: this.data.amount,
      //   userMasterId: this.data.userMasterId,
      //   isActive: this.data.isActive,
      //   orderDetails: this.data.orderDetails,
      // });

      // console.log('patchValue : ', this.form.value);

    }
    this.getProduct();
  }

  // get orderDetailsControlArray(): FormArray {
  //   return this.form.get('orderDetails') as FormArray;
  // }
  //
  // orderDetails(): FormGroup {
  //   return this.fb.group({
  //     id: [null],
  //     productMasterId: ['', [Validators.required]],
  //     count: [0, [Validators.required]],
  //     amount: [0, [Validators.required]],
  //   })
  // }

  // createOrders() {
  //   this.orderDetailsControlArray.push(this.orderDetails());
  // }
  //
  removeOrders(i: number) {
    // this.orderDetailsControlArray.removeAt(i);
  }

  // get f(): { [key: string]: AbstractControl } {
  //   return this.form.controls;
  // }

  addOrders() {
    let orderDetails = {
      id: null,
      amount: null,
      count: null,
      productMasterId: null,
    };
    this.orderDetailsHolder.push(orderDetails);
    console.log('Orders Details Holder : ', this.orderDetailsHolder);
  }

  getProduct() {
    this.commonService.getRequest('Product/getProductMasterDropdown').subscribe((result) => {
      console.log('Get Product : ', result);
      this.productDDHolder = result;
    })
  }

  updateAmt(event: any, i: number) {
    let id = event.target.value;
    console.log('id : ', id);
    this.orderDetailsHolder[i].count = 1;
    this.productDDHolder.filter((item) => {
      if (+item.id === +id) {
        this.orderDetailsHolder[i].amount = item.price;
        this.totalAmt();
      }
    })
  }

  updateCount(event: any, i: number) {
    let count = +event.target.value;
    if(count > 0){
      this.productDDHolder.filter((item) => {
        if (+item.id === +this.orderDetailsHolder[i].productMasterId) {
          this.orderDetailsHolder[i].amount = +item.price * +count;
          this.totalAmt();
        }
      })
    }else{
      this.orderDetailsHolder[i].count = 1;
    }
  }

  totalAmt(){
    this.orderDetailsHolder.forEach((item) => {
      console.log('item amt : ', item.amount);
      console.log('totalAmount amt : ', this.totalAmount);
      this.totalAmount = +item.amount + +this.totalAmount;
    });
  }

  onSubmit(): void {

    console.log('Orders Details Holder : ', this.orderDetailsHolder);
    //   let totalCount = 0;
    //   let totalAmt = 0;
    //   this.form.value.orderDetails.forEach((item: any) => {
    //     totalAmt = (+item.amount * item.count) + totalAmt;
    //     totalCount = +item.count + totalCount;
    //   });
    //
    //   this.form.patchValue({
    //     itemCount: totalCount,
    //     amount: totalAmt
    //   });
    //   console.log('Ta tc : ', totalAmt, totalCount);
    //
    //   console.log('Form Data : ', this.form.value);
    //   this.isSubmitted = true;
    //   if (this.form.invalid) {
    //     return;
    //   }
    //   this.commonService.postRequest('Order/createOrUpdate', this.form.value).subscribe((resp: any) => {
    //     console.log('Save Resp', resp);
    //     this.dialogRef.close(true);
    //   });
    //
  }

  trackBy(index: number, item: any) {
    return item.id;
  }
}

export class Orders implements IOrders {
  id!: number | undefined;
  itemCount!: number | undefined;
  amount!: number | undefined;
  userMasterId!: number | undefined;
  isActive!: boolean | undefined;
  orderDetails!: OrdersDetails[] | undefined;
}

export class OrdersDetails implements IOrdersDetails {
  id!: number | undefined;
  productMasterId!: number | undefined;
  count!: number | undefined;
  amount!: number | undefined;
}

export interface IOrders {
  id: number | undefined;
  itemCount: number | undefined;
  amount: number | undefined;
  userMasterId: number | undefined;
  isActive: boolean | undefined;
  orderDetails: IOrdersDetails[] | undefined;
}

export interface IOrdersDetails {
  id: number | undefined;
  productMasterId: number | undefined;
  count: number | undefined;
  amount: number | undefined;
}
