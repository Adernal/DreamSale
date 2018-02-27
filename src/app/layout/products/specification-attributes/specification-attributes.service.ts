import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class SpecificationAttributesService {
    temp: {};
    headers = new Headers({ 'Content-Type': 'application/json' });
    constructor(private http: Http) { }
    storeAttributes(attributes) {

        this.temp = attributes[attributes.length - 1];
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/SpecificationAttribute/Create', this.temp,
            { headers: this.headers });

    }
    getAttributes() {
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/SpecificationAttribute', {
            'Page': 1,
            'PageSize': 300
          }, {headers: this.headers});
    }
    updateAttributes(attributes, id) {

        console.log(attributes);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/SpecificationAttribute/Update',
         attributes[id],
          { headers: this.headers });
    }
    deleteAttributes(attributes, id) {

        console.log('Id = ' + id);


        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/SpecificationAttribute/Delete?id=' + id, null,
         { headers: this.headers });
    }


}
