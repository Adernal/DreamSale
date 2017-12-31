import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ProductReviewsService {
    temp: {};
    constructor(private http: Http) { }

    getReviews() {
        return this.http.get('http://denmakers2-001-site1.gtempurl.com/api/ProductReviews');
    }
    updateReviews(reviews, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(reviews);
        return this.http.post('http://denmakers2-001-site1.gtempurl.com/api/ProductAttribute/Update?continueEditing=true', reviews[id], { headers: headers });
    }
    deleteReviews(id:number[]) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        //console.log(reviews);

        return this.http.post('http://denmakers2-001-site1.gtempurl.com/api/ProductReviews/DeleteSelected',id, { headers: headers });
    }


}
