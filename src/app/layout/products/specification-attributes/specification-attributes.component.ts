import { Component, OnInit , ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SpecificationAttributesService } from './specification-attributes.service';

@Component({
  selector: 'app-specification-attributes',
  templateUrl: './specification-attributes.component.html',
  styleUrls: ['./specification-attributes.component.scss']
})
export class SpecificationAttributesComponent implements OnInit {
  @ViewChild('f') attributeForm: NgForm;
  submitted = false;
  attribute = [];
  Id: Number;
  Name = '';
  editMode = false;
  filteredSpecificationAttribute='';
  attributes;
  constructor(private specificationAttributeService : SpecificationAttributesService) { }

  ngOnInit() {
    //localStorage.removeItem("spec-attributes");
    this.getAttributes();
  }
  addAttribute() {
        this.submitted = true;
        if (this.editMode) {
            this.editAttribute();
        }
        else {
          if(this.attribute.length==0){
            this.Id =1;
          }
          else{
              this.Id = +this.attribute[this.attribute.length-1].Id+1;
          }


            this.Name = this.attributeForm.value.Name;

            this.attribute.push({
              "Id": this.Id,
              "CustomProperties": {
                "sample string 1": {},
                "sample string 3": {}
              },
              "Name": this.Name,
              "DisplayOrder": 3,
              "Locales": [
                {
                  "LanguageId": 1,
                  "Name": "sample string 2"
                },
                {
                  "LanguageId": 1,
                  "Name": "sample string 2"
                }
              ]
            }
);
            //localStorage.setItem("spec-attributes", JSON.stringify(this.attribute));
            //  this.attributes.attribute=attribute;

            this.specificationAttributeService.storeAttributes(this.attribute)
            .subscribe(
              (data)=>{
                console.log(data);
                alert("Added !");
                this.getAttributes();
              },
              (error)=>{
                      console.log(error);
                      alert("Can't fetch data ! Please refresh or check your connnection !");
                    }
            );
            this.attributeForm.reset();


        }

    }
    editAttribute() {
        this.editMode = false;
        this.Name = this.attributeForm.value.Name;

        this.attribute[+this.Id].Name = this.Name;


        // localStorage.setItem("spec-attributes", JSON.stringify(this.attribute));
        this.specificationAttributeService.updateAttributes(this.attribute,this.Id)
        .subscribe(
          (data)=>{
            console.log(data);

            this.getAttributes();
              alert("Edited !");
          },
          (error)=>      {
                  console.log(error);
                  alert("Can't fetch data ! Please refresh or check your connnection !");
                }
        );

    }
    editAttributeMode(id: HTMLFormElement) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        // console.log(this.attribute[1].Name);
        this.Name = this.attribute[+this.Id].Name;


    }
    getAttributes(){
      this.specificationAttributeService.getAttributes()
      .subscribe(
        (response)=>{
          this.attributes = (response.json());
          this.attribute = this.attributes.Data;
          console.log((this.attribute));
        //  this.attribute =[this.attributes];
        },
        (error)=>      {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
              }
      );

    }
    deleteAttribute(id:HTMLFormElement){
      var confirmation = confirm("Are you sure you want to delete ?");
      if(confirmation){
        this.Id = +this.attribute[+id.name].Id;

        this.attribute.splice(+id.name,1);

        //localStorage.setItem("attributes",JSON.stringify(this.attribute));
        this.specificationAttributeService.deleteAttributes(this.attribute,this.Id)
        .subscribe(
          (data)=>{
            console.log(data);

            this.getAttributes();
            alert("Deleted");
          },
          (error)=>{
                  console.log(error);
                  alert("Can't fetch data ! Please refresh or check your connnection !");
                }
        );

      }
      if(this.editMode){
              this.editMode=false;

          }
    }

}