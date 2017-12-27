import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class NeverSoldService {
    temp: {};
    Token = localStorage.getItem("Token");
    
    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http) { }

    getNeverSold() {
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/NeverSoldReportList',{Page:1,PageSize:100},{headers:this.headers});
    }

    getCategory() {

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/categories', {}, { headers: this.headers });
    }
    getManufacturers(){
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Manufacturers', {}, {headers: this.headers});
    }
    getStores(){
          return this.http.get('http://denmakers-001-site1.itempurl.com/api/Stores');
    }
    getVendors(){
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors?showHidden=true', {}, {headers: this.headers});
    }
    searchNeverSoldItems(searchParameters) {
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/NeverSoldReportList',searchParameters,{headers:this.headers});
    }


}
