import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';

@Injectable()
export class CustomersService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) { }

    getCustomers(page:number) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        return this.http.post(this.urlService.serverUrl+'/customers',{},{headers: headers});
    }
    updateCustomers(customers, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        console.log(customers);
        return this.http.post(this.urlService.serverUrl+'/customers/Update', customers[id], { headers: headers });
    }
    deleteCustomers(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });

        return this.http.post(this.urlService.serverUrl+'/customers/Delete?id='+id, null, { headers: headers });
    }
    searchCustomers(searchParameters){
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        return this.http.post(this.urlService.serverUrl+'/customers',searchParameters,{headers: headers});
    }
    getCustomerRoles(){
        return this.http.get(this.urlService.serverUrl+'/customers/Roles');

    }

}
