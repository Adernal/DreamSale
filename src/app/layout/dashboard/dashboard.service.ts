import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class DashboardService {
    temp: {};
    Token = localStorage.getItem("Token");
    headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() ,'Accept':'application/json',});
    constructor(private http: Http,private urlService:URLService) { }

    getOrders(){


        return this.http.post(this.urlService.serverUrl+'/orders/OrderList',{"Page":1 ,"PageSize":10 },{headers:this.headers})
}
getCustomers() {

    return this.http.post(this.urlService.serverUrl+'/customers',{
        "SearchCustomerRoleIds": [
            1,
            2]
    },{headers: this.headers});
}
getLowStockReport(){

    return this.http.post(this.urlService.serverUrl+'/Products/LowStockReport',{"Page":1,"PageSize":1},{headers: this.headers});
}
getReturnRequest(){

    return this.http.post(this.urlService.serverUrl+'/ReturnRequest',{
        "Command": {
        "Page": 1,
        "PageSize": 2
      }
    },{headers: this.headers});
}
getBestSeller(){
    return this.http.post(this.urlService.serverUrl+'/Orders/BestsellersReportList',{Page:1,PageSize:10},{headers:this.headers});
}
// getLatestOrders(){
//     return this.http.post('/ReturnRequest',{
//         "Command": {
//         "Page": 1,
//         "PageSize": 2
//       }
//     },{headers: this.headers});

// }
}




