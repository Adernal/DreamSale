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
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/BestsellersReportList',null,{headers:this.headers});
    }


}
