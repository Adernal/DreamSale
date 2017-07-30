import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-vendors',
    templateUrl: './vendors.component.html',
    styleUrls: ['./vendors.component.scss']
})

/* This is still in development ! Has a bugs ! */
export class VendorsComponent implements OnInit {
    @ViewChild('f') vendorForm: NgForm;
    submitted = false;
    vendor = [];
    vendor_id: Number;
    vendor_name = '';
    vendor_description:String;
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
                    this.vendor_id = 1;
                }
                else {
                    this.vendor_id = +this.vendor[this.vendor.length - 1].vendor_id + 1;
                }
                this.vendor_name = this.vendorForm.value.vendor_name;
                this.vendor_description = this.vendorForm.value.vendor_description;

            this.vendor.push({
                'vendor_id': this.vendor_id,
                'vendor_name': this.vendor_name,
                'vendor_description':this.vendor_description

            });
            localStorage.setItem("vendors", JSON.stringify(this.vendor));
            alert("Added !");
            console.log(this.vendor);
            this.vendorForm.reset();
        }

    }
    editVendor() {
        this.editMode = false;
        this.vendor_name = this.vendorForm.value.vendor_name;
        this.vendor[+this.vendor_id].vendor_name = this.vendor_name;
        this.vendor[+this.vendor_id].vendor_description = this.vendor_description;
        localStorage.setItem("vendors", JSON.stringify(this.vendor));
        alert("Edited !");

    }
    editVendorMode(id: HTMLFormElement) {
        this.editMode = true;
        this.vendor_id = +id.name;
        console.log(this.vendor_id);
        // console.log(this.vendor[1].vendor_name);
        this.vendor_name = this.vendor[+this.vendor_id].vendor_name;
        this.vendor_description = this.vendor[+this.vendor_id].vendor_description;


    }
    deleteVendor(id:HTMLFormElement){
        var confirmation = confirm("Are you sure you want to delete ?");
      if(confirmation){
        this.vendor_id = +id.name;
        this.vendor = JSON.parse(localStorage.getItem("vendors"));
        this.vendor.splice(+this.vendor_id,1);
        localStorage.setItem("vendors",JSON.stringify(this.vendor));
        this.vendorForm.reset();
        if(this.editMode){
            this.editMode=false;

        }
        alert("Deleted !");
      }
    }
    }
