import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class VendorService {
    temp: {};
    Token = localStorage.getItem("Token");
    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http,private urlService:URLService) { }
    storeVendor(Vendor) {



        return this.http.post(this.urlService.serverUrl+'/Vendors/Create', Vendor,
            { headers: this.headers });

    }
    getVendor() {

        return this.http.post(this.urlService.serverUrl+'/Vendors?showHidden='+true,{},{ headers : this.headers });
    }
    updateVendor(vendor) {

        console.log(vendor);
        return this.http.post(this.urlService.serverUrl+'/Vendors/Update', vendor, { headers: this.headers });
    }
    deleteVendor(id) {


        return this.http.post(this.urlService.serverUrl+'/Vendors/Delete?id='+id, null, { headers: this.headers });
    }


}
