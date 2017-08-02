import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

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
    Id: Number;
    Name = '';
    Description:string;
    Email:string;
    AdminComment:string;
    Address:string;
    active:Number;
    editMode = false;
    products;
    filteredvendor='';

    constructor() { }

    ngOnInit() {
        if(localStorage.getItem("vendors")!=null){
        this.vendor = JSON.parse(localStorage.getItem("vendors"));

      }
    }
    addVendor() {
        this.submitted = true;
        if (this.editMode) {
            this.editVendor();
        }
        else {
          if (this.vendor.length == 0) {
                    this.Id = 1;
                }
                else {
                    this.Id = +this.vendor[this.vendor.length - 1].Id + 1;
                }
                this.Name = this.vendorForm.value.Name;
                this.Description = this.vendorForm.value.Description;
                this.Email = this.vendorForm.value.Email;
                this.AdminComment = this.vendorForm.value.AdminComment;
                this.Address = this.vendorForm.value.Address;
                this.active = this.vendorForm.value.active;

            this.vendor.push({
                'Id': this.Id,
                'Name': this.Name,
                'Description':this.Description,
                'Email':this.Email,
                'AdminComment':this.AdminComment,
                'Address':this.Address,
                'Active':this.active

            });
            localStorage.setItem("vendors", JSON.stringify(this.vendor));
            alert("Added !");
            console.log(this.vendor);
            this.vendorForm.reset();
        }

    }
    editVendor() {
        this.editMode = false;
        this.Name = this.vendorForm.value.Name;
        this.vendor[+this.Id].Name = this.Name;
        this.vendor[+this.Id].Description = this.Description;
        localStorage.setItem("vendors", JSON.stringify(this.vendor));
        alert("Edited !");

    }
    editVendorMode(id: HTMLFormElement) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        // console.log(this.vendor[1].Name);
        this.Name = this.vendor[+this.Id].Name;
        this.Description = this.vendor[+this.Id].Description;
        this.Email = this.vendor[+this.Id].Email;
        this.Address = this.vendor[+this.Id].Address;
        this.active = this.vendor[+this.Id].Active;
        this.AdminComment = this.vendor[+this.Id].AdminComment;


    }
    deleteVendor(id:HTMLFormElement){
        var confirmation = confirm("Are you sure you want to delete ?");
      if(confirmation){
        this.Id = +id.name;
        this.vendor = JSON.parse(localStorage.getItem("vendors"));
        this.vendor.splice(+this.Id,1);
        localStorage.setItem("vendors",JSON.stringify(this.vendor));
        this.vendorForm.reset();
        if(this.editMode){
            this.editMode=false;

        }
        alert("Deleted !");
      }
    }
    }
