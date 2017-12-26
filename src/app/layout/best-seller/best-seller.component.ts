import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {NgModule} from '@angular/core';
import {NgForm} from '@angular/forms';
import {BestSellerService} from './best-seller.service';

@Component({
  selector: 'app-best-seller',
  templateUrl: './best-seller.component.html',
  styleUrls: ['./best-seller.component.scss']
})
export class BestSellerComponent implements OnInit {
  best_sellers;
  showAllBestSellers:boolean;
  constructor(private bestSellerService:BestSellerService) { }

  ngOnInit() {
    this.showAllBestSellers=true;
    this.getBestSeller();
  }
  getBestSeller(){
    this.bestSellerService.getBestSeller()
    .subscribe(
    (data) => {
      console.log(data);
      this.best_sellers =data.json().Data;
    
    },
    (error) => {
      alert('Failed to fetch best sellers !');
      console.log(error);
    }
    );
  }
}
