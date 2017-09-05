import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ProductService {
    temp: {};
    constructor(private http: Http) { }
    storeProduct(product) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp=product[0];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/Add?continueEditing=true', this.temp,
            { headers: headers });
    }
    getAllProducts(page:number){
         const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products?pageIndex='+page+'&pageSize=2',{},
             { headers: headers });
    //  return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Products/DefaultPageLoad');
    }
    searchProduct(searchProductParameters){
         const headers = new Headers({ 'Content-Type': 'application/json' });

         console.log(searchProductParameters[0]);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products?pageIndex=0&pageSize=25878',searchProductParameters[0],
             { headers: headers });
    //  return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Products/DefaultPageLoad');
    }
    getAttributes() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/0/2147483647');
    }
    getCurrentAttributes(id:number) {
        const headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/'+id+'/ProductAttributeMappingList',    {
          "Page": 0,
          "PageSize":200
      }, { headers: headers });
    }
    getSpecAttributes() {
      return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/SpecificationAttribute/0/2147483647');
    }
    getCurrentSpecAttributes(id:number) {
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/'+id+'/ProductAttributeMappingList',    {
          "Page": 1,
          "PageSize": 2
        });
    }
    getCategory() {
      return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Categories');
    }
    getManufacturers(){
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Manufacturers');
    }
    getStores(){
          return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Stores');
    }
    getVendors(){
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Vendors');
    }
    updateProduct(product) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(product);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/Edit?continueEditing=true', product, { headers: headers });
    }
    deleteProduct(id:number) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Products/Delete?id='+id, null, { headers: headers });
    }


}
