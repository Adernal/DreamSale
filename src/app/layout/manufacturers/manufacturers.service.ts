import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ManufacturersService {
    temp: {};
    constructor(private http: Http) { }
    storeManufacturers(manufacturer) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        //console.log(manufacturer);
        this.temp = manufacturer[0];
        console.log(this.temp);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Manufacturers/Add?continueEditing=true',this.temp,
            { headers: headers });
    }
    getManufacturers() {
        return this.http.get('http://denmakers-001-site1.itempurl.com/api/Manufacturers');
    }
    updateManufacturer(manufacturer) {
        const headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Manufacturers/Update?continueEditing=true', manufacturer, { headers: headers });
    }
    deleteManufacturer(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Manufacturers/Delete?id=' + id, null, { headers: headers });
    }

}
