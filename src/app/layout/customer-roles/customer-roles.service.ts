import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class CustomerRoleService {
    temp: {};
    constructor(private http: Http) { }
    storeCustomerRole(customer_role) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = customer_role[customer_role.length-1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/AddCustomerRole?continueEditing=true', this.temp,
            { headers: headers });
}
    getCustomerRole() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/Roles/0/2147483647?pageIdex=0');
    }
    updateCustomerRole(customer_role) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(customer_role);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/EditCustomerRole?continueEditing=true', customer_role, { headers: headers });
    }
    deleteCustomerRole(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/DeleteCustomerRole?id='+id, null, { headers: headers });
    }


}
