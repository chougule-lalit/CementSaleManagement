import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {FormBuilder} from '@angular/forms';
import {MatSort} from '@angular/material/sort';
import {MatDialog} from '@angular/material/dialog';
import {CommonService} from "../../shared/services/common.service";

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html'
})
export class SupplierComponent implements OnInit {

  displayedColumns = ['id', 'firstName', 'lastName', 'email', 'phone', 'role'];
  dataSource: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private commonService: CommonService, private fb: FormBuilder, public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getData();
  }

  getData(): void {
    const input = {
      maxResultCount: 100,
      skipCount: 0,
      roleId: JSON.parse(localStorage.getItem('user-details')!).role
    };

    this.commonService.postRequest('UserMaster/fetchUserList', input).subscribe((result: any) => {
      this.dataSource = result.items;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  // add(): void {
  //   const dialogRef = this.dialog.open(SupplierFormComponent);
  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result) {
  //       this.getUserList();
  //     }
  //   });
  // }
  //
  // edit(editData: any): void {
  //   const dialogRef = this.dialog.open(SupplierFormComponent, {
  //     data: editData,
  //   });
  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result) {
  //       this.getUserList();
  //     }
  //   });
  // }
  //
  // delete(id: any): void {
  //   this.commonService.postRequest('', id).subscribe((data) => {
  //     this.getUserList();
  //   });
  // }
}
