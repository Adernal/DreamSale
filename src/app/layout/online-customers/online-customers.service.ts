import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class OnlineCustomersService {
    temp: {};
    constructor(private http: Http) { }

    getOnlineCustomers() {
        return this.http.get('http://denmakers2-001-site1.gtempurl.com/api/onlineCustomers/0/2147483647');
    }


}
