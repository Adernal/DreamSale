import { Component, OnInit } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';

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
  constructor() { }

  ngOnInit() {
    this.products = JSON.parse(localStorage.getItem("products"));
    if (localStorage.getItem("reviews") != null) {
        this.review = JSON.parse(localStorage.getItem("reviews"));

    }

  }
  x

}
