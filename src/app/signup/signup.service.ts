import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class SignupService {
    temp: {};
    constructor(private http: Http) { }

    checkSignup(Credentials) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(Credentials);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Account/Register',Credentials,
        { headers: headers });
    }



}
