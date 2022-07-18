import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {FormBuilder} from '@angular/forms';
import {MatSort} from '@angular/material/sort';
import {MatDialog} from '@angular/material/dialog';
import {OrdersFormComponent} from './orders-form/orders-form.component';
import {CommonService} from "../../shared/services/common.service";

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html'
})
export class OrdersComponent implements OnInit {
  displayedColumns = ['id', 'customerName', 'amount', 'itemCount', 'orderDate', 'actions'];
  dataSource: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private commonService: CommonService, private fb: FormBuilder, public dialog: MatDialog) {
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
      console.log('fetchUserList : ', result);
      this.dataSource = result.items;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  add(): void {
    const dialogRef = this.dialog.open(OrdersFormComponent);
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed after insert : ', result);
      if (result) {
        this.getOrdersList();
      }
    });
  }

  edit(editData: any): void {
    console.log('Edit Data : ', editData);
    const dialogRef = this.dialog.open(OrdersFormComponent, {
      data: editData,
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed after update : ', result);
      if (result) {
        this.getOrdersList();
      }
    });
  }

  delete(id: any): void {
    this.commonService.deleteRequestWithId('Order/delete', id).subscribe((data) => {
      console.log('User Delete Resp : ', data);
      this.getOrdersList();
    });
  }

  cancelOrder(id: any): void {
    this.commonService.postRequest('Order/cancelOrder', {id}).subscribe((data) => {
      console.log('User Delete Resp : ', data);
      this.getOrdersList();
    });
  }
}
