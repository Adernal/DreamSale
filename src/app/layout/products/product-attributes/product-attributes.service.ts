import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../../shared/services';

@Injectable()
export class ProductAttributesService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) { }
    storeAttributes(attributes) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        this.temp = attributes[attributes.length - 1];
        return this.http.post(this.urlService.serverUrl+'/ProductAttribute', this.temp,
            { headers: headers });

    }
    getAttributes() {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        return this.http.post(this.urlService.serverUrl+'/ProductAttribute',{
            'Page': 1,
            'PageSize': 300
          },{headers:headers});
    }
    updateAttributes(attributes, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        console.log(attributes);
        // tslint:disable-next-line:max-line-length
        return this.http.post(this.urlService.serverUrl+'/ProductAttribute/Update?continueEditing=true', attributes[id], { headers: headers });
    }
    deleteAttributes(attributes, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        console.log("Id = " + id);
        console.log(attributes);

        return this.http.post(this.urlService.serverUrl+'/ProductAttribute/Delete/' + id, null, { headers: headers });
    }


}
