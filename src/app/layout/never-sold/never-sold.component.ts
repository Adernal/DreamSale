import { Component, OnInit, ViewChild } from '@angular/core';
import {Router} from '@angular/router';
import {NgModule} from '@angular/core';
import {NgForm} from '@angular/forms';
import {NeverSoldService} from './never-sold.service';
import {IMyDpOptions} from '../../../../node_modules/angular4-datepicker/src/my-date-picker';
import { IMyDateModel } from 'angular4-datepicker/src/my-date-picker/interfaces';

@Component({
  selector: 'app-never-sold',
  templateUrl: './never-sold.component.html',
  styleUrls: ['./never-sold.component.scss']
})
export class NeverSoldComponent implements OnInit {
  loadingImagePath: string;
  totalNeverSoldItems: any;
  neverSoldList;
  showAllNeverSold:boolean;
  currentPageNumber:number;
  categoryList;
  manufacturerList;
  storeList;
  vendorList;
  showSearchList:boolean;
  Category;
  Vendor;
  Store;
  Manufacturer;
  startDate;
  endDate;
  searchParameters;
  searchNeverSoldList;
  loadingNeverSold:boolean;
  @ViewChild('c') neverSoldForm: NgForm;
  public myDatePickerOptions: IMyDpOptions = {
   
    dateFormat: 'dd.mm.yyyy',
};
  constructor(private neverSoldService:NeverSoldService) { }

  ngOnInit() {
    this.showAllNeverSold=true;
    this.showSearchList=false;
    this.loadingNeverSold=false;
    this.loadingImagePath='../../assets/images/ajax-loader.gif';
    this.getNeverSold();
    this.getCategory();
    this.getManufacturers();
    this.getStores();
    this.getVendors();

  }
  getNeverSold(){
    this.loadingNeverSold=true;
    this.neverSoldService.getNeverSold()
    .subscribe(
    (data) => {
      this.loadingNeverSold=false;
      this.neverSoldList =data.json().Data;
      this.totalNeverSoldItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch never sold items !');
      console.log(error);
    }
    );
  }
  getCategory(){
    this.neverSoldService.getCategory()
    .subscribe(
    (data) => {
    
      this.categoryList =data.json().Data;
      //this.totalNeverSoldItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch category !');
      console.log(error);
    }
    );
  }
  getManufacturers(){
    this.neverSoldService.getManufacturers()
    .subscribe(
    (data) => {
    
      this.manufacturerList =data.json().Data;
      //this.totalNeverSoldItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch manufacturers !');
      console.log(error);
    }
    );
  }
  getStores(){
    this.neverSoldService.getStores()
    .subscribe(
    (data) => {
     
      this.storeList =data.json().Data;
      //this.totalNeverSoldItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch stores !');
      console.log(error);
    }
    );
  }
  getVendors(){
    this.neverSoldService.getVendors()
    .subscribe(
    (data) => {
    
      this.vendorList =data.json().Data;
      //this.totalNeverSoldItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch vendors !');
      console.log(error);
    }
    );
  }
  searchNeverSold(){
    this.loadingNeverSold=true;
    this.Category = this.neverSoldForm.value.Category;
    this.Vendor = this.neverSoldForm.value.Vendor;
    this.Store = this.neverSoldForm.value.Store;
    this.Manufacturer = this.neverSoldForm.value.Manufacturer;
    console.log(this.Category);
    console.log(this.Vendor);
    console.log(this.Store);
    console.log(this.Manufacturer);
    console.log(this.startDate);
    console.log(this.endDate);
    this.searchParameters={
    
      "StartDate": this.startDate,
      "EndDate": this.endDate,
      "SearchCategoryId": this.Category,
      
      "SearchManufacturerId": this.Manufacturer,
      
      "SearchStoreId": this.Store,
     
      "SearchVendorId": this.Vendor,
      
      
      "Page": 1,
      "PageSize": 50000
    };
    console.log(this.searchParameters);
    this.neverSoldService.searchNeverSoldItems(this.searchParameters)
    .subscribe(
    (data) => {
      this.loadingNeverSold=false;
      this.showAllNeverSold=false;
      this.showSearchList=true;
      this.searchNeverSoldList =data.json().Data;
      console.log(this.searchNeverSoldList);
      this.totalNeverSoldItems = data.json().Total;
      this.neverSoldForm.reset();
    },
    (error) => {
      alert('Failed to perform search !');
      console.log(error);
    }
    );

  }
  onStartDateChanged(event: IMyDateModel) {
    this.startDate = new Date(event.jsdate).toLocaleDateString()
}
onEndDateChanged(event: IMyDateModel) {
  this.endDate = new Date(event.jsdate).toLocaleDateString()
}
showAll(){
  this.showSearchList=false;
  this.showAllNeverSold=true;
  this.neverSoldForm.reset();
}
}
