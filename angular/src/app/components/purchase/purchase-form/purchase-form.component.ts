import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from "../../../shared/services/common.service";

@Component({
  selector: 'app-purchase-form',
  templateUrl: './purchase-form.component.html'
})
export class PurchaseFormComponent implements OnInit {
  mode = 'Create';
  supplierHolder: any[] = [];
  productDDHolder: any[] = [];
  purchaseDetailsHolder: any[] = [];
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
      this.commonService.getRequestWithId('Purchase/get', this.data.purchaseId).subscribe((resp) => {
        this.cancelDate = resp.cancelDate;
        this.orderDate = resp.orderDate;
        this.purchaseDetailsHolder = resp.purchaseDetails;
        this.totalAmount = resp.amount;
        this.totalCount = resp.itemCount;
      });
    }
  }


  removePurchase(i: number) {
    this.purchaseDetailsHolder.splice(i, 1);
    this.totalAmt();
  }

  addPurchase() {
    let purchaseDetails = {
      id: null,
      purchaseMasterId: null,
      amount: null,
      count: null,
      productMasterId: null,
    };
    this.purchaseDetailsHolder.push(purchaseDetails);
  }

  getProduct() {
    this.commonService.getRequest('Product/getProductMasterDropdown').subscribe((result) => {
      this.productDDHolder = result;
    })
  }

  updateAmt(event: any, i: number) {
    let id = event.target.value;
    this.purchaseDetailsHolder[i].count = 1;
    this.productDDHolder.filter((item) => {
      if (+item.id === +id) {
        this.purchaseDetailsHolder[i].amount = item.price;
        this.totalAmt();
      }
    })
  }

  updateCount(event: any, i: number) {
    let count = +event.target.value;
    if (count > 0) {
      this.productDDHolder.filter((item) => {
        if (+item.id === +this.purchaseDetailsHolder[i].productMasterId) {
          this.purchaseDetailsHolder[i].amount = +item.price * +count;
          this.totalAmt();
        }
      })
    } else {
      this.purchaseDetailsHolder[i].count = 1;
    }
  }

  totalAmt() {
    this.totalAmount = 0;
    this.totalCount = 0;
    this.purchaseDetailsHolder.forEach((item) => {
      this.totalCount = this.totalCount + item.count;
      this.totalAmount = +item.amount + +this.totalAmount;
    });
  }

  onSubmit(): void {

    let itemCount = 0;
    this.purchaseDetailsHolder.forEach((item) => {
      if (item.count > 0) {
        itemCount = item.count + itemCount;
      } else {
        alert('Please Enter Count');
      }
    });


    let formData: any = {
      id: this.data?.purchaseId ? this.data.purchaseId : null,
      itemCount: itemCount,
      amount: this.totalAmount,
      userMasterId: JSON.parse(localStorage.getItem('user-details')!).id,
      isActive: true,
      purchaseDetails: this.purchaseDetailsHolder
    };

    if(this.data?.orderId && this.cancelDate && this.orderDate){
      formData.cancelDate = this.cancelDate;
      formData.orderDate = this.orderDate;
    }
    this.commonService.postRequest('Purchase/createOrUpdate', formData).subscribe((resp: any) => {
      this.dialogRef.close(true);
    });
  }

  trackBy(index: number, item: any) {
    return item.id;
  }
}
