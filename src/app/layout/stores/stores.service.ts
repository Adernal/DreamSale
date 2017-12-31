import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class StoresService {
    temp: {};
    constructor(private http: Http) { }
    storeStores(store) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = store[store.length-1];
        return this.http.post('http://denmakers2-001-site1.gtempurl.com/api/Stores/CreateStore?continueEditing=true', this.temp,
            { headers: headers });

    }
    getStores() {
        return this.http.get('http://denmakers2-001-site1.gtempurl.com/api/Stores');
    }
    updateStores(store) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(store);
        return this.http.post('http://denmakers2-001-site1.gtempurl.com/api/Stores/EditStore?continueEditing=true', store, { headers: headers });
    }
    deleteStores(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        //console.log(store);
        return this.http.post('http://denmakers2-001-site1.gtempurl.com/api/Stores/DeleteStore?id='+id, null, { headers: headers });
    }


}
