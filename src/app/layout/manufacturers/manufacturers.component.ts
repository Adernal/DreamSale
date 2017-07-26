import { Component, OnInit ,ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ManufacturersService } from './manufacturers.service';

@Component({
  selector: 'app-manufacturers',
  templateUrl: './manufacturers.component.html',
  styleUrls: ['./manufacturers.component.scss']
})
export class ManufacturersComponent implements OnInit {
  @ViewChild('f') manufacturerForm :NgForm;
  submitted = false;
  manufacturer =[];
  Id:Number;
  Name='';
  manufacturer_description='';
  DisplayOrder:Number;
  editMode =false;
  filteredManufacturer='';
  manufacturers;
  constructor(private manufacturersService : ManufacturersService) { }

  ngOnInit() {
  //  localStorage.removeItem("manufacturer");
    //localStorage.removeItem("manufacturers");
    if (localStorage.getItem("manufacturer") != null) {
        this.manufacturer = JSON.parse(localStorage.getItem("manufacturer"));

    }
    //this.getManufacturers();
  }
  addManufacturer(){
    this.submitted=true;
    if(this.editMode){
      this.editManufacturer();
    }
    else{
      if(this.manufacturer.length==0){
        this.Id =1;
      }
      else{
          this.Id = +this.manufacturer[this.manufacturer.length-1].Id+1;
      }

      this.Name= this.manufacturerForm.value.Name;

      this.DisplayOrder= this.manufacturerForm.value.DisplayOrder;
      this.manufacturer.push({
        "Id": this.Id,
        "CustomProperties": {
          "sample string 1": {},
          "sample string 3": {}
        },
        "Name": this.Name,
        "Description": "Test Description",
        "ManufacturerTemplateId": 4,
        "AvailableManufacturerTemplates": [
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
        "MetaKeywords": "sample string 5",
        "MetaDescription": "sample string 6",
        "MetaTitle": "sample string 7",
        "SeName": "sample string 8",
        "PictureId": 9,
        "PageSize": 10,
        "AllowCustomersToSelectPageSize": true,
        "PageSizeOptions": "sample string 12",
        "PriceRanges": "sample string 13",
        "Published": true,
        "Deleted": true,
        "DisplayOrder": this.DisplayOrder,
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
        "SelectedCustomerRoleIds": [
          1,
          2
        ],
        "AvailableCustomerRoles": [
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
        "SelectedStoreIds": [
          1,
          2
        ],
        "AvailableStores": [
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
        "SelectedDiscountIds": [
          1,
          2
        ],
        "AvailableDiscounts": [
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
        ]
      });
    localStorage.setItem("manufacturers",JSON.stringify(this.manufacturer));
    alert("Added");
    // this.manufacturersService.storeManufacturers(this.manufacturer)
    // .subscribe(
    //   (response)=>console.log(response),
    //   (error)=>console.log(error)
    // );
  //  this.manufacturers.manufacturer=manufacturer;

    this.manufacturerForm.reset();
  }

  }
  editManufacturerMode(id : HTMLFormElement){
    this.editMode=true;
    this.Id = +id.name;
    console.log(this.Id);
     this.Name = this.manufacturer[+this.Id].Name;


     this.DisplayOrder = this.manufacturer[+this.Id].DisplayOrder;


  }
  editManufacturer(){
    this.editMode=false;
    this.Name= this.manufacturerForm.value.Name;
    this.DisplayOrder= this.manufacturerForm.value.DisplayOrder;
    this.manufacturer[+this.Id].Name = this.Name;
    this.manufacturer[+this.Id].DisplayOrder = this.DisplayOrder;
    localStorage.setItem("manufacturers",JSON.stringify(this.manufacturer));
    alert("Edited");
    // this.manufacturersService.updateManufacturers(this.manufacturer,this.Id)
    // .subscribe(
    //   (response)=>{
    //     console.log(response);
    //     alert("Edited !");
    //     this.getManufacturers();
    //   },
    //   (error)=>console.log(error)
    // );


  }
  deleteManufacturer(id:HTMLFormElement){
    var confirmation = confirm("Are you sure you want to delete ?");
    if(confirmation){
      this.Id = +this.manufacturer[+id.name].Id;
      this.manufacturer = JSON.parse(localStorage.getItem("manufacturers"));
      this.manufacturer.splice(+id.name,1);
      localStorage.setItem("manufacturers",JSON.stringify(this.manufacturer));
      alert("Deleted !");
      // this.manufacturersService.deleteAttributes(this.manufacturer,this.Id)
      // .subscribe(
      //   (response)=>{
      //     console.log(response);
      //     alert("Deleted !");
      //     this.getManufacturers();
      //   },
      //   (error)=>console.log(error)
      // );
    }
  }
  // getManufacturers(){
  //   this.manufacturersService.getManufacturers()
  //   .subscribe(
  //     (response)=>{
  //       this.manufacturers = (response.json());
  //       this.manufacturer = this.manufacturers.Data;
  //       console.log((this.manufacturer));
  //     //  this.attribute =[this.manufacturers];
  //     },
  //     (error)=>console.log(error)
  //   );
  //
  // }



}
