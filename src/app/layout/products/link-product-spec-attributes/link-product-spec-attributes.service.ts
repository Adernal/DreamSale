/*Services for all product-related pictures .Each API call is applicable for it's associated product */
import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../../shared/services';

@Injectable()
export class LinkProductSpecAttributesService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) { }
    addSpecAttribute(prodId,attributeId,specId,specName,value){
      console.log(prodId);
      console.log(attributeId);
      console.log(specId);
      console.log(specName);
      console.log(value);
        return this.http.get(this.urlService.serverUrl+'/Products/'+prodId+'/ProductSpecificationAttributeAdd/'+attributeId+'/'+specId+'/'+value+'/sampleString/true/true/1');

    }
    getSpecAttributes() {
     return this.http.get(this.urlService.serverUrl+'/SpecificationAttribute/0/2147483647');
    }
    getCurrentSpecAttributes(id:number) {
      console.log("Get Current Spec Attributes called !");
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post(this.urlService.serverUrl+'/Products/'+id+'/ProductSpecAttrList',{
          "Page": 0,
          "PageSize": 20
        },{ headers: headers });
    }
    getProductAttributes(id:number){
      console.log("Get Attributes called !");
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(id);

  return this.http.post(this.urlService.serverUrl+'/Products/'+id+'/ProductAttributeMappingList',    {
    "Page": 0,
    "PageSize":200
}, { headers: headers });
    }
    deleteSpecAttribute(id:number){
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        return this.http.post(this.urlService.serverUrl+'/Products/ProductSpecAttr/Delete?id='+id, null, { headers: headers });
    }



}
