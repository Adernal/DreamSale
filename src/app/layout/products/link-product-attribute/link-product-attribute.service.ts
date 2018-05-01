import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../../shared/services';

@Injectable()
export class LinkProductAttributeService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) { }
    getAttributes() {
      return this.http.get(this.urlService.serverUrl+'/ProductAttribute/0/2147483647');
  }
  getSpecAttributes() {
    return this.http.get(this.urlService.serverUrl+'/SpecificationAttribute/0/2147483647');
}


}
