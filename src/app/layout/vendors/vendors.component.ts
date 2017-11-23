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
    vendor;
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
                console.log(this.Display_Order);
              
                // this.Active = this.vendorForm.value.Active;

            this.vendor={
  "Id": 0,
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
  "Active": this.Active,
  "DisplayOrder": this.Display_Order,

}};
        //  localStorage.setItem("vendors", JSON.stringify(this.vendor));
            this.vendorService.storeVendor(this.vendor)
            .subscribe(
          (data)=>{
            console.log(data);
            alert("Added !");
            this.vendorForm.reset();
            this.getVendor();
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
        this.getVendor();
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
