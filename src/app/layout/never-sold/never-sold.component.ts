import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {NgModule} from '@angular/core';
import {NgForm} from '@angular/forms';
import {NeverSoldService} from './never-sold.service';

@Component({
  selector: 'app-never-sold',
  templateUrl: './never-sold.component.html',
  styleUrls: ['./never-sold.component.scss']
})
export class NeverSoldComponent implements OnInit {
  best_sellers;
  showAllNeverSold:boolean;
  constructor(private neverSoldService:NeverSoldService) { }

  ngOnInit() {
    this.showAllNeverSold=true;
    this.getNeverSold();
  }
  getNeverSold(){
    this.neverSoldService.getNeverSold()
    .subscribe(
    (data) => {
      console.log(data);
      this.best_sellers =data.json().Data;
    
    },
    (error) => {
      alert('Failed to fetch never sold items !');
      console.log(error);
    }
    );
  }
}
