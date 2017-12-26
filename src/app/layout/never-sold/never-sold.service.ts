import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class NeverSoldService {
    temp: {};
    Token = localStorage.getItem("Token");
    
    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http) { }

    getNeverSold() {
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Orders/NeverSoldReportList',{},{headers:this.headers});
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
