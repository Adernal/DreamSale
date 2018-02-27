import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ProductAttributesService {
    temp: {};
    constructor(private http: Http) { }
    storeAttributes(attributes) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = attributes[attributes.length - 1];
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/ProductAttribute', this.temp,
            { headers: headers });

    }
    getAttributes() {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/ProductAttribute',{
            'Page': 1,
            'PageSize': 300
          },{headers:headers});
    }
    updateAttributes(attributes, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(attributes);
        // tslint:disable-next-line:max-line-length
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/ProductAttribute/Update?continueEditing=true', attributes[id], { headers: headers });
    }
    deleteAttributes(attributes, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(attributes);

        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/ProductAttribute/Delete/' + id, null, { headers: headers });
    }


}
