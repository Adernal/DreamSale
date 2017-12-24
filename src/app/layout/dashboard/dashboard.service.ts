import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class DashboardService {
    temp: {};
    Token = localStorage.getItem("Token");
    constructor(private http: Http) { }

    getOrders(){
       
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/orders/OrderList',{"Page":1 ,"PageSize":1 },{headers:headers})
}
getCustomers() {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    return this.http.post('http://denmakers-001-site1.itempurl.com/api/customers',{
        "SearchCustomerRoleIds": [
            1,
            2]
    },{headers: headers});
}
getLowStockReport(){
    const headers = new Headers({ 'Content-Type': 'application/json' });
    return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products/LowStockReport',{"Page":1,"PageSize":1},{headers: headers});
}
getReturnRequest(){
    const headers = new Headers({ 'Content-Type': 'application/json' });
    return this.http.post('http://denmakers-001-site1.itempurl.com/api/ReturnRequest',{
        "Command": {
        "Page": 1,
        "PageSize": 2
      }
    },{headers: headers});
}
}




