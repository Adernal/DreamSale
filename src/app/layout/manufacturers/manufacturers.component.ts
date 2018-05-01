import { Component, OnInit ,ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ManufacturersService } from './manufacturers.service';
import { URLService } from '../../shared/services';

@Component({
  selector: 'app-manufacturers',
  templateUrl: './manufacturers.component.html',
  styleUrls: ['./manufacturers.component.scss']
})
export class ManufacturersComponent implements OnInit {
  currentPageNumber:number=1;
  filteredManufacturer:string;
  @ViewChild('f') manufacturerForm :NgForm;
  submitted = false;
  manufacturer =[];
  Id:number;
  Name='';
  Description='';
  DisplayOrder:Number;
  editMode =false;
  showSearchField:boolean;

  manufacturers=[];
  PictureId:number;
  imageUrl:string;
  loadingManufacturer:boolean;
  loadingImagePath:string;

  constructor(private manufacturersService : ManufacturersService,private urlService:URLService) {
    this.filteredManufacturer='';
   }

  ngOnInit() {
    this.loadingImagePath='../../assets/images/ajax-loader.gif';
    this.loadingManufacturer=false;
    this.showSearchField=false;
    this.getManufacturers();
  }
  addManufacturer(){
    this.submitted=true;
    this.loadingManufacturer=true;
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
        "Id": 0,
        "Name": this.Name,
        "Description": "Test Description",
        "DisplayOrder":this.DisplayOrder

      });
    // localStorage.setItem("manufacturers",JSON.stringify(this.manufacturer));
    // alert("Added");
    this.manufacturersService.storeManufacturers(this.manufacturer)
        .subscribe(
        (response) => {
          this.getManufacturers();
          this.manufacturer=[];
          alert("Added !");

        },
        (error) =>      {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
              }
        );
  }

  }
  editManufacturerMode(id : HTMLFormElement){
    this.editMode=true;
    this.Id = +id.name;
    this.manufacturer = this.getCurrentManufacturer(+this.Id)[0];
   this.Name = this.manufacturer["Name"];
   this.DisplayOrder = this.manufacturer["DisplayOrder"];


  }
  editManufacturer(){
    this.editMode=false;
    this.loadingManufacturer=true;
    this.Name= this.manufacturerForm.value.Name;
    this.DisplayOrder= this.manufacturerForm.value.DisplayOrder;

    this.manufacturer["Name"] = this.Name;
    this.manufacturer["DisplayOrder"] = this.DisplayOrder;
    this.manufacturersService.updateManufacturer(this.manufacturer)
        .subscribe(
        (response) => {
          this.getManufacturers();
          this.manufacturer=[];
          alert("Updated !");
        },
        (error) =>      {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
              }
        );


  }
  deleteManufacturer(id:HTMLFormElement){
    var confirmation = confirm("Are you sure you want to delete ?");
    if(confirmation){
      this.loadingManufacturer=true;

      this.manufacturersService.deleteManufacturer(+id.name)
      .subscribe(
        (response)=>{
          console.log(response);
          alert("Deleted !");
          this.getManufacturers();
        },
        (error)=>console.log(error)
      );
    }
  }
  getPictureDetails(file){
        this.PictureId = file.serverResponse.json().pictureId;
        this.imageUrl = file.serverResponse.json().imageUrl;
    }
  getManufacturers(){
    this.loadingManufacturer=true;
    this.showSearchField=false;
    this.manufacturersService.getManufacturers()
    .subscribe(
      (response)=>{
        this.manufacturers = (response.json().Data);
        this.showSearchField=true;
        this.loadingManufacturer=false;
      },
      (error)=>console.log(error)
    );

  }
  getCurrentManufacturer(id:number){
    return this.manufacturers.filter(
      function(manufacturer){ return manufacturer.Id == id }
  );
  }



}
