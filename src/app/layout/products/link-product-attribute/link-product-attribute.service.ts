import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class LinkProductAttributeService {
    temp: {};
    constructor(private http: Http) { }
    getAttributes() {
      return this.http.get('http://denmakers-001-site1.itempurl.com/api/ProductAttribute/0/2147483647');
  }
  getSpecAttributes() {
    return this.http.get('http://denmakers-001-site1.itempurl.com/api/SpecificationAttribute/0/2147483647');
}


}
