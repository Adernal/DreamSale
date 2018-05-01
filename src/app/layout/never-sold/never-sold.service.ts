import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class NeverSoldService {
    temp: {};
    Token = localStorage.getItem("Token");

    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http,private urlService:URLService) { }

    getNeverSold() {
        return this.http.post(this.urlService.serverUrl+'/Orders/NeverSoldReportList',{Page:1,PageSize:100},{headers:this.headers});
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
    searchNeverSoldItems(searchParameters) {
        return this.http.post(this.urlService.serverUrl+'/Orders/NeverSoldReportList',searchParameters,{headers:this.headers});
    }


}
