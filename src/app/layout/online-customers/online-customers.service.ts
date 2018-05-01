import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class OnlineCustomersService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) { }

    getOnlineCustomers() {
        return this.http.get(this.urlService.serverUrl+'/onlineCustomers/0/2147483647');
    }


}
