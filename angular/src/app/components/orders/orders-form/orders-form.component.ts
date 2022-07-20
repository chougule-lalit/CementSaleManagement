import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from "../../../shared/services/common.service";

@Component({
  selector: 'app-orders-form',
  templateUrl: './orders-form.component.html'
})
export class OrdersFormComponent implements OnInit {
  mode = 'Create';
  supplierHolder: any[] = [];
  productDDHolder: any[] = [];
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
    if (this.data) {
      this.mode = 'Update';
    }
    this.getProduct();
  }


  removeOrders(i: number, ) {
    this.orderDetailsHolder.splice(i, 1);
    this.totalAmount = 0;
    this.totalAmt();
  }

  addOrders() {
    let orderDetails = {
      id: null,
      orderMasterId: null,
      amount: null,
      count: null,
      productMasterId: null,
    };
    this.orderDetailsHolder.push(orderDetails);
  }

  getProduct() {
    this.commonService.getRequest('Product/getProductMasterDropdown').subscribe((result) => {
      this.productDDHolder = result;
    })
  }

  updateAmt(event: any, i: number) {
    let id = event.target.value;
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
    if (count > 0) {
      this.productDDHolder.filter((item) => {
        if (+item.id === +this.orderDetailsHolder[i].productMasterId) {
          this.orderDetailsHolder[i].amount = +item.price * +count;
          this.totalAmt();
        }
      })
    } else {
      this.orderDetailsHolder[i].count = 1;
    }
  }

  totalAmt() {
    this.totalAmount = 0;
    this.orderDetailsHolder.forEach((item) => {
      this.totalAmount = +item.amount + +this.totalAmount;
    });
  }

  onSubmit(): void {

    let itemCount = 0;
    this.orderDetailsHolder.forEach((item) => {
      if (item.count > 0) {
        itemCount = item.count + itemCount;
      } else {
        alert('Please Enter Count');
      }
    });


    let formData = {
      id: null,
      itemCount: itemCount,
      amount: this.totalAmount,
      userMasterId: JSON.parse(localStorage.getItem('user-details')!).id,
      isActive: true,
      orderDetails: this.orderDetailsHolder
    };

    console.log('formData : ', formData);

    this.commonService.postRequest('Order/createOrUpdate', formData).subscribe((resp: any) => {
      this.dialogRef.close(true);
    });
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
