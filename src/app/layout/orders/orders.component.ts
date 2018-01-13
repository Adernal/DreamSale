import { Component, OnInit , ViewChild } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';
import { OrderService } from './orders.service';
import {IMyDpOptions} from '../../../../node_modules/angular4-datepicker/src/my-date-picker';
import { IMyDateModel } from 'angular4-datepicker/src/my-date-picker/interfaces';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  endDate: string;
  startDate: string;
  currentPageNumber:number;
  @ViewChild('f') ordersForm: NgForm;
  @ViewChild('s') searchForm: NgForm;
  totalOrders:number;
  OrderId:number;
  loadingOrder:boolean;
  orderList=[];
  editMode:boolean;
  StoreName='';
  BillingEmail='';
  BillingLastName='';
  CustomerIp='';
  OrderTotal='';
  OrderStatus='';
  PaymentStatus='';
  ShippingStatus='';
  ShippingAddress='';
  BillingAddress='';
  Items=[];
  CreatedOn;
  loadingImagePath:string;
  searchMode:boolean;
  searchedOrders=[];
  showAllOrders:boolean;
  searchList:boolean;
  searchParameters={};
  CustomerFullName='';
  CustomerEmail='';
  
  public myDatePickerOptions: IMyDpOptions = {
   
    dateFormat: 'dd.mm.yyyy',
};
  order;
  orderDetails=[];
  constructor(private http:Http,private ordersService:OrderService) {
  this.loadingOrder=false;
  
  this.currentPageNumber=1;
  this.OrderId=0;
  this.editMode=false;
  this.loadingImagePath='../../assets/images/ajax-loader.gif';
  this.searchMode=true;
  this.showAllOrders=true;
  this.searchList=false;

}

  ngOnInit() {
    this.getOrders(1);
  }
  getOrders(page:number){
    this.loadingOrder=true;
    this.editMode=false;

    this.ordersService.getOrders(page)
        .subscribe(
        (response) => {
            this.loadingOrder=false;
            this.currentPageNumber=page;


            this.orderList = (response.json().Data);
            console.log(this.orderList);
            this.totalOrders = (response.json().Total);

        },
        (error) =>      {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
                this.loadingOrder=false;
              }
        );

  }
  editOrderMode(id:HTMLFormElement){
    this.editMode=true;
    this.searchMode =false;
    this.OrderId =+id.name;
    this.order = this.getCurrentOrder(this.OrderId)[0];
    this.StoreName = this.order["StoreName"];
    this.CustomerEmail = this.order["CustomerEmail"];
    this.CustomerFullName=this.order["CustomerFullName"];
    this.CustomerIp=this.order["CustomerIp"];
    this.OrderTotal=this.order["OrderTotal"];
    this.OrderStatus = this.order["OrderStatus"];
    this.PaymentStatus = this.order["PaymentStatus"];
    this.ShippingStatus = this.order["ShippingStatus"];
     this.ShippingAddress = this.order['ShippingAddress'];
    //this.getShippingAddress();
    this.BillingAddress = this.order["BillingAddress"];
    this.Items = this.order["Items"];
    console.log(this.Items);
    this.CreatedOn = new Date(this.order["CreatedOn"]).toString();

  }
  getCurrentOrder(id:number){
    return this.orderList.filter(
      function(order){ return order.Id == id }
  );
  }

showSearch(){
  this.editMode=false;
  this.searchMode=true;
  this.getOrders(this.currentPageNumber);

}
showAllProducts(){
  this.showAllOrders=true;
  this.searchList = false;
}
searchOrder(page:number){
  this.loadingOrder=true;
  this.order=[];
  this.BillingLastName = this.searchForm.value.BillingLastName;
  this.BillingEmail = this.searchForm.value.BillingEmail;
  //this.StoreName = this.searchForm.value.StoreName;
  this.OrderStatus = this.searchForm.value.OrderStatus;
  this.PaymentStatus = this.searchForm.value.PaymentStatus;
  this.searchParameters={
    "BillingLastName":this.BillingLastName,
    "BillingEmail":this.BillingEmail,
    //"StoreName":this.StoreName,
    "OrderStatusIds":[+this.OrderStatus],
    "PaymentStatusIds":[+this.PaymentStatus],
    "StartDate":this.startDate,
    "EndDate":this.endDate
  };
  this.ordersService.searchOrders((this.searchParameters),page-1)
      .subscribe(
      (response) => {
          this.loadingOrder=false;
          this.showAllOrders=false;
          this.searchList=true;
          this.searchedOrders = (response.json().Data);
          this.searchForm.reset();
          console.log(this.searchedOrders);

      },
      (error) =>      {
              console.log(error);
              alert("Can't fetch data ! Please refresh or check your connnection !");
              this.loadingOrder=false;
              this.searchForm.reset();
            }
      );

}
getShippingAddress(){
  this.ordersService.getShippingAddress(this.OrderId)
  .subscribe(
  (data) => {
    console.log(data);
   this.ShippingAddress = data.json();

  },
  (error) =>      {
          console.log(error);
          alert("Can't fetch Shipping Address ! Please refresh or check your connnection !");
        }
  );
  
}

changeOrderStatus(status){
  console.log(status);
    this.ordersService.changeOrderStatus(this.OrderId,status)
  .subscribe(
  (response) => {
   
    this.OrderStatus = response.json()["OrderStatus"];
    alert("Updated Order Status !");
    console.log(response.json());

  },
  (error) =>      {
          console.log(error);
          alert("Can't change order status ! Please refresh or check your connnection !");
        }
  );
  
}
changeShippingStatus(status){
  console.log(status);
    this.ordersService.changeShippingStatus(this.OrderId,status)
  .subscribe(
  (response) => {
   
    this.ShippingStatus = response.json()["ShippingStatus"];

  },
  (error) =>      {
          console.log(error);
          alert("Can't change Shipping status ! Please refresh or check your connnection !");
        }
  );
  
}
changePaymentStatus(status){
  console.log(status);
    this.ordersService.changePaymentStatus(this.OrderId,status)
  .subscribe(
  (response) => {
   
    this.PaymentStatus = response.json()["PaymentStatus"];
    console.log(response.json());
  },
  (error) =>      {
          console.log(error);
          alert("Can't change Payment status ! Please refresh or check your connnection !");
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
  this.showAllOrders=true;
  this.editMode=false;
  this.searchMode = true;
  this.searchList=false;
}
}
