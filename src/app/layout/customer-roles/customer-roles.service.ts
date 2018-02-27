import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class CustomerRoleService {
    temp: {};
    constructor(private http: Http) { }
    storeCustomerRole(customer_role) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = customer_role[customer_role.length-1];
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/customers/CreateCustomerRole', this.temp,
            { headers: headers });
}
    getCustomerRole() {
        return this.http.get('http://denmakers3-001-site1.ctempurl.com/api/customers/Roles');
    }
    updateCustomerRole(customer_role) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(customer_role);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/customers/UpdateCustomerRole', customer_role, { headers: headers });
    }
    deleteCustomerRole(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/customers/DeleteCustomerRole?id='+id, null, { headers: headers });
    }


}
