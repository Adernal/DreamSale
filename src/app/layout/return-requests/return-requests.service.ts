import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class ReturnRequestService {
    temp: {};
    Token = localStorage.getItem("Token");
    headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() ,'Accept':'application/json',});
    constructor(private http: Http,private urlService:URLService) { }

    getReturnRequest() {
        return this.http.post(this.urlService.serverUrl+'/ReturnRequest',{},{headers:this.headers});
    }
    updateReturnRequest(searchParameters) {


        return this.http.post(this.urlService.serverUrl+'/ReturnRequest/Update', searchParameters, { headers: this.headers });
    }
    deleteReturnRequest(id) {

        console.log("Id = " + id);
        //console.log(store);
        return this.http.post(this.urlService.serverUrl+'/ReturnRequest/Delete?id='+id, null, { headers: this.headers });
    }
    searchReturnRequest(searchParameters) {
        return this.http.post(this.urlService.serverUrl+'/ReturnRequest',searchParameters,{headers:this.headers});
    }

}
