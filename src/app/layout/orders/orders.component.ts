import { Component, OnInit , ViewChild } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';
import { OrderService } from './orders.service';
@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
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
  CreatedOn='';
  loadingImagePath:string;
  searchMode:boolean;
  searchedOrders=[];
  showAllOrders:boolean;
  searchList:boolean;
  searchParameters=[];
  CustomerFullName='';
  CustomerEmail='';

  order;
  orderDetails=[];
  constructor(private http:Http,private ordersService:OrderService) {
  this.loadingOrder=false;
  this.totalOrders=25878;
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

    this.ordersService.getOrders(page-1)
        .subscribe(
        (response) => {
            this.loadingOrder=false;
            this.currentPageNumber=page;


            this.orderList = (response.json().Data);

        },
        (error) =>      {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
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
    this.ShippingAddress = this.order["ShippingAddress"];
    this.BillingAddress = this.order["BillingAddress"];
    this.Items = this.order["Items"];
    this.CreatedOn = this.order["CreatedOn"];

  }
  getCurrentOrder(id:number){
    return this.orderList.filter(
      function(order){ return order.Id == id }
  );
  }
changeOrderStatus(){
  alert("Hi");
}
showSearch(){
  this.editMode=false;
  this.searchMode=true;
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
  this.searchParameters.push({
    "BillingLastName":this.BillingLastName,
    "BillingEmail":this.BillingEmail,
    //"StoreName":this.StoreName,
    "OrderStatusIds":[+this.OrderStatus],
    "PaymentStatusIds":[+this.PaymentStatus]
  });
  this.ordersService.searchOrders((this.searchParameters),page-1)
      .subscribe(
      (response) => {
          this.loadingOrder=false;
          this.showAllOrders=false;
          this.searchList=true;
          this.searchedOrders = (response.json().Data);
          console.log(this.searchedOrders);

      },
      (error) =>      {
              console.log(error);
              alert("Can't fetch data ! Please refresh or check your connnection !");
            }
      );

}
}
