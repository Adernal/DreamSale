import { Component, OnInit , ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgModule } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CustomerRoleService } from './customer-roles.service';

@Component({
  selector: 'app-customer-roles',
  templateUrl: './customer-roles.component.html',
  styleUrls: ['./customer-roles.component.scss']
})
export class CustomerRolesComponent implements OnInit {
  currentPageNumber:number=1;
  Id;
  Name;
  FreeShipping;
  TaxExempt;
  Active;
  IsSystemRole;
  SystemName;
  editMode=false;
  customer_role=[];
  customer_roles;
  submitted;
  currentCustomerRole=[];
  @ViewChild('f') customer_roleForm: NgForm;



  constructor(private customer_roleService: CustomerRoleService) { }

  ngOnInit() {
    //localStorage.removeItem("customer_roles");
    this.getCustomerRoles();
  }
addCustomerRole(){
  this.submitted = true;
  if (this.editMode) {
    this.editCustomerRole();
  }
  else {
    if (this.customer_role.length == 0) {
      this.Id = 1;
    }
    else {
      this.Id = +this.customer_role[this.customer_role.length - 1].Id + 1;
    }
    this.Name = this.customer_roleForm.value.Name;
    this.FreeShipping = this.customer_roleForm.value.FreeShipping;
    if(!this.FreeShipping){
      this.FreeShipping=false;
    }
    this.TaxExempt = this.customer_roleForm.value.TaxExempt;
    if(!this.TaxExempt){
      this.TaxExempt=false;
    }
    this.Active = this.customer_roleForm.value.Active;
    if(!this.Active){
      this.Active=false;
    }
    this.IsSystemRole = this.customer_roleForm.value.IsSystemRole;
    if(!this.IsSystemRole){
      this.IsSystemRole=false;
    }
    this.customer_role.push({
      "Id": this.Id,
      "Name": this.Name,
      "FreeShipping": this.FreeShipping,
      "TaxExempt": this.TaxExempt,
      "Active": this.Active,
      "IsSystemRole": this.IsSystemRole,
      "SystemName": "sample string 7",
      "EnablePasswordLifetime": true,
      "PurchasedWithProductId": 9,
      "PurchasedWithProductName": "sample string 10",
      "CustomProperties": {
        "sample string 1": {},
        "sample string 3": {}
      }
    });
    //  localStorage.setItem("customer_roles",JSON.stringify(this.customer_role));
    this.customer_roleService.storeCustomerRole(this.customer_role)
      .subscribe(
      (data) => {
        alert('Added !');
      },
      (error) => {
        alert('Failed to add !');
        console.log(error)
      }
      );

    console.log(this.customer_role);
    this.customer_roleForm.reset();


}
}
getCustomerRoles(){
  this.customer_roleService.getCustomerRole()
    .subscribe(
    (response) => {
      this.customer_roles = (response.json());
      this.customer_role = this.customer_roles.Data;


    },
    (error) => {
      console.log(error)
      alert('Can\'t fetch data ! Please refresh or check your connnection !');
    }
    );
}
editCustomerRoleMode(id : HTMLFormElement){
  this.editMode = true;
  this.Id = +id.name;
  this.currentCustomerRole = this.getCustomerRoleIndex(this.Id)[0];

  this.Name = this.currentCustomerRole["Name"];
  this.FreeShipping = this.currentCustomerRole["FreeShipping"];
  this.TaxExempt = this.currentCustomerRole["TaxExempt"];
  this.Active = this.currentCustomerRole["Active"];
  this.IsSystemRole = this.currentCustomerRole["IsSystemRole"];
}
editCustomerRole(){
  this.editMode=false;
  this.Name = this.customer_roleForm.value.Name;
  this.FreeShipping = this.customer_roleForm.value.FreeShipping;
  if(!this.FreeShipping){
    this.FreeShipping=false;
  }
  this.TaxExempt = this.customer_roleForm.value.TaxExempt;
  if(!this.TaxExempt){
    this.TaxExempt=false;
  }
  this.Active = this.customer_roleForm.value.Active;
  if(!this.Active){
    this.Active=false;
  }
  this.IsSystemRole = this.customer_roleForm.value.IsSystemRole;
  if(!this.IsSystemRole){
    this.IsSystemRole=false;
  }
  this.currentCustomerRole["Name"] = this.Name;
  this.currentCustomerRole["FreeShipping"] = this.FreeShipping;
  this.currentCustomerRole["TaxExempt"] = this.TaxExempt;
  this.currentCustomerRole["Active"] = this.Active;
  this.currentCustomerRole["IsSystemRole"] = this.IsSystemRole;

  this.customer_roleService.updateCustomerRole(this.currentCustomerRole)
    .subscribe(
    (data) => {

      console.log(data.json());

      this.getCustomerRoles();
      alert('Edited !');
    },
    (error) => {
      console.log(error);
      alert('Can\'t fetch data ! Please refresh or check your connnection !');
    }
    );
}
deleteCustomerRole(id : HTMLFormElement){
  const confirmation = confirm('Are you sure you want to delete ?');
  if (confirmation) {
    this.customer_roleService.deleteCustomerRole(+id.name)
      .subscribe(
      (data) => {
        console.log(data);

        this.getCustomerRoles();
        alert('Deleted !');
      },
      (error) => {
        console.log(error)
        alert('Can\'t fetch data ! Please refresh or check your connnection !')
      }
      );
  }
  if (this.editMode) {
    this.editMode = false;

  }
}
getCustomerRoleIndex(id:Number) {
 return this.customer_role.filter(
     function(customer_role
     ){ return customer_role.Id == id }
 );
}


}
