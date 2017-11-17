import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class DashboardService {
    temp: {};
    constructor(private http: Http) { }

    getOrders(){
        return this.http.get('http://denmakers-001-site1.itempurl.com/api/Orders/Reports/ByQuantity/0/25878');
}
getCustomers() {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    return this.http.post('http://denmakers-001-site1.itempurl.com/api/Customers/SearchCustomer?pageIndex=0&pageSize=25878',{},{headers: headers});
}



}
