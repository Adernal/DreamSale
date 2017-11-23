import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class OrderService {
    temp: {};
    Token = localStorage.getItem("Token");
    
    constructor(private http: Http) { }

    getOrders(page:number) {
       
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/orders/OrderList',{"Page":page ,"PageSize":10 },{headers:headers});
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
    getShippingAddress(orderid){
        const headers = new Headers({ 'Content-Type': 'application/json','Authorization':'Token '+this.Token });
        return this.http.get('http://denmakers-001-site1.itempurl.com/api/Orders/'+orderid+'/BillingAddress',{headers:headers});
    }
    changeOrderStatus(orderid,status){
        const headers = new Headers({ 'Content-Type': 'application/json','Authorization':'Token '+this.Token });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/ChangeOrderStatus',{"Id":orderid,"OrderStatusId":status},{headers:headers});
    }
    changeShippingStatus(orderid,status){
        const headers = new Headers({ 'Content-Type': 'application/json','Authorization':'Token '+this.Token });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/ChangeOrderStatus',{"Id":orderid,"ShippingStatusId":status},{headers:headers});
    }
    changePaymentStatus(orderid,status){
        const headers = new Headers({ 'Content-Type': 'application/json','Authorization':'Token '+this.Token });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/ChangeOrderStatus',{"Id":orderid,"PaymentStatusId":status},{headers:headers});
    }

}
