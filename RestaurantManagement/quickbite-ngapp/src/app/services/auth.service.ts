import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _isLoggedIn = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this._isLoggedIn.asObservable();

  constructor(private http: HttpClient) {
    this._isLoggedIn.next(localStorage.getItem('loginstatus') === 'true');
  }

  public loginUser = (data: any): Observable<any> => {
    const res: Observable<any> = this.http.post(
      'http://localhost:5073/user/auth/login',
      data,
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    );
    this._isLoggedIn.next(true);
    return res;
    // localStorage.setItem('loginstatus', 'true');
  };

  public logoutUser = (): Observable<any> => {
    const sessionData = JSON.parse(localStorage.getItem('session') || '{}');
    console.log('sessionData', sessionData);
    const res: Observable<any> = this.http.post(
      'http://localhost:5073/user/auth/logout',
      { refreshToken: sessionData.refreshToken },
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    );
    this._isLoggedIn.next(false);
    // localStorage.setItem('loginstatus', 'false');
    return res;
  };
}
