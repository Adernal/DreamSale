import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class CategoryService {
    temp: {};
    constructor(private http: Http) { }
    storeCategory(category) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = category[category.length-1];
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Categories/Add?continueEditing=true', this.temp,
            { headers: headers });
    }
    getCategory() {
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/categories',{});
    }
    updateCategory(category) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Categories/Update?continueEditing=true', category, { headers: headers });
    }
    deleteCategory(id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers-001-site1.itempurl.com/api/Categories/Delete?id='+id, null, { headers: headers });
    }


}
