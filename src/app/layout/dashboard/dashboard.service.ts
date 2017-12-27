import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class DashboardService {
    temp: {};
    Token = localStorage.getItem("Token");
    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http) { }

    getOrders(){
       
        
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/orders/OrderList',{"Page":1 ,"PageSize":10 },{headers:this.headers})
}
getCustomers() {
  
    return this.http.post('http://denmakers-001-site1.itempurl.com/api/customers',{
        "SearchCustomerRoleIds": [
            1,
            2]
    },{headers: this.headers});
}
getLowStockReport(){
    
    return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products/LowStockReport',{"Page":1,"PageSize":1},{headers: this.headers});
}
getReturnRequest(){
   
    return this.http.post('http://denmakers-001-site1.itempurl.com/api/ReturnRequest',{
        "Command": {
        "Page": 1,
        "PageSize": 2
      }
    },{headers: this.headers});
}
getBestSeller(){
    return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/BestsellersReportList',{Page:1,PageSize:10},{headers:this.headers});
}
// getLatestOrders(){
//     return this.http.post('http://denmakers-001-site1.itempurl.com/api/ReturnRequest',{
//         "Command": {
//         "Page": 1,
//         "PageSize": 2
//       }
//     },{headers: this.headers});

// }
}




