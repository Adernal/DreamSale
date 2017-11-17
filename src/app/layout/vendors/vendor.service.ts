import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class VendorService {
    temp: {};
    constructor(private http: Http) { }
    storeVendor(Vendor) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = Vendor[Vendor.length-1];
        console.log(this.temp);

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors/Create', this.temp,
            { headers: headers });

    }
    getVendor() {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors?showHidden='+true,{},{ headers : headers });
    }
    updateVendor(vendor) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(vendor);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors/Update', vendor, { headers: headers });
    }
    deleteVendor(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors/Delete?id='+id, null, { headers: headers });
    }


}
