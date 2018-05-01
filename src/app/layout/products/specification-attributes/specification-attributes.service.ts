import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../../shared/services';

@Injectable()
export class SpecificationAttributesService {
    temp: {};
    headers = new Headers({ 'Content-Type': 'application/json' });
    constructor(private http: Http,private urlService:URLService) { }
    storeAttributes(attributes) {

        this.temp = attributes[attributes.length - 1];
        return this.http.post(this.urlService.serverUrl+'/SpecificationAttribute/Create', this.temp,
            { headers: this.headers });

    }
    getAttributes() {
        return this.http.post(this.urlService.serverUrl+'/SpecificationAttribute', {
            'Page': 1,
            'PageSize': 300
          }, {headers: this.headers});
    }
    updateAttributes(attributes, id) {

        console.log(attributes);
        return this.http.post(this.urlService.serverUrl+'/SpecificationAttribute/Update',
         attributes[id],
          { headers: this.headers });
    }
    deleteAttributes(attributes, id) {

        console.log('Id = ' + id);


        return this.http.post(this.urlService.serverUrl+'/SpecificationAttribute/Delete?id=' + id, null,
         { headers: this.headers });
    }


}
