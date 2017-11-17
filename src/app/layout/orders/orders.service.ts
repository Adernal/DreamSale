import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class OrderService {
    temp: {};
    constructor(private http: Http) { }

    getOrders(page:number) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/OrderList?pageIndex='+page+'&pageSize=10',{},{headers:headers});
    }
    searchOrders(order,page:number) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(order[0]);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/OrderList?pageIndex='+page+'&pageSize=10',order[0],{headers:headers});
    }
    updateOrder(customer_role, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(customer_role);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Customers/EditOrder', customer_role[id], { headers: headers });
    }
    deleteOrder(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        //console.log(customer_role);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Customers/DeleteOrder?id='+id, null, { headers: headers });
    }


}
