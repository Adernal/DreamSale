/*Services for all product-related pictures .Each API call is applicable for it's associated product */
import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class LinkProductSpecAttributesService {
    temp: {};
    constructor(private http: Http) { }
    addSpecAttribute(prodId,attributeId,specId,specName,value){

        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Products/'+prodId+'/ProductSpecificationAttributeAdd/'+attributeId+'/'+specId+'/'+value+'/sampleString/true/true/1');

    }
    getSpecAttributes() {
      return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/0/2147483647');
    }
    getCurrentSpecAttributes(id:number) {
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/'+id+'/ProductSpecAttrList',    {
          "Page": 0,
          "PageSize": 20
        });
    }
    deleteSpecAttribute(id:number){
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/ProductSpecAttr/Delete?id='+id, null, { headers: headers });
    }



}