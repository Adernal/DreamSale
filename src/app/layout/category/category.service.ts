import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class CategoryService {
    temp: {};
    constructor(private http: Http) { }
    storeCategory(category) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = category[category.length-1];
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Categories/Add?continueEditing=true', this.temp,
            { headers: headers });

    }
    getCategory() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/Categories');
    }
    updateCategory(category, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(category);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Categories/Update?continueEditing=true', category[id], { headers: headers });
    }
    deleteCategory(category, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(category);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/Categories/Delete?id='+id, null, { headers: headers });
    }


}