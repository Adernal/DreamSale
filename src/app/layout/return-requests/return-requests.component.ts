import { Component, OnInit , ViewChild } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';
import { ReturnRequestService } from './return-requests.service';

@Component({
  selector: 'app-return-requests',
  templateUrl: './return-requests.component.html',
  styleUrls: ['./return-requests.component.scss']
})
export class ReturnRequestsComponent implements OnInit {

  showAllReturnRequests :boolean;
  returnRequestList;
  totalReturnRequests;
  loadingReturnRequest:boolean;
  currentPageNumber:number;
  constructor(private http:Http,private returnRequestService: ReturnRequestService) { }

  ngOnInit() {
    this.showAllReturnRequests=true;
    this.getAllReturnRequests();
  }
  getAllReturnRequests(){
    this.returnRequestService.getReturnRequest()
    .subscribe(
    (response) => {
        this.loadingReturnRequest=false;
        // this.currentPageNumber=page;


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
}
