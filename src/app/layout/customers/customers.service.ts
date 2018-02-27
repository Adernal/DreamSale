import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class CustomersService {
    temp: {};
    constructor(private http: Http) { }

    getCustomers(page:number) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/customers',{},{headers: headers});
    }
    updateCustomers(customers, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(customers);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/customers/Update', customers[id], { headers: headers });
    }
    deleteCustomers(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/customers/Delete?id='+id, null, { headers: headers });
    }
    searchCustomers(searchParameters){
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/customers',searchParameters,{headers: headers});
    }
    getCustomerRoles(){
        return this.http.get('http://denmakers3-001-site1.ctempurl.com/api/customers/Roles');

    }

}
