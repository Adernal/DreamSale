import { Component, OnInit } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';
import { ProductReviewsService } from './product-reviews.service';

@Component({
  selector: 'app-product-reviews',
  templateUrl: './product-reviews.component.html',
  styleUrls: ['./product-reviews.component.scss']
})
export class ProductReviewsComponent implements OnInit {
editMode = false;
review;
reviews;
products;
Id;
deleteSelection:number[];
filteredReviews='';
  constructor(private reviewService: ProductReviewsService) { }

  ngOnInit() {
    this.getReviews();
    }

  getReviews(){
    this.reviewService.getReviews()
      .subscribe(
      (response) => {
        this.reviews = (response.json());
        this.review = this.reviews.Data;

        console.log(("Fetched Reviews"));

        //  this.attribute =[this.attributes];
      },
      (error) => {
        console.log(error)
        alert('Can\'t fetch data ! Please refresh or check your connnection !');
      }
      );

  }
  deleteReview(id){
    const confirmation = confirm('Are you sure you want to delete ?');
    if (confirmation) {
      this.Id = +this.review[+id.name].Id;
      this.deleteSelection[0]=this.Id;
      this.reviewService.deleteReviews(this.deleteSelection)
        .subscribe(
        (data) => {

          this.getReviews();
          alert('Deleted !');
        },
        (error) => {
          console.log(error)
          alert('Can\'t fetch data ! Please refresh or check your connnection !')
        }
        );
    }
    if (this.editMode) {
      this.editMode = false;

    }
  }




}
