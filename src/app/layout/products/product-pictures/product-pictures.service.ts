/*Services for all product-related pictures .Each API call is applicable for it's associated product */
import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

@Injectable()
export class ProductPicturesService {
    temp: {};
    constructor(private http: Http) { }

    getPicture(id:number){
        const headers = new Headers({ 'Content-Type': 'application/json' });
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Products/'+id+'/ProductPictureList',    {
          "Page": 0,
          "PageSize":200
      }, { headers: headers });

    }
    addPicture(picture){
        const headers = new Headers({ 'Content-Type': 'application/json' });
        this.temp = picture[0];

        console.log(picture[0]);
        return this.http.get('http://denmakers3-001-site1.ctempurl.com/api/Products/'+this.temp["ProductId"]+'/ProductPictureAdd/'+this.temp["PictureId"]+'/'+this.temp["DisplayOrder"]+'/sampleString/sampleString');

    }
    deletePicture(id:number){
        const headers = new Headers({ 'Content-Type': 'application/json' });
        console.log("Id = " + id);
        return this.http.post('http://denmakers3-001-site1.ctempurl.com/api/Products/ProductPicture/Delete?id='+id, null, { headers: headers });
    }


}
