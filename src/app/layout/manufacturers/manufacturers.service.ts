import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class ManufacturersService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) { }
    storeManufacturers(manufacturer) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        //console.log(manufacturer);
        this.temp = manufacturer[0];
        console.log(this.temp);
        return this.http.post(this.urlService.serverUrl+'/Manufacturers/Create',this.temp,
            { headers: headers });
    }
    getManufacturers() {
        return this.http.post(this.urlService.serverUrl+'/Manufacturers',{});
    }
    updateManufacturer(manufacturer) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });

        return this.http.post(this.urlService.serverUrl+'/Manufacturers/Update', manufacturer, { headers: headers });
    }
    deleteManufacturer(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        return this.http.post(this.urlService.serverUrl+'/Manufacturers/Delete?id=' + id, null, { headers: headers });
    }

}
