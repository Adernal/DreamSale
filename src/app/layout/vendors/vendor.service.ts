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

        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/vendors/CreateVendor', this.temp,
            { headers: headers });

    }
    getVendor() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors');
    }
    updateVendor(vendor) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(vendor);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/vendors/EditVendor', vendor, { headers: headers });
    }
    deleteVendor(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors/Delete?id='+id, null, { headers: headers });
    }


}
