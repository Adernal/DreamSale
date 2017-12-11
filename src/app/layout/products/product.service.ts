import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';


@Injectable()
export class ProductService {
    temp: {};
    Token = localStorage.getItem("Token");
    headers = new Headers({ 'Content-Type': 'application/json' ,'Accept':'application/json','Authorization':'Token '+this.Token});
    constructor(private http: Http) { }
    storeProduct(product) {
        const headers = new Headers({ 'Content-Type': 'application/json' , 'Accept' : 'application/json'});
        this.temp = product[0];
        console.log(this.temp);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products/Create', this.temp,
      
            {headers:this.headers});
    }
    getAllProducts(page:number){
         console.log(page);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products',
            {'Page':1,'PageSize':50000},
             {headers:this.headers}
            );

    }
    searchProduct(searchProductParameters){
         

         console.log();
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products?pageIndex=0&pageSize=25878',searchProductParameters,
             {headers:this.headers});
    //  return this.http.get('http://denmakers-001-site1.itempurl.com/api/Products/DefaultPageLoad');
    }
    // getAttributes() {
    //     return this.http.get('http://denmakers-001-site1.itempurl.com/api/ProductAttribute/0/2147483647');
    // }
    getCurrentAttributes(id:number) {
        

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products/'+id+'/ProductAttributeMappingList',    {
          "Page": 0,
          "PageSize":200
      }, {headers:this.headers});
    }
    addAttribute(attribute){
      
      this.temp = attribute[0];

      console.log(attribute[0]);
      return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products/ProductAttributeMapping/Add',this.temp, {headers:this.headers});

    }
    deleteAttribute(id: number) {

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products/ProductAttributeMapping/Delete?id=' + id, null,
         {headers: this.headers});

    }
    addSpecAttribute(prodId,attributeId,specId,specName,value){

        return this.http.get('http://denmakers-001-site1.itempurl.com/api/Products/'+prodId+'/ProductSpecificationAttributeAdd/' +attributeId + '/'+specId+'/' + value + '/sampleString/true/true/1');

    }
    getSpecAttributes() {
      return this.http.post('http://denmakers-001-site1.itempurl.com/api/SpecificationAttribute', {
        'Page': 1,
        'PageSize': 2
      }, {headers: this.headers});
    }
    getCurrentSpecAttributes(id: number) {
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products/' + id + '/ProductSpecAttrList',    {
          'Page': 0,
          'PageSize': 20
        });
    }
    deleteSpecAttribute(id: number){

        console.log('Id = ' + id);

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products/ProductSpecAttr/Delete?id=' + id, null, {headers: this.headers});
    }
    getCategory() {

        return this.http.post('http://denmakers-001-site1.itempurl.com/api/categories', {}, { headers: this.headers });
    }
    getManufacturers(){
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Manufacturers', {}, {headers: this.headers});
    }
    getStores(){
          return this.http.get('http://denmakers-001-site1.itempurl.com/api/Stores');
    }
    getVendors(){
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Vendors?showHidden=true', {}, {headers: this.headers});
    }
    updateProduct(product) {


        console.log(product);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Products/Update', product, {headers: this.headers});
    }
    deleteProduct(id: number) {

        console.log('Id = ' + id);
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/products/Delete?id=' + id, null, {headers: this.headers});
    }
    // importProducts(selectedFiles){
    //     return this.http.post('http://denmakers-001-site1.itempurl.com:35894/api/categories/ImportXlsx', selectedFiles, {headers: this.headers});
    // }


}
