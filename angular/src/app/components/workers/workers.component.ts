import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {FormBuilder} from '@angular/forms';
import {MatSort} from '@angular/material/sort';
import {MatDialog} from '@angular/material/dialog';
import {CommonService} from "../../shared/services/common.service";

@Component({
  selector: 'app-workers',
  templateUrl: './workers.component.html'
})
export class WorkersComponent implements OnInit {

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
  //   const dialogRef = this.dialog.open(WorkersFormComponent);
  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result) {
  //       this.getData();
  //     }
  //   });
  // }
  //
  // edit(editData: any): void {
  //   const dialogRef = this.dialog.open(WorkersFormComponent, {
  //     data: editData,
  //   });
  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result) {
  //       this.getData();
  //     }
  //   });
  // }
  //
  // delete(id: any): void {
  //   this.commonService.deleteRequestWithId('', id).subscribe((data) => {
  //     this.getData();
  //   });
  // }

}
