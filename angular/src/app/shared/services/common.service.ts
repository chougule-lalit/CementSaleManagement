import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  private apiUrl = environment.ApiBaseUrl;

  constructor(private http: HttpClient) {
  }

  fetchUserList(payload: any): Observable<any> {
    return this.http.post(this.apiUrl + '/UserMaster/fetchUserList', payload);
  }

  createOrUpdateUser(payload: any): Observable<any> {
    return this.http.post(this.apiUrl + '/UserMaster/createOrUpdate', payload);
  }

  deleteUser(id: any): Observable<any> {
    return this.http.delete(this.apiUrl + '/UserMaster/delete/' + id);
  }

  postRequest(apiUrlEndPoint: string, payload: any): Observable<any> {
    return this.http.post(this.apiUrl + '/' + apiUrlEndPoint, payload);
  }

  deleteRequestWithId(apiUrlEndPoint: string, id: any): Observable<any> {
    return this.http.delete(this.apiUrl + '/' + apiUrlEndPoint + '/' + id);
  }
  deleteRequestWithParams(apiUrlEndPoint: string, id: any): Observable<any> {
    return this.http.delete(this.apiUrl + '/' + apiUrlEndPoint, { params: { id: id } });
  }

  getRequest(apiUrlEndPoint: string): Observable<any> {
    return this.http.get(this.apiUrl + '/' + apiUrlEndPoint);
  }

  getRequestWithId(apiUrlEndPoint: string, id: number): Observable<any> {
    return this.http.get(this.apiUrl + '/' + apiUrlEndPoint + '/' + id);
  }

  getRoleDropdown(): Observable<any> {
    return this.http.get(this.apiUrl + '/RoleMaster/getRoleDropdown');
  }
}
