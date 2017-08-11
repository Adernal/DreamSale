import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ProductReviewsService {
    temp: {};
    constructor(private http: Http) { }

    getReviews() {
        return this.http.get('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/0/2147483647');
    }
    updateReviews(reviews, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log(reviews);
        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Update?continueEditing=true', reviews[id], { headers: headers });
    }
    deleteReviews(reviews, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        console.log(reviews);

        return this.http.post('http://piyushdaftary-001-site1.ctempurl.com/api/ProductAttribute/Delete/' + id, null, { headers: headers });
    }


}
