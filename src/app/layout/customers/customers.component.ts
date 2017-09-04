import { Component, OnInit } from '@angular/core';
import { CustomersService } from './customers.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
/* This is still in development ! Has bugs ! */
export class CustomersComponent implements OnInit {

currentPageNumber:number=1;
editMode=false;
customer=[];
customers;

  constructor(private customersService: CustomersService) { }

  ngOnInit() {
    this.getCustomers();
  }
getCustomers(){
  this.customersService.getCustomers()
    .subscribe(
    (response) => {
      this.customers = (response.json());
      console.log(this.customers);
      this.customer.push(this.customers);
    },
    (error) => {
      console.log(error)
      alert('Can\'t fetch data ! Please refresh or check your connnection !');
    }
    );
}

editCustomerMode(){

}
editCustomer(){

}
deleteCustomer(){

}
}
