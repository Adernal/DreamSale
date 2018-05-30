import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class StoresService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) { }
    storeStores(store) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        this.temp = store[store.length-1];
        return this.http.post(this.urlService.serverUrl+'/Stores/CreateStore?continueEditing=true', this.temp,
            { headers: headers });

    }
    getStores() {
        return this.http.get(this.urlService.serverUrl+'/Stores');
    }
    updateStores(store) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        console.log(store);
        return this.http.post(this.urlService.serverUrl+'/Stores/EditStore?continueEditing=true', store, { headers: headers });
    }
    deleteStores(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        console.log("Id = " + id);
        //console.log(store);
        return this.http.post(this.urlService.serverUrl+'/Stores/DeleteStore?id='+id, null, { headers: headers });
    }


}
