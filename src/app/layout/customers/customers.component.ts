import { Component, OnInit , ViewChild } from '@angular/core';
import { CustomersService } from './customers.service';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
/* This is still in development ! Has bugs ! */
export class CustomersComponent implements OnInit {

@ViewChild('c') customerForm: NgForm;
currentPageNumber:number;
editMode:boolean;
customer=[];
customerList=[];
CustomerId:number;
Email:string;
Name:string;
CustomerRoleNames;
Company:string;
Active:boolean;
CreatedOn:string;
LastActivityDate:string;
loadingCustomer:boolean;
loadingImagePath:string;
totalCustomers:number;
searchParameters={};
  constructor(private http : Http,private customersService: CustomersService) {
      this.currentPageNumber=1;
      this.editMode=false;
      this.CustomerId=0;
      this.Email='';
      this.Name='';
      this.Company='';
      this.Active=false;
      this.CreatedOn='';
      this.LastActivityDate='';
      this.loadingCustomer=false;
      this.loadingImagePath='../../assets/images/ajax-loader.gif';
      this.totalCustomers=0;
  }

  ngOnInit() {
    this.getCustomers(1);
  }
getCustomers(page:number){
  this.loadingCustomer=true;
  this.customersService.getCustomers(page)
    .subscribe(
    (response) => {
      this.currentPageNumber=page;
      this.customerList = (response.json().Data);
      console.log(this.customerList);
      this.loadingCustomer=false;
      this.totalCustomers = response.json().Total;
    },
    (error) => {
      console.log(error)
      alert('Can\'t fetch data ! Please refresh or check your connnection !');
    }
    );
}

// editCustomerMode(id:HTMLFormElement){
//   this.editMode =true;
//   this.CustomerId = +id.name;
//   this.customer = this.getCurrentCustomer(this.CustomerId);
//   // this.Name = this.customer["Name"];
//   // this.Email = this.customer["Email"];
//   // this.Company = this.customer["Company"];
//   // this.Active = this.customer["Active"];
//   // this.CreatedOn
//
// }
// editCustomer(){
//
// }
deleteCustomer(id:HTMLFormElement){
  var confirmation = confirm("Are you sure you want to delete ?");
  if (confirmation) {
    this.loadingCustomer=true;
    this.customersService.deleteCustomers(+id.name)
      .subscribe(
      (data) => {

        alert('Deleted !');
        console.log(data.json());
        this.getCustomers(this.currentPageNumber);
      },
      (error) => {
        console.log(error)
        alert('Can\'t fetch data ! Please refresh or check your connnection !')
      }
      );

}
}
getCurrentCustomer(id){
  return [];
}
searchCustomer(){
  this.loadingCustomer=true;
  this.Name = this.customerForm.value.Name;
  this.Email = this.customerForm.value.Email;
  this.searchParameters={
    "SearchEmail":this.Email,
    "SearchUsername":this.Name
  };
  this.customersService.searchCustomers(this.searchParameters)
    .subscribe(
    (response) => {
      this.customerList=(response.json().Data);
    },
    (error) => {
      console.log(error)
      alert('Can\'t fetch data ! Please refresh or check your connnection !')
    }
    );
}
}
