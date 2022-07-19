import {Component, OnInit, ViewChild} from '@angular/core';
import {CommonService} from "../../../shared/services/common.service";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";

@Component({
  selector: 'app-cancelled-orders-list',
  templateUrl: './cancelled-orders-list.component.html'
})
export class CancelledOrdersListComponent implements OnInit {

  displayedColumns = ['id', 'customerName', 'amount', 'itemCount', 'orderDate'];
  dataSource: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private commonService: CommonService) {
  }

  ngOnInit(): void {
    this.getOrdersList();
  }

  getOrdersList(): void {
    const input = {
      maxResultCount: 100,
      skipCount: 0,
    };
    this.commonService.postRequest('Order/fetchCancelledOrderList', input).subscribe((result) => {
      this.dataSource = result.items;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
}
