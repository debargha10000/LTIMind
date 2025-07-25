import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}
  public createNewCustomer = (data: any): Observable<any> => {
    return this.http.post('http://localhost:5073/user/', data, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
  };
}
