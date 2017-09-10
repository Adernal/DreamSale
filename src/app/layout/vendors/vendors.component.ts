import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { VendorService } from './vendor.service';
@Component({
    selector: 'app-vendors',
    templateUrl: './vendors.component.html',
    styleUrls: ['./vendors.component.scss']
})

/* This is still in development ! Has a lot bugs ! */
export class VendorsComponent implements OnInit {
    @ViewChild('f') vendorForm: NgForm;
    submitted = false;
    vendor = [];
    Id: number;
    Name = '';
    Description:string;
    Email:string;
    AdminComment:string;
    Address:string;
    Active:Number;
    Display_Order:Number
    editMode = false;
    products;
    filteredVendor='';
    vendors;
    PictureId:number;
    imageUrl:string;

    // filteredEmail='';

    constructor(private vendorService : VendorService) {
      this.PictureId=0;
      this.imageUrl='';
  }

    ngOnInit() {
      //   if(localStorage.getItem("vendors")!=null){
      //   this.vendor = JSON.parse(localStorage.getItem("vendors"));
      //
      // }
      this.getVendor();
    }
    addVendor() {
        this.submitted = true;

                this.Name = this.vendorForm.value.Name;
                this.Description = this.vendorForm.value.Description;
                this.Email = this.vendorForm.value.Email;
                this.AdminComment = this.vendorForm.value.AdminComment;
              //  this.Address = this.vendorForm.value.Address;
                this.Display_Order = this.vendorForm.value.Display_Order;
                this.Active = this.vendorForm.value.Active;
                console.log(this.Active);
                // this.Active = this.vendorForm.value.Active;

            this.vendor.push({
  "Id": 0,
  "CustomProperties": {
    "sample string 1": {},
    "sample string 3": {}
  },
  "Name":this.Name,
  "Email":this.Email,
  "Description":this.Description,
  "PictureId": this.PictureId,
  "AdminComment":this.AdminComment,
  "Address": {
    "Id": 1,
    "CustomProperties": {
      "sample string 1": {},
      "sample string 3": {}
    },
    "FirstName": "sample string 2",
    "LastName": "sample string 3",
    "Email": "sample string 4",
    "Company": "sample string 5",
    "CountryId": 1,
    "CountryName": "sample string 6",
    "StateProvinceId": 1,
    "StateProvinceName": "sample string 7",
    "City": "sample string 8",
    "Address1": "sample string 9",
    "Address2": "sample string 10",
    "ZipPostalCode": "sample string 11",
    "PhoneNumber": "sample string 12",
    "FaxNumber": "sample string 13",
    "AddressHtml": "sample string 14",
    "FormattedCustomAddressAttributes": "sample string 15",
    "CustomAddressAttributes": [
      {
        "Id": 1,
        "Name": "sample string 2",
        "IsRequired": true,
        "DefaultValue": "sample string 4",
        "AttributeControlType": 1,
        "Values": [
          {
            "Id": 1,
            "Name": "sample string 2",
            "IsPreSelected": true
          },
          {
            "Id": 1,
            "Name": "sample string 2",
            "IsPreSelected": true
          }
        ]
      },
      {
        "Id": 1,
        "Name": "sample string 2",
        "IsRequired": true,
        "DefaultValue": "sample string 4",
        "AttributeControlType": 1,
        "Values": [
          {
            "Id": 1,
            "Name": "sample string 2",
            "IsPreSelected": true
          },
          {
            "Id": 1,
            "Name": "sample string 2",
            "IsPreSelected": true
          }
        ]
      }
    ],
    "AvailableCountries": [
      {
        "Disabled": true,
        "Group": {
          "Disabled": true,
          "Name": "sample string 2"
        },
        "Selected": true,
        "Text": "sample string 3",
        "Value": "sample string 4"
      },
      {
        "Disabled": true,
        "Group": {
          "Disabled": true,
          "Name": "sample string 2"
        },
        "Selected": true,
        "Text": "sample string 3",
        "Value": "sample string 4"
      }
    ],
    "AvailableStates": [
      {
        "Disabled": true,
        "Group": {
          "Disabled": true,
          "Name": "sample string 2"
        },
        "Selected": true,
        "Text": "sample string 3",
        "Value": "sample string 4"
      },
      {
        "Disabled": true,
        "Group": {
          "Disabled": true,
          "Name": "sample string 2"
        },
        "Selected": true,
        "Text": "sample string 3",
        "Value": "sample string 4"
      }
    ],
    "FirstNameEnabled": true,
    "FirstNameRequired": true,
    "LastNameEnabled": true,
    "LastNameRequired": true,
    "EmailEnabled": true,
    "EmailRequired": true,
    "CompanyEnabled": true,
    "CompanyRequired": true,
    "CountryEnabled": true,
    "CountryRequired": true,
    "StateProvinceEnabled": true,
    "CityEnabled": true,
    "CityRequired": true,
    "StreetAddressEnabled": true,
    "StreetAddressRequired": true,
    "StreetAddress2Enabled": true,
    "StreetAddress2Required": true,
    "ZipPostalCodeEnabled": true,
    "ZipPostalCodeRequired": true,
    "PhoneEnabled": true,
    "PhoneRequired": true,
    "FaxEnabled": true,
    "FaxRequired": true
  },
  "Active": this.Active,
  "DisplayOrder": this.Display_Order,
  "MetaKeywords": "sample string 9",
  "MetaDescription": "sample string 10",
  "MetaTitle": "sample string 11",
  "SeName": "sample string 12",
  "PageSize": 13,
  "AllowCustomersToSelectPageSize": true,
  "PageSizeOptions": "sample string 15",
  "Locales": [
    {
      "Id": 1,
      "LanguageId": 2,
      "Name": "sample string 3",
      "Description": "sample string 4",
      "MetaKeywords": "sample string 5",
      "MetaDescription": "sample string 6",
      "MetaTitle": "sample string 7",
      "SeName": "sample string 8"
    },
    {
      "Id": 1,
      "LanguageId": 2,
      "Name": "sample string 3",
      "Description": "sample string 4",
      "MetaKeywords": "sample string 5",
      "MetaDescription": "sample string 6",
      "MetaTitle": "sample string 7",
      "SeName": "sample string 8"
    }
  ],
  "AssociatedCustomers": [
    {
      "Id": 1,
      "Email": "sample string 2"
    },
    {
      "Id": 1,
      "Email": "sample string 2"
    }
  ],
  "AddVendorNoteMessage": "sample string 16"
}
);
        //  localStorage.setItem("vendors", JSON.stringify(this.vendor));
            this.vendorService.storeVendor(this.vendor)
            .subscribe(
          (data)=>{
            console.log(data);
            alert("Added !");
            this.vendorForm.reset();
          },
          (error)=>{
            alert("Failed to add !");
            console.log(error)}
        );
        //    alert("Added !");
            // console.log(this.vendor);
            // this.vendorForm.reset();
        }


