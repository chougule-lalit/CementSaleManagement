import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
import {map} from 'rxjs/operators';
import {Router} from '@angular/router';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = environment.ApiBaseUrl;

  constructor(private http: HttpClient, private router: Router) {
  }

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/UserMaster/login`, {email, password}).pipe(map((resp: any) => {
      if (resp && resp.isSuccess) {
        localStorage.setItem('user-details', JSON.stringify(resp));
      }
      return resp;
    }));
  }

  register(payload: object): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, payload);
  }

  logOut(): void {
    localStorage.removeItem('user-details');
    this.router.navigate(['/login']);
  }

}

