import { Component, OnInit } from '@angular/core';
import { OnlineCustomersService } from './online-customers.service';

@Component({
  selector: 'app-online-customers',
  templateUrl: './online-customers.component.html',
  styleUrls: ['./online-customers.component.scss']
})
export class OnlineCustomersComponent implements OnInit {
online_customer;
online_customers;
  constructor(private online_customersService: OnlineCustomersService) { }

  ngOnInit() {
    this.getOnlineCustomers();
  }
  getOnlineCustomers(){
    this.online_customersService.getOnlineCustomers()
      .subscribe(
      (response) => {
        this.online_customers = (response.json());
        this.online_customer = this.online_customers.Data;
        console.log((this.online_customer));

      },
      (error) => {
        console.log(error)
        alert('Can\'t fetch data ! Please refresh or check your connnection !');
      }
      );
  }
}
