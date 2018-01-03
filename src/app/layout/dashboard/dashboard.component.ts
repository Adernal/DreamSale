import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../router.animations';
import { Http } from '@angular/http';
import { DashboardService } from './dashboard.service';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss'],
    animations: [routerTransition()]
})
export class DashboardComponent implements OnInit {
    bestSellerList: any;
    returnRequestCount: any;
    public alerts: Array<any> = [];
    public sliders: Array<any> = [];
    orderCount;
    orderList;
    customerCount;
    lowStockCount;
    loadingImagePath:string;
    showOrderCount:boolean;
    showCustomerCount:boolean;
    showReturnRequestCount:boolean;
    showLowStockCount:boolean;

    constructor(private http :Http ,  private dashboardService:DashboardService) {
        this.sliders.push({
            imagePath: 'assets/images/slider1.jpg',
            label: 'First slide label',
            text: 'Nulla vitae elit libero, a pharetra augue mollis interdum.'
        }, {
            imagePath: 'assets/images/slider2.jpg',
            label: 'Second slide label',
            text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.'
        }, {
            imagePath: 'assets/images/slider3.jpg',
            label: 'Third slide label',
            text: 'Praesent commodo cursus magna, vel scelerisque nisl consectetur.'
        });

        this.alerts.push({
            id: 1,
            type: 'success',
            message: `Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                Voluptates est animi quibusdam praesentium quam, et perspiciatis,
                consectetur velit culpa molestias dignissimos
                voluptatum veritatis quod aliquam! Rerum placeat necessitatibus, vitae dolorum`
        }, {
            id: 2,
            type: 'warning',
            message: `Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                Voluptates est animi quibusdam praesentium quam, et perspiciatis,
                consectetur velit culpa molestias dignissimos
                voluptatum veritatis quod aliquam! Rerum placeat necessitatibus, vitae dolorum`
        });
    }

    ngOnInit() {
      this.loadingImagePath='../../assets/images/ajax-loader.gif';
      this.showOrderCount=false;
      this.showCustomerCount=false;
      this.showLowStockCount=false;
      this.showReturnRequestCount=false;
       this.getOrders();
      this.getCustomers();
      this.getLowStockReport();
      this.getReturnRequest();
      this.getBestSeller();
    }

    public closeAlert(alert: any) {
        const index: number = this.alerts.indexOf(alert);
        this.alerts.splice(index, 1);
    }
    getOrders(){
        this.showOrderCount=true;
      this.dashboardService.getOrders()
          .subscribe(
          (response) => {


              this.showOrderCount=false; 
              this.orderCount = (response.json().Total);
              this.orderList=(response.json().Data);
              console.log(this.orderCount);
              console.log(this.orderList);

          },
          (error) =>      {
                  console.log(error);
                  this.showOrderCount=false; 
                  alert("Can't fetch total orders ! Please refresh or check your connnection !");
                }
          );
    }
    getCustomers(){
        this.showCustomerCount=true;
        this.dashboardService.getCustomers()
            .subscribe(
            (response) => {


                this.showCustomerCount=false;
                this.customerCount = (response.json().Total);
                console.log(this.customerCount);

            },
            (error) =>      {
                    console.log(error);
                    this.showCustomerCount=false;
                    alert("Can't fetch total customers ! Please refresh or check your connnection !");
                  }
            );
    }
    getLowStockReport(){
        this.showLowStockCount=true;
        this.dashboardService.getLowStockReport()
        .subscribe(
        (response) => {


            this.showLowStockCount=false;
            this.lowStockCount = (response.json().Total);
            console.log(this.lowStockCount);

        },
        (error) =>      {
                console.log(error);
                this.showLowStockCount=false;
                alert("Can't fetch low stock data ! Please refresh or check your connnection !");
              }
        );
    }
    getReturnRequest(){
        this.showReturnRequestCount=true;
        this.dashboardService.getReturnRequest()
        .subscribe(
        (response) => {


            this.showReturnRequestCount=false;
            this.returnRequestCount = (response.json().Total);
            console.log(this.returnRequestCount);

        },
        (error) =>      {
                console.log(error);
                this.showReturnRequestCount=false;
                alert("Can't fetch return request data ! Please refresh or check your connnection !");
              }
        );
    }
    getBestSeller(){
        this.dashboardService.getBestSeller()
        .subscribe(
        (response) => {


        
            this.bestSellerList = (response.json().Data);
            console.log(this.bestSellerList);

        },
        (error) =>      {
                console.log(error);
                alert("Can't fetch best Seller data ! Please refresh or check your connnection !");
              }
        );
    }
}
