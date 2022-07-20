import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {FormBuilder} from '@angular/forms';
import {MatSort} from '@angular/material/sort';
import {MatDialog} from '@angular/material/dialog';
import {CommonService} from "../../shared/services/common.service";
import {PurchaseFormComponent} from "./purchase-form/purchase-form.component";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html'
})
export class PurchaseComponent implements OnInit {
  displayedColumns = ['id', 'supplierName', 'amount', 'itemCount', 'orderDate', 'actions'];
  blob;
  dataSource: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private commonService: CommonService, private _snackBar: MatSnackBar, private fb: FormBuilder, public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getPurchaseList();
  }

  getPurchaseList(): void {
    const input = {
      maxResultCount: 100,
      skipCount: 0,
    };
    this.commonService.postRequest('Purchase/fetchPurchaseList', input).subscribe((result) => {
      this.dataSource = result.items;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  getPurchaseReport() {
    this.commonService.getFile('Purchase/getPurchaseReport').subscribe((result) => {
      this.blob = new Blob([result], {type: 'application/xlsx'});
      let downloadURL = window.URL.createObjectURL(result);
      let link = document.createElement('a');
      link.href = downloadURL;
      link.download = "purchase-report.xlsx";
      link.click();
    });
  }

  getCancelledPurchaseReport() {
    this.commonService.getFile('Purchase/getPurchaseCancelReport').subscribe((result) => {
      this.blob = new Blob([result], {type: 'application/xlsx'});
      let downloadURL = window.URL.createObjectURL(result);
      let link = document.createElement('a');
      link.href = downloadURL;
      link.download = "cancelled-purchase-report.xlsx";
      link.click();
    });
  }


  add(): void {
    const dialogRef = this.dialog.open(PurchaseFormComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getPurchaseList();
      }
    });
  }

  edit(editData: any): void {
    const dialogRef = this.dialog.open(PurchaseFormComponent, {
      data: editData,
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getPurchaseList();
      }
    });
  }

  delete(id: any): void {
    if (confirm('Are you sure! Delete this purchase')) {
      this.commonService.deleteRequestWithId('Purchase/delete', id).subscribe((resp) => {
        if (resp) {
          this.openSnackBar('Purchase Deleted Successfully', 'Close');
          this.getPurchaseList();
        }
      });
    }
  }

  cancelPurchase(id: any): void {
    if (confirm('Are you sure! Cancel this purchase')) {
      this.commonService.postRequest(`Purchase/cancelPurchase?id=${id}`, {id}).subscribe((resp) => {
        if (resp) {
          this.openSnackBar('Purchase Cancelled Successfully', 'Close');
          this.getPurchaseList();
        }
      });
    }
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
}
