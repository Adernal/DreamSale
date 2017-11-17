import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class LoginService {
    temp: {};
    constructor(private http: Http) { }
    checkRegister(store) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = store[store.length-1];
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Stores/CreateStore?continueEditing=true', this.temp,
            { headers: headers });

    }
    checkLogin(Credentials) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(Credentials);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Account/Login',Credentials,
        { headers: headers });
    }
   


}
