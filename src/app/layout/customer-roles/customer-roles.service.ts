import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class CustomerRoleService {
    temp: {};
    constructor(private http: Http , private urlService:URLService) { }
    storeCustomerRole(customer_role) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = customer_role[customer_role.length-1];
        return this.http.post(this.urlService.serverUrl+'/customers/CreateCustomerRole', this.temp,
            { headers: headers });
}
    getCustomerRole() {
        return this.http.get(this.urlService.serverUrl+'/customers/Roles');
    }
    updateCustomerRole(customer_role) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(customer_role);
        return this.http.post(this.urlService.serverUrl+'/customers/UpdateCustomerRole', customer_role, { headers: headers });
    }
    deleteCustomerRole(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post(this.urlService.serverUrl+'/customers/DeleteCustomerRole?id='+id, null, { headers: headers });
    }


}
