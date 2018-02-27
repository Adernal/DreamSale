import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class BestSellerService {
    temp: {};
    Token = localStorage.getItem("Token");

    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http) { }

    getBestSeller() {
        console.log(this.Token);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Orders/BestsellersReportList',{Page:1,PageSize:50000},{headers:this.headers});
    }
    getCategory() {

        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/categories', {}, { headers: this.headers });
    }
    getManufacturers(){
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Manufacturers', {}, {headers: this.headers});
    }
    getStores(){
          return this.http.get('http://denmakers3-001-site1.ctempurl.com/api/Stores');
    }
    getVendors(){
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Vendors?showHidden=true', {}, {headers: this.headers});
    }
    searchBestSellerItems(searchParameters) {
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Orders/BestsellersReportList',searchParameters,{headers:this.headers});
    }


}
