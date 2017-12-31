import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class LoginService {
    temp: {};
    constructor(private http: Http) { }
  
    checkLogin(Credentials) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(Credentials);
        return this.http.post('//denmakers2-001-site1.gtempurl.com/api/Account/Login',Credentials,
        { headers: headers });
    }
   


}
