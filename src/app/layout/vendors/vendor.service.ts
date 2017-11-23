import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class VendorService {
    temp: {};
    Token = localStorage.getItem("Token");
    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http) { }
    storeVendor(Vendor) {
        
      

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors/Create', Vendor,
            { headers: this.headers });

    }
    getVendor() {
        
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors?showHidden='+true,{},{ headers : this.headers });
    }
    updateVendor(vendor) {
        
        console.log(vendor);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors/Update', vendor, { headers: this.headers });
    }
    deleteVendor(id) {
        

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors/Delete?id='+id, null, { headers: this.headers });
    }


}
