import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class OrderService {
    temp: {};
    Token = localStorage.getItem("Token");
     headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() ,'Accept':'application/json',});
    constructor(private http: Http,private urlService:URLService) { }

    getOrders(page:number) {

        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() ,'Accept':'application/json',});
        return this.http.post(this.urlService.serverUrl+'/orders/OrderList',{"Page":page ,"PageSize":10 },{headers:this.headers});
    }
    searchOrders(searchParameters,page:number) {

        console.log(searchParameters);
        return this.http.post(this.urlService.serverUrl+'/Orders/OrderList',searchParameters,{headers:this.headers});
    }
    updateOrder(customer_role, id) {

        console.log(customer_role);
        return this.http.post(this.urlService.serverUrl+'/Customers/EditOrder', customer_role[id], { headers: this.headers });
    }
    deleteOrder(id) {

        console.log("Id = " + id);
        //console.log(customer_role);
        return this.http.post(this.urlService.serverUrl+'/Customers/DeleteOrder?id='+id, null, { headers: this.headers });
    }
    getShippingAddress(orderid){

        return this.http.get(this.urlService.serverUrl+'/Orders/'+orderid+'/BillingAddress',{headers:this.headers});
    }
    changeOrderStatus(orderid,status){

        return this.http.post(this.urlService.serverUrl+'/Orders/ChangeOrderStatus',{"Id":orderid,"OrderStatusId":status},{headers:this.headers});
    }
    changeShippingStatus(orderid,status){

        return this.http.post(this.urlService.serverUrl+'/Orders/ChangeOrderStatus',{"Id":orderid,"ShippingStatusId":status},{headers:this.headers});
    }
    changePaymentStatus(orderid,status){

        return this.http.post(this.urlService.serverUrl+'/Orders/ChangeOrderStatus',{"Id":orderid,"PaymentStatusId":status},{headers:this.headers});
    }

}
