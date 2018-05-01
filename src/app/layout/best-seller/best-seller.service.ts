import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class BestSellerService {
    temp: {};
    Token = localStorage.getItem("Token");

    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http,private urlService:URLService) { }

    getBestSeller() {
        console.log(this.Token);
        return this.http.post(this.urlService.serverUrl+'/Orders/BestsellersReportList',{Page:1,PageSize:50000},{headers:this.headers});
    }
    getCategory() {

        return this.http.post(this.urlService.serverUrl+'/categories', {}, { headers: this.headers });
    }
    getManufacturers(){
        return this.http.post(this.urlService.serverUrl+'/Manufacturers', {}, {headers: this.headers});
    }
    getStores(){
          return this.http.get(this.urlService.serverUrl+'/Stores');
    }
    getVendors(){
        return this.http.post(this.urlService.serverUrl+'/Vendors?showHidden=true', {}, {headers: this.headers});
    }
    searchBestSellerItems(searchParameters) {
        return this.http.post(this.urlService.serverUrl+'/Orders/BestsellersReportList',searchParameters,{headers:this.headers});
    }


}
