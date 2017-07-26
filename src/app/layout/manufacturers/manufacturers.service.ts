import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ManufacturersService {
    temp: {};
    constructor(private http: Http) { }
    storeManufacturers(manufacturers) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(manufacturers);
        this.temp = manufacturers[manufacturers.length - 1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers/Add?continueEditing=true',this.temp,
            { headers: headers });
    }
    getManufacturers() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers');
    }
    updateManufacturers(manufacturers, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id :" + id);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers/Update?continueEditing=true', manufacturers[id], { headers: headers });
    }
    deleteAttributes(manufacturers, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(manufacturers);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers/Delete?id=' + id, null, { headers: headers });
    }

}
