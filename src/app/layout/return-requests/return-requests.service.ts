import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ReturnRequestService {
    temp: {};
    constructor(private http: Http) { }
    storeReturnRequest(store) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = store[store.length-1];
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/ReturnRequest/CreateStore?continueEditing=true', this.temp,
            { headers: headers });

    }
    getReturnRequest() {
        return this.http.get('http://denmakers-001-site1.itempurl.com/api/ReturnRequest');
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
