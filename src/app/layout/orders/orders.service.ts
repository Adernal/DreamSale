import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class OrderService {
    temp: {};
    constructor(private http: Http) { }
//     storeOrder(customer_role) {
//         const headers = new Headers({ 'Content-Type': 'application/json' });
//         this.temp = customer_role[customer_role.length-1];
//         return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/AddOrder?continueEditing=true', this.temp,
//             { headers: headers });
// }
    getOrder() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/Roles/0/2147483647?pageIdex=0');
    }
    updateOrder(customer_role, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(customer_role);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/EditOrder', customer_role[id], { headers: headers });
    }
    deleteOrder(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        //console.log(customer_role);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Customers/DeleteOrder?id='+id, null, { headers: headers });
    }


}