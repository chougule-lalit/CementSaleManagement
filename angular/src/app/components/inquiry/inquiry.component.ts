import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {CommonService} from 'src/app/shared/services/common.service';
import {CreateAndUpdateModalComponent} from './create-and-update-modal/create-and-update-modal.component';

@Component({
  selector: 'app-inquiry',
  templateUrl: './inquiry.component.html'
})
export class InquiryComponent implements OnInit {
  displayedColumns = ['id', 'firstName', 'lastName', 'email', 'phone', 'actions'];
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
    };
    const data = [
      {
        id: 1,
        firstName: 'Yash',
        lastName: 'Dada',
        email: 'yash@gmail.com',
        phone: '9535089694',
      },
      {
        id: 2,
        firstName: 'Naval Chinmay',
        lastName: 'Jhaveri',
        email: 'naval@gmail.com',
        phone: '9876576694',
      }
    ];
    this.dataSource = data;
    this.commonService.postRequest('Enquiry/fetchEnquiryList', input).subscribe((result) => {
      this.dataSource = result.items;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  add(): void {
    const dialogRef = this.dialog.open(CreateAndUpdateModalComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  edit(editData: any): void {
    const dialogRef = this.dialog.open(CreateAndUpdateModalComponent, {
      data: editData,
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  delete(id: any): void {
    this.commonService.deleteRequestWithId('Enquiry/delete', id).subscribe((data) => {
      this.getData();
    });
  }

}
