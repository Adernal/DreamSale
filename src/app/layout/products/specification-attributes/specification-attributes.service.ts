import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class SpecificationAttributesService {
    temp: {};
    constructor(private http: Http) { }
    storeAttributes(attributes) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = attributes[attributes.length-1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/Add?continueEditing=true', this.temp,
            { headers: headers });

    }
    getAttributes() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/0/2147483647');
    }
    updateAttributes(attributes, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(attributes);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/Update?continueEditing=true', attributes[id], { headers: headers });
    }
    deleteAttributes(attributes, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(attributes);

        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/Delete/'+id, null, { headers: headers });
    }


}
