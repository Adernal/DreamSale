import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ProductService {
    temp: {};
    constructor(private http: Http) { }
    storeProduct(product) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = product[product.length-1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Add?continueEditing=true', this.temp,
            { headers: headers });
    }
    getAllProducts(){
         const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products?pageIndex=0&pageSize=258768',{},
             { headers: headers });
    //  return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Products/DefaultPageLoad');
    }
    getAttributes() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/0/2147483647');
    }
    getSpecAttributes() {
      return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/0/2147483647');
    }
    getCategory() {
      return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Categories');
    }
    updateProduct(product, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(product);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Update?continueEditing=true', product[id], { headers: headers });
    }
    deleteProduct(product, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(product);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Delete/' + id, null, { headers: headers });
    }


}
