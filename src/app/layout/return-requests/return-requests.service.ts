import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ReturnRequestService {
    temp: {};
    Token = localStorage.getItem("Token");
    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http) { }

    getReturnRequest() {
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/ReturnRequest',{},{headers:this.headers});
    }
    updateReturnRequest(searchParameters) {


        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/ReturnRequest/Update', searchParameters, { headers: this.headers });
    }
    deleteReturnRequest(id) {

        console.log("Id = " + id);
        //console.log(store);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/ReturnRequest/Delete?id='+id, null, { headers: this.headers });
    }
    searchReturnRequest(searchParameters) {
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/ReturnRequest',searchParameters,{headers:this.headers});
    }

}
