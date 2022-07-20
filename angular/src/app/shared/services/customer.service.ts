import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from '../../../environments/environment';
import { Customer } from '../interfaces/customer';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrl = environment.ApiBaseUrl;

  constructor(private http: HttpClient) { }

  getTasks(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.apiUrl}/Customer/getAllCustomers`, httpOptions);
  }
}
