import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../shared/services';

@Injectable()
export class LoginService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) {

     }

    checkLogin(Credentials) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(Credentials);
        return this.http.post(this.urlService.serverUrl+'/Account/Login',Credentials,
        { headers: headers });
    }



}
