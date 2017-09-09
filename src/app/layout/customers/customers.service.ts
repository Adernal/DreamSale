import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class CustomersService {
    temp: {};
    constructor(private http: Http) { }

    getCustomers(page:number) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/SearchCustomer?pageIndex='+page+'&pageSize=10',{},{headers: headers});
    }
    updateCustomers(customers, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(customers);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/Update?continueEditing=true', customers[id], { headers: headers });
    }
    deleteCustomers(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/DeleteCustomer?id='+id, null, { headers: headers });
    }
    searchCustomers(searchParameters){
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/SearchCustomer?pageIndex=0&pageSize=25878',searchParameters,{headers: headers});
    }


}