    editVendor() {
        this.editMode = false;
        this.Name = this.vendorForm.value.Name;
        this.Description = this.vendorForm.value.Description;
        this.Email = this.vendorForm.value.Email;
        this.AdminComment = this.vendorForm.value.AdminComment;

        this.Display_Order = this.vendorForm.value.Display_Order;
        this.Active = this.vendorForm.value.Active;

        this.vendor["Name"] = this.Name;
        this.vendor["Description"] = this.Description;
        this.vendor["Email"] = this.Email;
        this.vendor["AdminComment"] = this.AdminComment;

        this.vendor["DisplayOrder"] = this.Display_Order;
        this.vendor["Active"] =this.Active;
        this.vendorService.updateVendor(this.vendor)
        .subscribe(
      (data)=>{
        console.log(data);
        alert("Edited !");
        this.vendorForm.reset();
      },
      (error)=>{
        alert("Failed to edit !");
        console.log(error)}
    );

    }
    editVendorMode(id: HTMLFormElement) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        this.vendor = this.getCurrentVendor(this.Id)[0];
        // console.log(this.vendor[1].Name);
        this.Name = this.vendor["Name"];
        this.Description =this.vendor["Description"];
        this.Email = this.vendor["Email"];
        this.AdminComment =this.vendor["AdminComment"];
        this.Display_Order = this.vendor["DisplayOrder"];
        this.Active = this.vendor["Active"];


    }
    deleteVendor(id:HTMLFormElement){
        var confirmation = confirm("Are you sure you want to delete ?");
      if(confirmation){
        this.vendorService.deleteVendor(+id.name)
          .subscribe(
          (data) => {

            alert('Deleted !');
            this.getVendor();
          },
          (error) => {
            console.log(error)
            alert('Can\'t fetch data ! Please refresh or check your connnection !')
          }
          );
      }
    }
    getVendor(){
      this.vendorService.getVendor()
      .subscribe(
        (response)=>{
          this.vendors = (response.json().Data);
          //this.vendor = this.vendors.Data;
          console.log((this.vendors));

        //  this.attribute =[this.attributes];
        },
        (error)=>{
          console.log(error)
          alert("Can't fetch data ! Please refresh or check your connnection !");
        }
      );

    }
    getCurrentVendor(id:number){
      return this.vendors.filter(
        function(vendor){ return vendor.Id == id }
    );
    }
    getPictureDetails(file){
          this.PictureId = file.serverResponse.json().pictureId;
          this.imageUrl = file.serverResponse.json().imageUrl;
      }
    }
