import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { URLService } from '../../../shared/services';

@Injectable()
export class ProductReviewsService {
    temp: {};
    constructor(private http: Http,private urlService:URLService) { }

    getReviews() {
        return this.http.get('/ProductReviews');
    }
    updateReviews(reviews, id) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        console.log(reviews);
        return this.http.post(this.urlService.serverUrl+'/ProductAttribute/Update?continueEditing=true', reviews[id], { headers: headers });
    }
    deleteReviews(id:number[]) {
        const headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Token ' + localStorage.getItem("Token").toUpperCase() });
        console.log("Id = " + id);
        //console.log(reviews);

        return this.http.post(this.urlService.serverUrl+'/ProductReviews/DeleteSelected',id, { headers: headers });
    }


}
