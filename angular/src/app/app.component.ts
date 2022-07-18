import {Component, OnInit} from '@angular/core';
import {Customer} from './shared/interfaces/customer';
import {CustomerService} from './shared/services/customer.service';
import {AuthService} from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Home';
  customers: Customer[] = [];

  constructor(private customerService: CustomerService, private authService: AuthService) {
  }

  ngOnInit(): void {
    // this.getCustomers();
  }

  getCustomers(): void {
    this.customerService.getTasks().subscribe(result => {
      this.customers = result;
    });
  }

  get isLoggedIn(): any {
    return JSON.parse(localStorage.getItem('user-details')!);
  }

  logOut(): void {
    this.authService.logOut();
  }
}
