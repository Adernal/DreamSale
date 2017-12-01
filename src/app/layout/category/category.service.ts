import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class CategoryService {
    temp: {};
    constructor(private http: Http) { }
    storeCategory(category) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = category[category.length-1];
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Categories/Create', this.temp,
            { headers: headers });
    }
    getCategory(page) {
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Categories',{"Page":1 ,"PageSize":5000});
    }
    updateCategory(category) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Categories/Update', category, { headers: headers });
    }
    deleteCategory(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Categories/Delete?id='+id, null, { headers: headers });
    }


}
