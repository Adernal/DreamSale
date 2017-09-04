import { Component, OnInit , ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgModule } from '@angular/core';
import { NgForm } from '@angular/forms';
import { StoresService } from './stores.service';

@Component({
  selector: 'app-stores',
  templateUrl: './stores.component.html',
  styleUrls: ['./stores.component.scss']
})
export class StoresComponent implements OnInit {

  @ViewChild('f') storeForm: NgForm;
  submitted = false;
  store = [];
  Id: Number;
  Name = '';
  CompanyAddress = '';
  CompanyName='';
  CompanyVat='';
  CompanyPhoneNumber='';
  Url='';
  DisplayOrder: Number;
  editMode = false;
  filteredStores = '';
  stores;
  currentStore=[];

  constructor(private storeService: StoresService) {}

  ngOnInit() {
    localStorage.removeItem("stores");
    this.getStores();
  }
  addStores() {
    this.submitted = true;
    if (this.editMode) {
      this.editStores();

    }
    else {
      if (this.store.length == 0) {
        this.Id = 1;
        console.log(this.Id);
      }
      else {
        this.Id = +this.store[this.store.length - 1].Id + 1;
      }

      this.Name = this.storeForm.value.Name;
      this.CompanyName = this.storeForm.value.CompanyName;
      this.CompanyAddress = this.storeForm.value.CompanyAddress;
      this.CompanyVat = this.storeForm.value.CompanyVat;
      this.CompanyPhoneNumber = this.storeForm.value.CompanyPhoneNumber;
      this.Url = this.storeForm.value.Url;
      this.DisplayOrder = this.storeForm.value.DisplayOrder;
          this.store.push({
    "Id":0,
    "CustomProperties": {},
    "Name": this.Name,
    "Url": this.Url,
    "SslEnabled": false,
    "SecureUrl": null,
    "Hosts": "www.google.com",
    "DefaultLanguageId": 0,
    "AvailableLanguages": [
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "---",
            "Value": "0"
        },
        {
            "Disabled": false,
            "Group": null,
            "Selected": false,
            "Text": "English",
            "Value": "1"
        }
    ],
    "DisplayOrder": this.DisplayOrder,
    "CompanyName": this.CompanyName,
    "CompanyAddress": this.CompanyAddress,
    "CompanyPhoneNumber": this.CompanyAddress,
    "CompanyVat": this.CompanyVat,
    "Locales": []
});
        //localStorage.setItem("stores",JSON.stringify(this.store));
      this.storeService.storeStores(this.store)
        .subscribe(
        (data) => {
          console.log(data);
          alert('Added !');
        },
        (error) => {
          alert('Failed to add !');
          console.log(error)
        }
        );
       //this.stores.store=store;
      console.log(this.store);
      this.storeForm.reset();
    }

  }
  editStoresMode(id: HTMLFormElement) {
    this.editMode = true;
    this.Id = +id.name;
    this.currentStore = this.getStoreIndex(this.Id)[0];

    //console.log(this.Id);
    // console.log(this.store[1].Name);
    this.Name = this.currentStore["Name"];
    this.CompanyName = this.currentStore["CompanyName"];
    this.CompanyAddress = this.currentStore["CompanyAddress"];
    this.CompanyPhoneNumber = this.currentStore["CompanyPhoneNumber"];
    this.CompanyVat = this.currentStore["CompanyVat"];
    this.Url = this.currentStore["Url"];
    this.DisplayOrder = this.currentStore["DisplayOrder"];


  }
  editStores() {
    this.editMode = false;
    this.Name = this.storeForm.value.Name;
    this.CompanyName = this.storeForm.value.CompanyName;
    this.CompanyAddress = this.storeForm.value.CompanyAddress;
    this.CompanyPhoneNumber = this.storeForm.value.CompanyPhoneNumber;
    this.CompanyVat = this.storeForm.value.CompanyVat;
    this.Url = this.storeForm.value.Url;
    this.DisplayOrder = this.storeForm.value.DisplayOrder;


    this.currentStore["Name"] = this.Name;
    this.currentStore["CompanyName"] = this.CompanyName;
    this.currentStore["CompanyAddress"] = this.CompanyAddress;
    this.currentStore["CompanyPhoneNumber"] = this.CompanyPhoneNumber;
    this.currentStore["CompanyVat"] = this.CompanyVat;
    this.currentStore["Url"] = this.Url;
    this.currentStore["DisplayOrder"] = this.DisplayOrder;

    // localStorage.setItem("stores",JSON.stringify(this.store));
    this.storeService.updateStores(this.store)
      .subscribe(
      (data) => {

        console.log(data.json());

        this.getStores();
        alert('Edited !');
      },
      (error) => {
        console.log(error);
        alert('Can\'t fetch data ! Please refresh or check your connnection !');
      }
      );

  }
  getStores() {
  //  this.store = JSON.parse(localStorage.getItem("stores"));
    this.storeService.getStores()
      .subscribe(
      (response) => {
        this.stores = (response.json());
        this.store = this.stores.Data;
        console.log((this.store));
      },
      (error) => {
        console.log(error)
        alert('Can\'t fetch data ! Please refresh or check your connnection !');
      }
      );

  }
  deleteStores(id: HTMLFormElement) {
    const confirmation = confirm('Are you sure you want to delete ?');
    if (confirmation) {
      this.storeService.deleteStores(+id.name)
        .subscribe(
        (data) => {
          console.log(data);

          this.getStores();
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
  getStoreIndex(id:Number) {
   return this.store.filter(
       function(store){ return store.Id == id }
   );
 }

}
