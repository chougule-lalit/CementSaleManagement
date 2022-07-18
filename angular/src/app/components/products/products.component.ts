import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {FormBuilder} from '@angular/forms';
import {MatSort} from '@angular/material/sort';
import {MatDialog} from '@angular/material/dialog';
import {ProductFormComponent} from './product-form/product-form.component';
import {CommonService} from "../../shared/services/common.service";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html'
})
export class ProductsComponent implements OnInit {
  displayedColumns = ['id', 'productName', 'companyName', 'price', 'actions'];
  dataSource: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private commonService: CommonService, private fb: FormBuilder, public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getProductList();
  }

  getProductList(): void {
    const input = {
      maxResultCount: 100,
      skipCount: 0,
    };
    this.commonService.postRequest('Product/fetchProductMasterList', input).subscribe((result: any) => {
      console.log('Listing Page Data : ', result);
      this.dataSource = result.items;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  add(): void {
    const dialogRef = this.dialog.open(ProductFormComponent);
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed after insert : ', result);
      if (result) {
        this.getProductList();
      }
    });
  }

  edit(editData: any): void {
    console.log('Edit Data : ', editData);
    const dialogRef = this.dialog.open(ProductFormComponent, {
      data: editData,
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed after update : ', result);
      if (result) {
        this.getProductList();
      }
    });
  }

  delete(id: any): void {
    this.commonService.deleteRequestWithId('Product/delete', id).subscribe((data) => {
      console.log('User Delete Resp : ', data);
      this.getProductList();
    });
  }
}
