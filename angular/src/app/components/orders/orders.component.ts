import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {FormBuilder} from '@angular/forms';
import {MatSort} from '@angular/material/sort';
import {MatDialog} from '@angular/material/dialog';
import {OrdersFormComponent} from './orders-form/orders-form.component';
import {CommonService} from "../../shared/services/common.service";
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html'
})
export class OrdersComponent implements OnInit {
  displayedColumns = ['id', 'customerName', 'amount', 'itemCount', 'orderDate', 'actions'];
  dataSource: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  blob;

  constructor(private commonService: CommonService, private _snackBar: MatSnackBar, private fb: FormBuilder, public dialog: MatDialog) {
  }


  ngOnInit(): void {
    this.getOrdersList();
  }

  getOrdersList(): void {
    const input = {
      maxResultCount: 100,
      skipCount: 0,
    };
    this.commonService.postRequest('Order/fetchOrderList', input).subscribe((result) => {
      this.dataSource = result.items;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  getOrdersReport() {
    this.commonService.getFile('Order/getPurchaseReport').subscribe((result) => {
      this.blob = new Blob([result], {type: 'application/xlsx'});
      let downloadURL = window.URL.createObjectURL(result);
      let link = document.createElement('a');
      link.href = downloadURL;
      link.download = "orders-report.xlsx";
      link.click();
    });
  }

  getOrdersCancelledReport() {
    this.commonService.getFile('Order/getPurchaseCancelReport').subscribe((result) => {
      this.blob = new Blob([result], {type: 'application/xlsx'});
      let downloadURL = window.URL.createObjectURL(result);
      let link = document.createElement('a');
      link.href = downloadURL;
      link.download = "cancelled-orders-report.xlsx";
      link.click();
    });
  }

  add(): void {
    const dialogRef = this.dialog.open(OrdersFormComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getOrdersList();
      }
    });
  }

  edit(editData: any): void {
    const dialogRef = this.dialog.open(OrdersFormComponent, {
      data: editData,
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getOrdersList();
      }
    });
  }

  delete(id: any): void {
    if (confirm('Are You Sure! Delete this order')) {
      this.commonService.deleteRequestWithId('Order/delete', id).subscribe((resp) => {
        if (resp) {
          this.openSnackBar('Order Deleted Successfully', 'Close');
          this.getOrdersList();
        }
      });
    }
  }

  cancelOrder(id: any): void {
    if (confirm('Are You Sure! Cancel this order')) {
      this.commonService.postRequest(`Order/cancelOrder?id=${id}`, {}).subscribe((resp) => {
        if (resp) {
          this.openSnackBar('Order Cancelled Successfully', 'Close');
          this.getOrdersList();
        }
      });
    }
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }

}
