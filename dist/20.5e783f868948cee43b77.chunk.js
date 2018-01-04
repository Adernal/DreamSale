webpackJsonp([20],{AEbG:function(e,t){e.exports='<div class="container">\n    <h1> Best Seller Management </h1>\n         <hr>\n         \x3c!-- Search Form--\x3e\n         <div class="row">\n            <div class="col-lg-6" style="float: none; margin: 0 auto;">\n            <form role="form" (ngSubmit)="searchBestSeller(c)" #c="ngForm">\n      \n                       <fieldset class="form-group card mb-3">\n                        <label class="card-header">Category</label>\n                        <select ngModel name="Category">\n                          \n                            <option *ngFor="let category of categoryList" [value]="category.Id">{{category.Name}}</option>\n                          </select>\n                    </fieldset>\n                    <fieldset class="form-group card mb-3">\n                        <label class="card-header">Manufacturer</label>\n                        <select ngModel name="Manufacturer">\n                          \n                            <option *ngFor="let manufacturer of manufacturerList" [value]="manufacturer.Id">{{manufacturer.Name}}</option>\n                          </select>\n                    </fieldset>\n                    <fieldset class="form-group card mb-3">\n                      <label for="" class="card-header">Store</label>\n                      <select ngModel name="Store">\n                        \n                        <option *ngFor="let store of storeList" [value]="store.Id">{{store.Name}}</option>\n                      </select>\n      \n                    </fieldset>\n                    <fieldset class="form-group card mb-3">\n                        <label for="" class="card-header">Vendor</label>\n                        <select ngModel name="Vendor">\n                          \n                          <option *ngFor="let vendor of vendorList" [value]="vendor.Id">{{vendor.Name}}</option>\n                        </select>\n        \n                      </fieldset>\n                      \n                            <fieldset class="form-group card mb-3">\n                                    <label class="card-header">Order Status</label>\n                                 \n                                    <select name="OrderStatus" class="form-control" ngModel >\n                                      <option value="0">Select Status</option>  \n                                      <option value="10">Pending</option>\n                                      <option value="20">Processing </option>\n                                      <option value="30">Complete</option>\n                                      <option value="40">Cancelled</option>\n                                    </select>\n                          </fieldset>\n                  \n                          \n                  \n                            <fieldset class="form-group card mb-3">\n                                      <label class="card-header">Payment Status</label>\n                                   \n                                       <select class="form-control" ngModel name="PaymentStatus">\n                                              <option value="10">Pending</option>\n                                              <option value="20">Authorized</option>\n                                              <option value="30">Paid</option>\n                                              <option value="35">Partially Refunded</option>\n                                              <option value="40">Refunded</option>\n                                              <option value="50">Voided</option>\n                                            </select> \n                            </fieldset>\n                            <fieldset  class="form-group card mb-3">\n                               <label for="" class="card-header">Start Date</label>\n                    \n                      <my-date-picker name="startDate" [options]="myDatePickerOptions" (dateChanged)="onStartDateChanged($event)"></my-date-picker>\n                     </fieldset>\n                     <fieldset class="form-group card mb-3">\n                        <label for="" class="card-header">End Date</label>\n                    \n                      <my-date-picker name="endDate" [options]="myDatePickerOptions" (dateChanged)="onEndDateChanged($event)"></my-date-picker>\n                     </fieldset>\n\n                    <button type="submit" class="btn btn-primary" [disabled]="!c.valid">Search List</button>\n                  </form>\n                    <img [src]="loadingImagePath" *ngIf="loadingBestSeller" alt="">\n                \n       \n            </div>\n           \n          </div>\n       \n    </div>\n\x3c!-- All never sold products--\x3e    \n        <div class="row" *ngIf="showAllBestSeller">\n            <div class="card mb-3">\n                    <div class="card-header">\n                      <h2>Best Sellers</h2>\n                    </div>\n                    <div class="card-block" style="width:100%;">\n                        <table class="table table-bordered" style="table-layout:fixed">\n                            <thead class="thead-inverse">\n                            <tr>\n    \n                                <th>Name</th>\n                                \x3c!-- <th>Action</th> --\x3e\n    \n    \n    \n                            </tr>\n                            </thead>\n                            <tbody>\n                             <tr *ngFor ="let bestSeller of bestSellerList | paginate: { id:\'BestSellerList\', itemsPerPage:10 , currentPage: currentPageNumber , totalItems: totalBestSellerItems}">\n                              <td>{{bestSeller.ProductName}}</td>\n                              \x3c!-- <td><button type="button" name="{{bestSeller.Id}}" class="btn btn-primary" (click)="editBestSellerMode(c)" #c><i class="fa fa-edit"></i></button></td> --\x3e\n                            </tr>\n     \n    \n                            </tbody>\n                        </table>\n                         <img [src]="loadingImagePath" *ngIf="loadingBestSeller" alt=""> \n                      <pagination-controls (pageChange)="currentPageNumber = $event" id="BestSellerList"></pagination-controls>\n                    </div>\n                </div>\n    \n    \n    \n        </div>\n\x3c!-- Search Results--\x3e\n        <div class="row" *ngIf="showSearchList">\n            <div class="card mb-3">\n                    <div class="card-header">\n                      <h2>Best Sellers</h2>\n                      <button class="btn btn-success" (click)="showAll()">Show All</button>\n                    </div>\n                    <div class="card-block" style="width:100%;">\n                        <table class="table table-bordered" style="table-layout:fixed">\n                            <thead class="thead-inverse">\n                            <tr>\n    \n                                <th>Name</th>\n                                \x3c!-- <th>Action</th> --\x3e\n    \n    \n    \n                            </tr>\n                            </thead>\n                            <tbody>\n                             <tr *ngFor ="let bestSeller of searchBestSellerList | paginate: { id:\'BestSellerList\', itemsPerPage:10 , currentPage: currentPageNumber , totalItems: totalBestSellerItems}">\n                              <td>{{bestSeller.ProductName}}</td>\n                              \x3c!-- <td><button type="button" name="{{bestSeller.Id}}" class="btn btn-primary" (click)="editBestSellerMode(c)" #c><i class="fa fa-edit"></i></button></td> --\x3e\n                            </tr>\n     \n    \n                            </tbody>\n                        </table>\n                         <img [src]="loadingImagePath" *ngIf="loadingBestSeller" alt=""> \n                      <pagination-controls (pageChange)="currentPageNumber = $event" id="BestSellerList"></pagination-controls>\n                    </div>\n                </div>\n    \n    \n    \n        </div>\n '},"HK6+":function(e,t,n){"use strict";var r=n("/oeL"),o=n("CPp0");n.d(t,"a",function(){return l});var s=this&&this.__decorate||function(e,t,n,r){var o,s=arguments.length,a=s<3?t:null===r?r=Object.getOwnPropertyDescriptor(t,n):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(e,t,n,r);else for(var l=e.length-1;l>=0;l--)(o=e[l])&&(a=(s<3?o(a):s>3?o(t,n,a):o(t,n))||a);return s>3&&a&&Object.defineProperty(t,n,a),a},a=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},l=function(){function e(e){this.http=e,this.Token=localStorage.getItem("Token"),this.headers=new o.c({"Content-Type":"application/json",Accept:"application/json",Authorization:"Token "+this.Token})}return e.prototype.getBestSeller=function(){return console.log(this.Token),this.http.post("http://denmakers2-001-site1.gtempurl.com/api/Orders/BestsellersReportList",{Page:1,PageSize:5e4},{headers:this.headers})},e.prototype.getCategory=function(){return this.http.post("http://denmakers2-001-site1.gtempurl.com/api/categories",{},{headers:this.headers})},e.prototype.getManufacturers=function(){return this.http.post("http://denmakers2-001-site1.gtempurl.com/api/Manufacturers",{},{headers:this.headers})},e.prototype.getStores=function(){return this.http.get("http://denmakers2-001-site1.gtempurl.com/api/Stores")},e.prototype.getVendors=function(){return this.http.post("http://denmakers2-001-site1.gtempurl.com/api/Vendors?showHidden=true",{},{headers:this.headers})},e.prototype.searchBestSellerItems=function(e){return this.http.post("http://denmakers2-001-site1.gtempurl.com/api/Orders/BestsellersReportList",e,{headers:this.headers})},e}();l=s([n.i(r.Injectable)(),a("design:paramtypes",["function"==typeof(i=void 0!==o.b&&o.b)&&i||Object])],l);var i},JxTg:function(e,t,n){"use strict";var r=n("/oeL"),o=n("BkNc"),s=n("zJLw");n.d(t,"a",function(){return i});var a=this&&this.__decorate||function(e,t,n,r){var o,s=arguments.length,a=s<3?t:null===r?r=Object.getOwnPropertyDescriptor(t,n):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(e,t,n,r);else for(var l=e.length-1;l>=0;l--)(o=e[l])&&(a=(s<3?o(a):s>3?o(t,n,a):o(t,n))||a);return s>3&&a&&Object.defineProperty(t,n,a),a},l=[{path:"",component:s.a}],i=function(){function e(){}return e}();i=a([n.i(r.NgModule)({imports:[o.a.forChild(l)],exports:[o.a]})],i)},Mwr1:function(e,t,n){t=e.exports=n("rP7Y")(!1),t.push([e.i,"",""]),e.exports=e.exports.toString()},i0fP:function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=n("/oeL"),o=n("qbdv"),s=n("JxTg"),a=n("zJLw"),l=n("gOac"),i=n("bm2B"),c=n("iz04"),d=n("HK6+"),u=n("DjFD");n.d(t,"BestSellerModule",function(){return p});var h=this&&this.__decorate||function(e,t,n,r){var o,s=arguments.length,a=s<3?t:null===r?r=Object.getOwnPropertyDescriptor(t,n):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(e,t,n,r);else for(var l=e.length-1;l>=0;l--)(o=e[l])&&(a=(s<3?o(a):s>3?o(t,n,a):o(t,n))||a);return s>3&&a&&Object.defineProperty(t,n,a),a},p=function(){function e(){}return e}();p=h([n.i(r.NgModule)({imports:[o.k,s.a,l.b,i.a,c.a,u.MyDatePickerModule],providers:[d.a],declarations:[a.a]})],p)},zJLw:function(e,t,n){"use strict";var r=n("/oeL"),o=n("bm2B"),s=n("HK6+");n.d(t,"a",function(){return i});var a=this&&this.__decorate||function(e,t,n,r){var o,s=arguments.length,a=s<3?t:null===r?r=Object.getOwnPropertyDescriptor(t,n):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(e,t,n,r);else for(var l=e.length-1;l>=0;l--)(o=e[l])&&(a=(s<3?o(a):s>3?o(t,n,a):o(t,n))||a);return s>3&&a&&Object.defineProperty(t,n,a),a},l=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},i=function(){function e(e){this.bestSellerService=e,this.myDatePickerOptions={dateFormat:"dd.mm.yyyy"}}return e.prototype.ngOnInit=function(){this.showAllBestSeller=!0,this.showSearchList=!1,this.loadingBestSeller=!1,this.loadingImagePath="../../assets/images/ajax-loader.gif",this.getBestSeller(),this.getCategory(),this.getManufacturers(),this.getStores(),this.getVendors()},e.prototype.getBestSeller=function(){var e=this;this.loadingBestSeller=!0,this.bestSellerService.getBestSeller().subscribe(function(t){e.loadingBestSeller=!1,e.bestSellerList=t.json().Data,e.totalBestSellerItems=t.json().Total},function(t){alert("Failed to fetch best seller items !"),e.loadingBestSeller=!1,console.log(t)})},e.prototype.getCategory=function(){var e=this;this.bestSellerService.getCategory().subscribe(function(t){e.categoryList=t.json().Data},function(e){alert("Failed to fetch category !"),console.log(e)})},e.prototype.getManufacturers=function(){var e=this;this.bestSellerService.getManufacturers().subscribe(function(t){e.manufacturerList=t.json().Data},function(e){alert("Failed to fetch manufacturers !"),console.log(e)})},e.prototype.getStores=function(){var e=this;this.bestSellerService.getStores().subscribe(function(t){e.storeList=t.json().Data},function(e){alert("Failed to fetch stores !"),console.log(e)})},e.prototype.getVendors=function(){var e=this;this.bestSellerService.getVendors().subscribe(function(t){e.vendorList=t.json().Data},function(e){alert("Failed to fetch vendors !"),console.log(e)})},e.prototype.searchBestSeller=function(){var e=this;this.loadingBestSeller=!0,this.Category=this.bestSellerForm.value.Category,this.Vendor=this.bestSellerForm.value.Vendor,this.Store=this.bestSellerForm.value.Store,this.Manufacturer=this.bestSellerForm.value.Manufacturer,this.orderStatus=this.bestSellerForm.value.OrderStatus,this.paymentStatus=this.bestSellerForm.value.PaymentStatus,console.log(this.Category),console.log(this.Vendor),console.log(this.Store),console.log(this.Manufacturer),console.log(this.startDate),console.log(this.endDate),console.log(this.orderStatus),console.log(this.paymentStatus),this.searchParameters={StartDate:this.startDate,EndDate:this.endDate,StoreId:this.Store,OrderStatusId:this.orderStatus,PaymentStatusId:this.paymentStatus,CategoryId:this.Category,ManufacturerId:this.Manufacturer,VendorId:this.Vendor,Page:1,PageSize:5e4},console.log(this.searchParameters),this.bestSellerService.searchBestSellerItems(this.searchParameters).subscribe(function(t){e.loadingBestSeller=!1,e.showAllBestSeller=!1,e.showSearchList=!0,e.searchBestSellerList=t.json().Data,console.log(e.searchBestSellerList),e.totalBestSellerItems=t.json().Total,e.bestSellerForm.reset()},function(e){alert("Failed to perform search !"),console.log(e)})},e.prototype.onStartDateChanged=function(e){this.startDate=new Date(e.jsdate).toLocaleDateString()},e.prototype.onEndDateChanged=function(e){this.endDate=new Date(e.jsdate).toLocaleDateString()},e.prototype.showAll=function(){this.showSearchList=!1,this.showAllBestSeller=!0,this.bestSellerForm.reset()},e}();a([n.i(r.ViewChild)("c"),l("design:type","function"==typeof(c=void 0!==o.b&&o.b)&&c||Object)],i.prototype,"bestSellerForm",void 0),i=a([n.i(r.Component)({selector:"app-best-seller",template:n("AEbG"),styles:[n("Mwr1")]}),l("design:paramtypes",["function"==typeof(d=void 0!==s.a&&s.a)&&d||Object])],i);var c,d}});