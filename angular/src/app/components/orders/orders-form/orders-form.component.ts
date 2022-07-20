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
  totalCount = 0;
  cancelDate!: any;
  orderDate!: any;

  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private commonService: CommonService
  ) {
  }

  ngOnInit(): void {
    if (this.data) {
      this.mode = 'Update';
    }
    this.getProduct();
    if (this.data) {
      this.mode = 'Update';
      this.commonService.getRequestWithId(`Order/get`, this.data.orderId).subscribe((resp) => {
        this.cancelDate = resp.cancelDate;
        this.orderDate = resp.orderDate;
        this.orderDetailsHolder = resp.orderDetails;
        this.totalAmount = resp.amount;
        this.totalCount = resp.itemCount;
      });
    }
  }


  removeOrders(i: number,) {
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
    this.totalCount = 0;
    this.orderDetailsHolder.forEach((item) => {
      this.totalCount = this.totalCount + item.count;
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


    let formData: any = {
      id: this.data?.orderId ? this.data.orderId : null,
      itemCount: itemCount,
      amount: this.totalAmount,
      userMasterId: JSON.parse(localStorage.getItem('user-details')!).id,
      isActive: true,
      orderDetails: this.orderDetailsHolder
    };

    if (this.data?.orderId && this.cancelDate && this.orderDate) {
      formData.cancelDate = this.cancelDate;
      formData.orderDate = this.orderDate;
    }


    this.commonService.postRequest('Order/createOrUpdate', formData).subscribe((resp: any) => {
      this.dialogRef.close(true);
    });
  }

  trackBy(index: number, item: any) {
    return item.id;
  }
}
