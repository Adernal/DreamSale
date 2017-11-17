import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-product-tags',
    templateUrl: './product-tags.component.html',
    styleUrls: ['./product-tags.component.scss']
})
export class ProductTagsComponent implements OnInit {
    currentPageNumber:number=1;
    @ViewChild('f') tagForm: NgForm;
    submitted = false;
    tag = [];
    tag_id: Number;
    tag_name = '';
    tag_description: string;
    editMode = false;
    products;
    filteredTag= '';

    constructor() { }

    ngOnInit() {
        if (localStorage.getItem('tags') != null) {
        this.tag = JSON.parse(localStorage.getItem("tags"));

      }
    }
    addTag() {
        this.submitted = true;
        if (this.editMode) {
            this.editTag();
        }
        else {
          if (this.tag.length == 0) {
                    this.tag_id = 1;
                }
                else {
                    this.tag_id = +this.tag[this.tag.length - 1].tag_id + 1;
                }
            this.tag_name = this.tagForm.value.tag_name;
              this.tag_description = this.tagForm.value.tag_description;

            this.tag.push({
                'tag_id': this.tag_id,
                'tag_name': this.tag_name,
                'tag_description':this.tag_description

            });
            localStorage.setItem("tags", JSON.stringify(this.tag));
            alert("Added !");
            //  this.tags.tag=tag;
            console.log(this.tag);
            this.tagForm.reset();
        }

    }
    editTag() {
        this.editMode = false;
        this.tag_name = this.tagForm.value.tag_name;

        this.tag[+this.tag_id].tag_name = this.tag_name;
        this.tag[+this.tag_id].tag_description = this.tag_description;


        localStorage.setItem("tags", JSON.stringify(this.tag));
        alert("Edited !");

    }
    editTagMode(id: HTMLFormElement) {
        this.editMode = true;
        this.tag_id = +id.name;
        console.log(this.tag_id);
        // console.log(this.tag[1].tag_name);
        this.tag_name = this.tag[+this.tag_id].tag_name;
        this.tag_description = this.tag[+this.tag_id].tag_description;


    }
    deleteTag(id:HTMLFormElement){
        var confirmation = confirm("Are you sure you want to delete ?");
      if(confirmation){
        this.tag_id = +id.name;
        this.tag = JSON.parse(localStorage.getItem("tags"));
        this.tag.splice(+this.tag_id,1);
        localStorage.setItem("tags",JSON.stringify(this.tag));
        this.tagForm.reset();
        if(this.editMode){
            this.editMode=false;

        }
        alert("Deleted !");
      }
    }
    }
