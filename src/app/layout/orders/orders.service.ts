import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class OrderService {
    temp: {};
    Token = localStorage.getItem("Token");
     headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http) { }

    getOrders(page:number) {

        const headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/orders/OrderList',{"Page":page ,"PageSize":10 },{headers:this.headers});
    }
    searchOrders(searchParameters,page:number) {

        console.log(searchParameters);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Orders/OrderList',searchParameters,{headers:this.headers});
    }
    updateOrder(customer_role, id) {

        console.log(customer_role);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Customers/EditOrder', customer_role[id], { headers: this.headers });
    }
    deleteOrder(id) {

        console.log("Id = " + id);
        //console.log(customer_role);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Customers/DeleteOrder?id='+id, null, { headers: this.headers });
    }
    getShippingAddress(orderid){

        return this.http.get('http://denmakers3-001-site1.ctempurl.com/api/Orders/'+orderid+'/BillingAddress',{headers:this.headers});
    }
    changeOrderStatus(orderid,status){

        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Orders/ChangeOrderStatus',{"Id":orderid,"OrderStatusId":status},{headers:this.headers});
    }
    changeShippingStatus(orderid,status){

        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Orders/ChangeOrderStatus',{"Id":orderid,"ShippingStatusId":status},{headers:this.headers});
    }
    changePaymentStatus(orderid,status){

        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Orders/ChangeOrderStatus',{"Id":orderid,"PaymentStatusId":status},{headers:this.headers});
    }

}
