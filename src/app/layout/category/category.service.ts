import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class CategoryService {
    temp: {};
    Token = localStorage.getItem("Token");
    constructor(private http: Http,private urlService:URLService) { }
    storeCategory(category) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        this.temp = category[category.length-1];
        return this.http.post(this.urlService.serverUrl+'/Categories/Create', this.temp,
            { headers: headers });
    }
    getCategory(page) {
        return this.http.post(this.urlService.serverUrl+'/Categories',{"Page":1 ,"PageSize":5000});
    }
    updateCategory(category) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        return this.http.post(this.urlService.serverUrl+'/Categories/Update', category, { headers: headers });
    }
    deleteCategory(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        return this.http.post(this.urlService.serverUrl+'/Categories/Delete?id='+id, null, { headers: headers });
    }


}
