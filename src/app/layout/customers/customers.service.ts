import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class CustomersService {
    temp: {};
    constructor(private http: Http) { }

    getCustomers() {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/SearchCustomer?pageIndex=0&pageSize=258768',{},{headers: headers});
    }
    updateCustomers(customers, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(customers);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/Update?continueEditing=true', customers[id], { headers: headers });
    }
    deleteCustomers(customers, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(customers);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/Delete?id='+id, null, { headers: headers });
    }


}
