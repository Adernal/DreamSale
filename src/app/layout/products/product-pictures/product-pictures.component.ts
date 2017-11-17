import { Component, OnInit, ViewChild, Input, ElementRef } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';
import { ProductPicturesService } from './product-pictures.service';

@Component({
  selector: 'app-product-pictures',
  templateUrl: './product-pictures.component.html',
  styleUrls: ['./product-pictures.component.scss']
})
export class ProductPicturesComponent implements OnInit {

  loadingImagePath:string;
  @Input('ProductId') Id;
  @ViewChild('p') pictureForm: NgForm;
  currentPageNumber:number;
  loadingPicture:boolean;
  pictureList;
  pictureId:number;
  pictureDisplayOrder:number;
  imageUrl:string;
  currentPicture=[];
  constructor(private http: Http, private productPicturesService: ProductPicturesService) {
      this.currentPageNumber=1;
      this.loadingPicture=false;
      this.loadingImagePath = '../../../assets/images/ajax-loader.gif';
      this.pictureDisplayOrder=0;
      this.pictureId=1;
      this.imageUrl='';
  }


  ngOnInit() {
    console.log(this.Id);
    this.getPicture();
  }
  addPicture(){
    this.loadingPicture=true;
     this.pictureDisplayOrder = this.pictureForm.value.DisplayOrder;
       this.currentPicture.push({
    "Id": 0,
    "CustomProperties": {
      "sample string 1": {},
      "sample string 3": {}
    },
    "ProductId": this.Id,
    "PictureId": this.pictureId,
    "PictureUrl": this.imageUrl,
    "DisplayOrder": this.pictureDisplayOrder,
    "OverrideAltAttribute": "sample string 6",
    "OverrideTitleAttribute": "sample string 7"
  });
       this.productPicturesService.addPicture(this.currentPicture)
       .subscribe(
       (response) => {
         this.loadingPicture=false;
           alert("Added !");
           this.getPicture();
           //this.getPicture();
           this.pictureForm.reset();
           this.currentPicture=[];
       },
       (error) =>      {
               console.log(error);
               alert("Can't fetch data ! Please refresh or check your connnection !");
             }
       );
  }
  getPicture(){
    console.log("New get picture function called !");
    this.loadingPicture=true;
      this.productPicturesService.getPicture(this.Id)
      .subscribe(
      (response) => {

          this.pictureList = (response.json().Data);
          this.loadingPicture=false;
          console.log(this.pictureList);
      },
      (error) =>      {
              console.log(error);
              alert("Can't fetch data ! Please refresh or check your connnection !");
            }
      );
  }
  deletePicture(id:HTMLFormElement){
      var confirmation = confirm("Are you sure you want to delete ?");
      if (confirmation) {
        this.loadingPicture=true;
        this.productPicturesService.deletePicture(+id.name)
          .subscribe(
          (data) => {
            this.loadingPicture=false;
            alert('Deleted !');
            this.pictureForm.reset();
            this.getPicture();
          },
          (error) => {
            console.log(error)
            alert('Can\'t fetch data ! Please refresh or check your connnection !')
          }
          );
      }
  }

  getPictureDetails(file){
        this.pictureId = file.serverResponse.json().pictureId;
        this.imageUrl = file.serverResponse.json().imageUrl;
    }


}
