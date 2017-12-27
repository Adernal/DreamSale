import { Component, OnInit , ViewChild } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';
import { ReturnRequestService } from './return-requests.service';
import {IMyDpOptions} from '../../../../node_modules/angular4-datepicker/src/my-date-picker';
import { IMyDateModel } from 'angular4-datepicker/src/my-date-picker/interfaces';

@Component({
  selector: 'app-return-requests',
  templateUrl: './return-requests.component.html',
  styleUrls: ['./return-requests.component.scss']
})
export class ReturnRequestsComponent implements OnInit {
  searchReturnRequestList: any;
  searchParameters: {};
  editMode:boolean;
  showAllReturnRequests :boolean;
  returnRequestList;
  totalReturnRequests;
  loadingReturnRequest:boolean;
  currentPageNumber:number;
  startDate;
  endDate;
  returnRequestStatusId;
  Id;
  currentReturnRequest;
  showSearchList:boolean;
  @ViewChild('c') returnRequestForm: NgForm;
  @ViewChild('u') editReturnRequestForm: NgForm;
  public myDatePickerOptions: IMyDpOptions = {
   
    dateFormat: 'dd.mm.yyyy',
};
  constructor(private http:Http,private returnRequestService: ReturnRequestService) { }

  ngOnInit() {
    this.showAllReturnRequests=true;
    this.showSearchList=false;
    this.editMode=false;
    this.loadingReturnRequest=false;
    this.getAllReturnRequests();
  }
  getAllReturnRequests(){
    this.loadingReturnRequest=true;
    this.returnRequestService.getReturnRequest()
    .subscribe(
    (response) => {
        this.loadingReturnRequest=false;
        this.returnRequestList = (response.json().Data);
        console.log(this.returnRequestList);
        this.totalReturnRequests = (response.json().Total);

    },
    (error) =>      {
            console.log(error);
            alert("Can't fetch Return Requests ! Please refresh or check your connnection !");
          }
    );
  }
  searchReturnRequest(){
    this.loadingReturnRequest=true;
    this.returnRequestStatusId=this.returnRequestForm.value.ReturnRequestStatusId;
    this.searchParameters={
   
      "StartDate": this.startDate,
      "EndDate": this.endDate,
      
      "ReturnRequestStatusId": this.returnRequestStatusId,
      
    };
    this.returnRequestService.searchReturnRequest(this.searchParameters)
    .subscribe(
    (response) => {
        this.loadingReturnRequest=false;
        // this.currentPageNumber=page;
        this.showAllReturnRequests=false;
        this.showSearchList=true;

        this.searchReturnRequestList = (response.json().Data);
        console.log(this.searchReturnRequestList);
        this.totalReturnRequests = (response.json().Total);

    },
    (error) =>      {
            console.log(error);
            alert("Can't fetch Return Requests ! Please refresh or check your connnection !");
          }
    );
    this.returnRequestForm.reset();
  }
  onStartDateChanged(event: IMyDateModel) {
    this.startDate = new Date(event.jsdate).toLocaleDateString()
}
onEndDateChanged(event: IMyDateModel) {
  this.endDate = new Date(event.jsdate).toLocaleDateString()
}
showAll(){
  this.returnRequestForm.reset();
  this.showAllReturnRequests=true;
  this.showSearchList=false;
  this.editMode=false;

}
editReturnRequestMode(id:HTMLFormElement){
  this.editMode = true;
  this.Id = +id.name;
  this.currentReturnRequest = this.getCurrentReturnRequest(this.Id)[0];
  this.returnRequestStatusId = this.currentReturnRequest["ReturnRequestStatusId"];
  
}
updateReturnRequest(){
  this.returnRequestStatusId=this.editReturnRequestForm.value.returnRequestStatusId;
  this.searchParameters={
    "Id":this.Id,
    "ReturnRequestStatusId":this.returnRequestStatusId
  }
  this.returnRequestService.updateReturnRequest(this.searchParameters)
      .subscribe(
      (data) => {
        this.loadingReturnRequest=false;
        alert('Deleted !');
        this.getAllReturnRequests();
      },
      (error) => {
        this.loadingReturnRequest=false;
        console.log(error)
        alert('Can\'t delete Return Request ! Please refresh or check your connnection !')
      }
      );

}
deleteReturnRequest(id: HTMLFormElement){
  var confirmation = confirm("Are you sure you want to delete ?");
  if (confirmation) {
    this.loadingReturnRequest=true;
    this.returnRequestService.deleteReturnRequest(+id.name)
      .subscribe(
      (data) => {
        this.loadingReturnRequest=false;
        alert('Deleted !');
        this.getAllReturnRequests();
      },
      (error) => {
        this.loadingReturnRequest=false;
        console.log(error)
        alert('Can\'t delete Return Request ! Please refresh or check your connnection !')
      }
      );
  }
}
getCurrentReturnRequest(id){
  return this.returnRequestList.filter(
    function(returnRequest){ return returnRequest.Id == id }
);
}

}
