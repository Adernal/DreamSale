import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ReturnRequestService {
    temp: {};
    Token = localStorage.getItem("Token");
     headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http) { }
    storeReturnRequest(store) {
      
        this.temp = store[store.length-1];
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/ReturnRequest/CreateStore?continueEditing=true', this.temp,
            { headers: this.headers });

    }
    getReturnRequest() {
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/ReturnRequest',{},{headers:this.headers});
    }
    updateReturnRequest(store) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(store);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/ReturnRequest/EditStore?continueEditing=true', store, { headers: headers });
    }
    deleteReturnRequest(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        //console.log(store);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/ReturnRequest/DeleteStore?id='+id, null, { headers: headers });
    }


}
