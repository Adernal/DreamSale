import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../shared/services';

@Injectable()
export class SignupService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) { }

    checkSignup(Credentials) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        console.log(Credentials);
        return this.http.post(this.urlService.serverUrl+'/Account/Register',Credentials,
        { headers: headers });
    }



}
