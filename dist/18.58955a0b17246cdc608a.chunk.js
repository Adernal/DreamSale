webpackJsonp([18],{"2APV":function(e,t,n){"use strict";var o=n("/oeL"),r=n("CPp0");n.d(t,"a",function(){return c});var i=this&&this.__decorate||function(e,t,n,o){var r,i=arguments.length,a=i<3?t:null===o?o=Object.getOwnPropertyDescriptor(t,n):o;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(e,t,n,o);else for(var c=e.length-1;c>=0;c--)(r=e[c])&&(a=(i<3?r(a):i>3?r(t,n,a):r(t,n))||a);return i>3&&a&&Object.defineProperty(t,n,a),a},a=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},c=function(){function e(e){this.http=e,this.Token=localStorage.getItem("Token"),this.headers=new r.c({"Content-Type":"application/json",Accept:"application/json",Authorization:"Token "+this.Token})}return e.prototype.getNeverSold=function(){return this.http.post("http://denmakers-001-site1.itempurl.com/api/Orders/NeverSoldReportList",{},{headers:this.headers})},e.prototype.updateNeverSold=function(e){var t=new r.c({"Content-Type":"application/json"});return console.log(e),this.http.post("http://denmakers-001-site1.itempurl.com/api/NeverSold/EditStore?continueEditing=true",e,{headers:t})},e.prototype.deleteNeverSold=function(e){var t=new r.c({"Content-Type":"application/json"});return console.log("Id = "+e),this.http.post("http://denmakers-001-site1.itempurl.com/api/NeverSold/DeleteStore?id="+e,null,{headers:t})},e}();c=i([n.i(o.Injectable)(),a("design:paramtypes",["function"==typeof(d=void 0!==r.b&&r.b)&&d||Object])],c);var d},"2HVZ":function(e,t,n){t=e.exports=n("rP7Y")(!1),t.push([e.i,"",""]),e.exports=e.exports.toString()},"3iA6":function(e,t,n){"use strict";var o=n("/oeL"),r=n("BkNc"),i=n("a3/t");n.d(t,"a",function(){return d});var a=this&&this.__decorate||function(e,t,n,o){var r,i=arguments.length,a=i<3?t:null===o?o=Object.getOwnPropertyDescriptor(t,n):o;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(e,t,n,o);else for(var c=e.length-1;c>=0;c--)(r=e[c])&&(a=(i<3?r(a):i>3?r(t,n,a):r(t,n))||a);return i>3&&a&&Object.defineProperty(t,n,a),a},c=[{path:"",component:i.a}],d=function(){function e(){}return e}();d=a([n.i(o.NgModule)({imports:[r.a.forChild(c)],exports:[r.a]})],d)},U8DF:function(e,t){e.exports='<div class="container">\n    <h1> Never Sold Management </h1>\n         <hr>\n       \n    </div>\n    \n        <div class="row" *ngIf="showAllNeverSold">\n            <div class="card mb-3">\n                    <div class="card-header">\n                      <h2>Never Sold</h2>\n                    </div>\n                    <div class="card-block" style="width:100%;">\n                        <table class="table table-bordered" style="table-layout:fixed">\n                            <thead class="thead-inverse">\n                            <tr>\n    \n                                <th>Name</th>\n                                <th>Action</th>\n    \n    \n    \n                            </tr>\n                            </thead>\n                            <tbody>\n                            \x3c!-- <tr *ngFor ="let neverSold of neverSoldList">\n                              <td>{{neverSold.ProductName}}</td>\n                              <td>{{neverSold.Quantity}}</td>\n                              <td>{{neverSold.CustomerInfo}}</td>\n                              <td>{{neverSold.OrderId}}</td>\n                              <td>{{neverSold.NeverSoldStatusStr}}</td>\n                              <td>{{neverSold.CreatedOn | date}}</td>\n                              <td><button type="button" name="{{neverSold.Id}}" class="btn btn-primary" (click)="editNeverSoldMode(c)" #c><i class="fa fa-edit"></i></button></td>\n                            </tr>\n     --\x3e\n    \n                            </tbody>\n                        </table>\n                         <img [src]="loadingImagePath" *ngIf="loadingOrder" alt=""> \n                      <pagination-controls (pageChange)="getAllNeverSold($event)" id="NeverSold"></pagination-controls>\n                    </div>\n                </div>\n    \n    \n    \n        </div>\n '},"a3/t":function(e,t,n){"use strict";var o=n("/oeL"),r=n("2APV");n.d(t,"a",function(){return c});var i=this&&this.__decorate||function(e,t,n,o){var r,i=arguments.length,a=i<3?t:null===o?o=Object.getOwnPropertyDescriptor(t,n):o;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(e,t,n,o);else for(var c=e.length-1;c>=0;c--)(r=e[c])&&(a=(i<3?r(a):i>3?r(t,n,a):r(t,n))||a);return i>3&&a&&Object.defineProperty(t,n,a),a},a=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},c=function(){function e(e){this.neverSoldService=e}return e.prototype.ngOnInit=function(){this.showAllNeverSold=!0,this.getNeverSold()},e.prototype.getNeverSold=function(){var e=this;this.neverSoldService.getNeverSold().subscribe(function(t){console.log(t),e.best_sellers=t.json().Data},function(e){alert("Failed to fetch never sold items !"),console.log(e)})},e}();c=i([n.i(o.Component)({selector:"app-never-sold",template:n("U8DF"),styles:[n("2HVZ")]}),a("design:paramtypes",["function"==typeof(d=void 0!==r.a&&r.a)&&d||Object])],c);var d},zWE1:function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var o=n("/oeL"),r=n("qbdv"),i=n("3iA6"),a=n("a3/t"),c=n("gOac"),d=n("bm2B"),l=n("iz04"),s=n("2APV");n.d(t,"NeverSoldModule",function(){return p});var f=this&&this.__decorate||function(e,t,n,o){var r,i=arguments.length,a=i<3?t:null===o?o=Object.getOwnPropertyDescriptor(t,n):o;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(e,t,n,o);else for(var c=e.length-1;c>=0;c--)(r=e[c])&&(a=(i<3?r(a):i>3?r(t,n,a):r(t,n))||a);return i>3&&a&&Object.defineProperty(t,n,a),a},p=function(){function e(){}return e}();p=f([n.i(o.NgModule)({imports:[r.k,i.a,c.b,d.a,l.a],providers:[s.a],declarations:[a.a]})],p)}});