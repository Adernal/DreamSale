import { Component, OnInit , ViewChild } from '@angular/core';
import {Router} from '@angular/router';
import {NgModule} from '@angular/core';
import {NgForm} from '@angular/forms';
import {BestSellerService} from './best-seller.service';
import {IMyDpOptions} from '../../../../node_modules/angular4-datepicker/src/my-date-picker';
import { IMyDateModel } from 'angular4-datepicker/src/my-date-picker/interfaces';


@Component({
  selector: 'app-best-seller',
  templateUrl: './best-seller.component.html',
  styleUrls: ['./best-seller.component.scss']
})
export class BestSellerComponent implements OnInit {
  loadingImagePath: string;
  totalBestSellerItems: any;
  bestSellerList;
  showAllBestSeller:boolean;
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
  searchBestSellerList;
  loadingBestSeller:boolean;
  orderStatus;
  paymentStatus;
  @ViewChild('c') bestSellerForm: NgForm;
  public myDatePickerOptions: IMyDpOptions = {
   
    dateFormat: 'dd.mm.yyyy',
};
  constructor(private bestSellerService:BestSellerService) { }

  ngOnInit() {
    this.showAllBestSeller=true;
    this.showSearchList=false;
    this.loadingBestSeller=false;
    this.loadingImagePath='../../assets/images/ajax-loader.gif';
    this.getBestSeller();
    this.getCategory();
    this.getManufacturers();
    this.getStores();
    this.getVendors();

  }
  getBestSeller(){
    this.loadingBestSeller=true;
    this.bestSellerService.getBestSeller()
    .subscribe(
    (data) => {
      this.loadingBestSeller=false;
      this.bestSellerList =data.json().Data;
      this.totalBestSellerItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch best seller items !');
      this.loadingBestSeller=false;
      console.log(error);
    }
    );
  }
  getCategory(){
    this.bestSellerService.getCategory()
    .subscribe(
    (data) => {
    
      this.categoryList =data.json().Data;
      //this.totalBestSellerItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch category !');
      console.log(error);
    }
    );
  }
  getManufacturers(){
    this.bestSellerService.getManufacturers()
    .subscribe(
    (data) => {
    
      this.manufacturerList =data.json().Data;
      //this.totalBestSellerItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch manufacturers !');
      console.log(error);
    }
    );
  }
  getStores(){
    this.bestSellerService.getStores()
    .subscribe(
    (data) => {
     
      this.storeList =data.json().Data;
      //this.totalBestSellerItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch stores !');
      console.log(error);
    }
    );
  }
  getVendors(){
    this.bestSellerService.getVendors()
    .subscribe(
    (data) => {
    
      this.vendorList =data.json().Data;
      //this.totalBestSellerItems = data.json().Total;
    },
    (error) => {
      alert('Failed to fetch vendors !');
      console.log(error);
    }
    );
  }
  searchBestSeller(){
    this.loadingBestSeller=true;
    this.Category = this.bestSellerForm.value.Category;
    this.Vendor = this.bestSellerForm.value.Vendor;
    this.Store = this.bestSellerForm.value.Store;
    this.Manufacturer = this.bestSellerForm.value.Manufacturer;
    this.orderStatus=this.bestSellerForm.value.OrderStatus;
    this.paymentStatus=this.bestSellerForm.value.PaymentStatus;
    console.log(this.Category);
    console.log(this.Vendor);
    console.log(this.Store);
    console.log(this.Manufacturer);
    console.log(this.startDate);
    console.log(this.endDate);
    console.log(this.orderStatus);
    console.log(this.paymentStatus);
    this.searchParameters={
    
      "StartDate": this.startDate,
      "EndDate": this.endDate,
      "StoreId": this.Store,
      "OrderStatusId": this.orderStatus,
      "PaymentStatusId": this.paymentStatus,
      "CategoryId": this.Category,
      "ManufacturerId": this.Manufacturer,         
      "VendorId": this.Vendor,
      "Page": 1,
      "PageSize": 50000
    };
    console.log(this.searchParameters);
    this.bestSellerService.searchBestSellerItems(this.searchParameters)
    .subscribe(
    (data) => {
      this.loadingBestSeller=false;
      this.showAllBestSeller=false;
      this.showSearchList=true;
      this.searchBestSellerList =data.json().Data;
      console.log(this.searchBestSellerList);
      this.totalBestSellerItems = data.json().Total;
      this.bestSellerForm.reset();
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
  this.showAllBestSeller=true;
  this.bestSellerForm.reset();
}
}
