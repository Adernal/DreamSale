import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../shared/services';


@Injectable()
export class ProductService {
    temp: {};
    Token = localStorage.getItem("Token");

    headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() ,'Accept':'application/json',});
    constructor(private http: Http,private urlService:URLService) { }
    storeProduct(product) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() , 'Accept' : 'application/json'});
        this.temp = product[0];
        console.log(this.temp);
        return this.http.post(this.urlService.serverUrl+'/Products/Create', this.temp,

            {headers:this.headers});
    }
    getAllProducts(page:number){
         console.log(page);
        return this.http.post(this.urlService.serverUrl+'/Products',
            {'Page':1,'PageSize':50000},
             {headers:this.headers}
            );

    }
    searchProduct(searchProductParameters){


         console.log();
        return this.http.post(this.urlService.serverUrl+'/Products?pageIndex=0&pageSize=25878',searchProductParameters,
             {headers:this.headers});
    //  return this.http.get('/Products/DefaultPageLoad');
    }
    // getAttributes() {
    //     return this.http.get('/ProductAttribute/0/2147483647');
    // }
    getCurrentAttributes(id:number) {


        return this.http.post(this.urlService.serverUrl+'/Products/'+id+'/ProductAttributeMappingList',    {
          "Page": 0,
          "PageSize":200
      }, {headers:this.headers});
    }
    addAttribute(attribute){

      this.temp = attribute[0];

      console.log(attribute[0]);
      return this.http.post(this.urlService.serverUrl+'/Products/ProductAttributeMapping/Add',this.temp, {headers:this.headers});

    }
    deleteAttribute(id: number) {

        return this.http.post(this.urlService.serverUrl+'/Products/ProductAttributeMapping/Delete?id=' + id, null,
         {headers: this.headers});

    }
    addSpecAttribute(prodId,attributeId,specId,specName,value){

        return this.http.get(this.urlService.serverUrl+'/Products/'+prodId+'/ProductSpecificationAttributeAdd/' +attributeId + '/'+specId+'/' + value + '/sampleString/true/true/1');

    }
    getSpecAttributes() {
      return this.http.post(this.urlService.serverUrl+'/SpecificationAttribute', {
        'Page': 1,
        'PageSize': 2
      }, {headers: this.headers});
    }
    getCurrentSpecAttributes(id: number) {
        return this.http.post(this.urlService.serverUrl+'/Products/' + id + '/ProductSpecAttrList',    {
          'Page': 0,
          'PageSize': 20
        });
    }
    deleteSpecAttribute(id: number){

        console.log('Id = ' + id);

        return this.http.post(this.urlService.serverUrl+'/Products/ProductSpecAttr/Delete?id=' + id, null, {headers: this.headers});
    }
    getCategory() {

        return this.http.post(this.urlService.serverUrl+'/categories', {}, { headers: this.headers });
    }
    getManufacturers(){
        return this.http.post(this.urlService.serverUrl+'/Manufacturers', {}, {headers: this.headers});
    }
    getStores(){
          return this.http.get(this.urlService.serverUrl+'/Stores');
    }
    getVendors(){
        return this.http.post(this.urlService.serverUrl+'/Vendors?showHidden=true', {}, {headers: this.headers});
    }
    updateProduct(product) {

        console.log("Product sent :"+product);
        return this.http.post(this.urlService.serverUrl+'/Products/Update', product, {headers: this.headers});
    }
    deleteProduct(id: number) {

        console.log('Id = ' + id);
        return this.http.post(this.urlService.serverUrl+'/products/Delete?id=' + id, null, {headers: this.headers});
    }
    // importProducts(selectedFiles){
    //     return this.http.post('http://denmakers3-001-site1.ctempurl.com:35894/api/categories/ImportXlsx', selectedFiles, {headers: this.headers});
    // }


}
