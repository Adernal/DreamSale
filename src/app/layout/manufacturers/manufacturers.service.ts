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
        return this.http.post('http://denmakers2-001-site1.gtempurl.com/api/Manufacturers/Create',this.temp,
            { headers: headers });
    }
    getManufacturers() {
        return this.http.post('http://denmakers2-001-site1.gtempurl.com/api/Manufacturers',{});
    }
    updateManufacturer(manufacturer) {
        const headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.post('http://denmakers2-001-site1.gtempurl.com/api/Manufacturers/Update', manufacturer, { headers: headers });
    }
    deleteManufacturer(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers2-001-site1.gtempurl.com/api/Manufacturers/Delete?id=' + id, null, { headers: headers });
    }

}
