import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class NeverSoldService {
    temp: {};
    constructor(private http: Http) { }
    NeverSoldtores(store) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = store[store.length-1];
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/NeverSold/CreateStore?continueEditing=true', this.temp,
            { headers: headers });

    }
    getNeverSold() {
        return this.http.get('http://denmakers-001-site1.itempurl.com/api/NeverSold');
    }
    updateNeverSold(store) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(store);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/NeverSold/EditStore?continueEditing=true', store, { headers: headers });
    }
    deleteNeverSold(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        //console.log(store);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/NeverSold/DeleteStore?id='+id, null, { headers: headers });
    }


}
