import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class VendorService {
    temp: {};
    constructor(private http: Http) { }
    storeVendor(Vendor) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = Vendor[Vendor.length-1];
        
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Vendor/CreateVendor?continueEditing=true', this.temp,
            { headers: headers });

    }
    getVendor() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors');
    }
    updateVendor(Vendor, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(Vendor);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors/Update?continueEditing=true', Vendor[id], { headers: headers });
    }
    deleteVendor(Vendor, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(Vendor);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors/Delete?id='+id, null, { headers: headers });
    }


}
