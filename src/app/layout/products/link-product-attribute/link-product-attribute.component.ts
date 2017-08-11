import { Component, OnInit , ViewChild } from '@angular/core';
import { LinkProductAttributeService } from './link-product-attribute.service';
import { Http } from '@angular/http';
import { NgForm  } from '@angular/forms';

@Component({
  selector: 'app-link-product-attribute',
  templateUrl: './link-product-attribute.component.html',
  styleUrls: ['./link-product-attribute.component.scss']
})
export class LinkProductAttributeComponent implements OnInit {
  @ViewChild('f') productForm: NgForm;
  products;
  product_attribute;
  product_attributes;
  specification_attributes;
  specification_attribute;
  editMode=false;
  product_id;
count;

  filteredProduct='';
  constructor(private http: Http, private linkProductService: LinkProductAttributeService) { }

  ngOnInit() {
    this.products = JSON.parse(localStorage.getItem("products"));
  }
  linkAttributeMode(id: HTMLFormElement) {
      this.editMode = true;
      this.product_id = +id.name;
      console.log(this.product_id);
      this.product_attributes = (this.products[+this.product_id].product_attributes);
      this.specification_attributes = (this.products[+this.product_id].specification_attributes);

  }
  linkAttributes(){
    this.editMode = false;
    this.count=0;
    // for(let index in this.product_attributes){
    //   this.product_attributes[index].Description=this.productForm.value.product_description;
    // }

  }
  // getAttributes() {
  //     this.linkProductService.getAttributes()
  //         .subscribe(
  //         (response) => {
  //             this.product_attributes = (response.json());
  //             this.product_attribute = this.product_attributes.Data;
  //             console.log((this.product_attribute));
  //             //  this.attribute =[this.attributes];
  //         },
  //         (error) =>      {
  //                 console.log(error);
  //                 alert("Can't fetch data ! Please refresh or check your connnection !");
  //               }
  //         );
  //
  // }
  // getSpecAttributes() {
  //     this.linkProductService.getSpecAttributes()
  //         .subscribe(
  //         (response) => {
  //             this.specification_attributes = (response.json());
  //             this.specification_attribute = this.specification_attributes.Data;
  //             console.log((this.specification_attribute));
  //             //  this.attribute =[this.attributes];
  //         },
  //         (error) =>      {
  //                 console.log(error);
  //                 alert("Can't fetch data ! Please refresh or check your connnection !");
  //               }
  //         );
  //
  // }
  //
  // getAllData() {
  //     this.getAttributes();
  //     this.getSpecAttributes();
  //
  // }
}
