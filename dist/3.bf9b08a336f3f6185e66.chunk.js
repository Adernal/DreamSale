webpackJsonp([3],{"72ch":function(e,t,i){t=e.exports=i("rP7Y")(!1),t.push([e.i,".chat-panel .chat-dropdown{margin-top:-3px}.chat-panel .chat{height:350px;overflow-y:scroll;margin:0;padding:0;list-style:none}.chat-panel .chat .left img{margin-right:15px}.chat-panel .chat .right img{margin-left:15px}.chat-panel .chat li{margin-bottom:10px;margin-right:15px;padding-bottom:5px;border-bottom:1px dotted #999}.chat-panel .card-footer input{padding:3px}",""]),e.exports=e.exports.toString()},"9Ks4":function(e,t){e.exports='<div [@routerTransition]>\n    \x3c!-- <h2 class="text-muted">Dashboard <small>Statistics Overview</small></h2>\n    <div class="row">\n        <div class="col-md-12">\n            <ngb-carousel>\n                <ng-template ngbSlide *ngFor="let slider of sliders">\n                    <img class="img-fluid mx-auto d-block" [src]="slider.imagePath" alt="Random first slide" width="100%">\n                    <div class="carousel-caption">\n                        <h3>{{slider.label}}</h3>\n                        <p>{{slider.text}}</p>\n                    </div>\n                </ng-template>\n            </ngb-carousel>\n        </div>\n    </div>\n    <hr> --\x3e\n    <div class="row">\n        <div class="col-xl-3 col-lg-6">\n                <img [src]="loadingImagePath" *ngIf="showOrderCount" alt="">\n            <app-stat [bgClass]="\'card-primary\'" [icon]="\'fa-comments\'" [count]="orderCount" [label]="\'Orders\'" [routerLink]="[\'/orders\']" ></app-stat>\n        </div>\n        <div class="col-xl-3 col-lg-6">\n                <img [src]="loadingImagePath" *ngIf="showReturnRequestCount" alt="">\n            <app-stat [bgClass]="\'card-info\'" [icon]="\'fa-tasks\'" [count]="returnRequestCount" [label]="\' Return Requests\'" ></app-stat>\n        </div>\n        <div class="col-xl-3 col-lg-6">\n                <img [src]="loadingImagePath" *ngIf="showCustomerCount" alt="">\n            <app-stat [bgClass]="\'card-success\'" [icon]="\'fa-shopping-cart\'" [count]="customerCount" [label]="\'Registered Customers\'" [routerLink]="[\'/customers\']" ></app-stat>\n        </div>\n        <div class="col-xl-3 col-lg-6">\n                <img [src]="loadingImagePath" *ngIf="showLowStockCount" alt="">\n            <app-stat [bgClass]="\'card-danger\'" [icon]="\'fa-support\'" [count]="lowStockCount" [label]="\'Low Stock Products\'"></app-stat>\n        </div>\n    </div>\n    <hr />\n    \x3c!-- <ngb-alert [type]="alert.type" (close)="closeAlert(alert)" *ngFor="let alert of alerts">{{ alert.message }}</ngb-alert> --\x3e\n    <hr />\n    <div class="row">\n        <div class="col-lg-8">\n            <div class="card card-default">\n                <div class="card-header">\n                    <i class="fa fa-clock-o fa-fw"></i>Latest Orders\n                </div>\n              \n    <div class="row" >\n           \n                    \n                    <div class="card-block" style="width:100%;">\n                        <table class="table table-bordered" style="table-layout:fixed">\n                            <thead class="thead-inverse">\n                            <tr>\n    \n                                <th>Order Status</th>\n                                <th>Payment Status</th>\n                                <th>Shipping Status</th>\n                                <th>Customer</th>\n                                \n                                <th>Total</th>\n                                \x3c!-- <th>Action</th> --\x3e\n    \n    \n    \n                            </tr>\n                            </thead>\n                            <tbody>\n                            <tr *ngFor ="let order of orderList ">\n                              <td>{{order.OrderStatus}}</td>\n                              <td>{{order.PaymentStatus}}</td>\n                              <td>{{order.ShippingStatus}}</td>\n                              <td>{{order.CustomerFullName}}</td>\n            \n                              <td>{{order.OrderTotal}}</td>\n                              \x3c!-- <td><button type="button" name="{{order.Id}}" class="btn btn-primary" (click)="editOrderMode(c)" #c><i class="fa fa-edit"></i></button></td> --\x3e\n                            </tr>\n    \n    \n                            </tbody>\n                        </table>\n                     \n                  \n                    </div>\n                \n    \n    \n    \n        </div>\n            </div>\n            \x3c!-- /.card --\x3e\n        </div>\n        \x3c!-- /.col-lg-8 --\x3e\n        <div class="col-lg-4">\n            <div class="card card-default">\n                <div class="card-header">\n                    <i class="fa fa-bell fa-fw"></i> Best Sellers\n                </div>\n                <div class="row" >\n                   \n                               \n                                <div class="card-block" style="width:100%;">\n                                    <table class="table table-bordered" style="table-layout:fixed">\n                                        <thead class="thead-inverse">\n                                        <tr>\n                \n                                            <th>Name</th>\n                                            \x3c!-- <th>Action</th> --\x3e\n                \n                \n                \n                                        </tr>\n                                        </thead>\n                                        <tbody>\n                                         <tr *ngFor ="let bestSeller of bestSellerList ">\n                                          <td>{{bestSeller.ProductName}}</td>\n                                          \x3c!-- <td><button type="button" name="{{bestSeller.Id}}" class="btn btn-primary" (click)="editBestSellerMode(c)" #c><i class="fa fa-edit"></i></button></td> --\x3e\n                                        </tr>\n                 \n                \n                                        </tbody>\n                                    </table>\n                                     <img [src]="loadingImagePath" *ngIf="loadingBestSeller" alt=""> \n                                  \x3c!-- <pagination-controls (pageChange)="currentPageNumber = $event" id="BestSellerList"></pagination-controls> --\x3e\n                                </div>\n                       \n                \n                \n                \n                    </div>\n          \n            </div>\n            \x3c!-- /.card --\x3e\n\n            \x3c!-- <app-chat></app-chat> --\x3e\n            \x3c!-- /.card .chat-card --\x3e\n        </div>\n        \x3c!-- /.col-lg-4 --\x3e\n    </div>\n</div>\n'},F8yw:function(e,t,i){"use strict";var n=i("/oeL");i.d(t,"a",function(){return o});var a=this&&this.__decorate||function(e,t,i,n){var a,s=arguments.length,o=s<3?t:null===n?n=Object.getOwnPropertyDescriptor(t,i):n;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)o=Reflect.decorate(e,t,i,n);else for(var r=e.length-1;r>=0;r--)(a=e[r])&&(o=(s<3?a(o):s>3?a(t,i,o):a(t,i))||o);return s>3&&o&&Object.defineProperty(t,i,o),o},s=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},o=function(){function e(){}return e.prototype.ngOnInit=function(){},e}();o=a([i.i(n.Component)({selector:"app-chat",template:i("zm24"),styles:[i("72ch")]}),s("design:paramtypes",[])],o)},"K+6V":function(e,t,i){t=e.exports=i("rP7Y")(!1),t.push([e.i,'.timeline{position:relative;padding:20px 0 20px;list-style:none}.timeline:before{content:" ";position:absolute;top:0;bottom:0;left:50%;width:3px;margin-left:-1.5px;background-color:#eee}.timeline>li{position:relative;margin-bottom:20px}.timeline>li:after,.timeline>li:before{content:" ";display:table}.timeline>li:after{clear:both}.timeline>li>.timeline-panel{float:left;position:relative;width:46%;padding:20px;border:1px solid #d4d4d4;border-radius:2px;box-shadow:0 1px 6px rgba(0,0,0,.175)}.timeline>li>.timeline-panel:before{content:" ";display:inline-block;position:absolute;top:26px;right:-15px;border-top:15px solid transparent;border-right:0 solid #ccc;border-bottom:15px solid transparent;border-left:15px solid #ccc}.timeline>li>.timeline-panel:after{content:" ";display:inline-block;position:absolute;top:27px;right:-14px;border-top:14px solid transparent;border-right:0 solid #fff;border-bottom:14px solid transparent;border-left:14px solid #fff}.timeline>li>.timeline-badge{z-index:100;position:absolute;top:16px;left:50%;width:50px;height:50px;margin-left:-25px;border-radius:50% 50% 50% 50%;text-align:center;font-size:1.4em;line-height:50px;color:#fff;background-color:#999}.timeline>li.timeline-inverted>.timeline-panel{float:right}.timeline>li.timeline-inverted>.timeline-panel:before{right:auto;left:-15px;border-right-width:15px;border-left-width:0}.timeline>li.timeline-inverted>.timeline-panel:after{right:auto;left:-14px;border-right-width:14px;border-left-width:0}.timeline-badge.primary{background-color:#2e6da4!important}.timeline-badge.success{background-color:#3f903f!important}.timeline-badge.warning{background-color:#f0ad4e!important}.timeline-badge.danger{background-color:#d9534f!important}.timeline-badge.info{background-color:#5bc0de!important}.timeline-title{margin-top:0;color:inherit}.timeline-body>p,.timeline-body>ul{margin-bottom:0}.timeline-body>p+p{margin-top:5px}@media (max-width:767px){ul.timeline:before{left:40px}ul.timeline>li>.timeline-panel{width:calc(100% - 90px);width:-webkit-calc(100% - 90px)}ul.timeline>li>.timeline-badge{top:16px;left:15px;margin-left:0}ul.timeline>li>.timeline-panel{float:right}ul.timeline>li>.timeline-panel:before{right:auto;left:-15px;border-right-width:15px;border-left-width:0}ul.timeline>li>.timeline-panel:after{right:auto;left:-14px;border-right-width:14px;border-left-width:0}}',""]),e.exports=e.exports.toString()},SFx4:function(e,t,i){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var n=i("/oeL"),a=i("qbdv"),s=i("0WLp"),o=i("XL72"),r=i("TKqQ"),l=i("y5XU"),c=i("gOac"),d=i("jBxV"),u=i("wso+");i.d(t,"DashboardModule",function(){return m});var p=this&&this.__decorate||function(e,t,i,n){var a,s=arguments.length,o=s<3?t:null===n?n=Object.getOwnPropertyDescriptor(t,i):n;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)o=Reflect.decorate(e,t,i,n);else for(var r=e.length-1;r>=0;r--)(a=e[r])&&(o=(s<3?a(o):s>3?a(t,i,o):a(t,i))||o);return s>3&&o&&Object.defineProperty(t,i,o),o},m=function(){function e(){}return e}();m=p([i.i(n.NgModule)({imports:[a.k,s.a.forRoot(),s.b.forRoot(),o.a,c.c],declarations:[r.a,l.a,l.b,l.c],providers:[d.a,u.a]})],m)},TKqQ:function(e,t,i){"use strict";var n=i("/oeL"),a=i("5O89"),s=i("CPp0"),o=i("jBxV");i.d(t,"a",function(){return c});var r=this&&this.__decorate||function(e,t,i,n){var a,s=arguments.length,o=s<3?t:null===n?n=Object.getOwnPropertyDescriptor(t,i):n;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)o=Reflect.decorate(e,t,i,n);else for(var r=e.length-1;r>=0;r--)(a=e[r])&&(o=(s<3?a(o):s>3?a(t,i,o):a(t,i))||o);return s>3&&o&&Object.defineProperty(t,i,o),o},l=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},c=function(){function e(e,t){this.http=e,this.dashboardService=t,this.alerts=[],this.sliders=[],this.sliders.push({imagePath:"assets/images/slider1.jpg",label:"First slide label",text:"Nulla vitae elit libero, a pharetra augue mollis interdum."},{imagePath:"assets/images/slider2.jpg",label:"Second slide label",text:"Lorem ipsum dolor sit amet, consectetur adipiscing elit."},{imagePath:"assets/images/slider3.jpg",label:"Third slide label",text:"Praesent commodo cursus magna, vel scelerisque nisl consectetur."}),this.alerts.push({id:1,type:"success",message:"Lorem ipsum dolor sit amet, consectetur adipisicing elit.\n                Voluptates est animi quibusdam praesentium quam, et perspiciatis,\n                consectetur velit culpa molestias dignissimos\n                voluptatum veritatis quod aliquam! Rerum placeat necessitatibus, vitae dolorum"},{id:2,type:"warning",message:"Lorem ipsum dolor sit amet, consectetur adipisicing elit.\n                Voluptates est animi quibusdam praesentium quam, et perspiciatis,\n                consectetur velit culpa molestias dignissimos\n                voluptatum veritatis quod aliquam! Rerum placeat necessitatibus, vitae dolorum"})}return e.prototype.ngOnInit=function(){this.loadingImagePath="../../assets/images/ajax-loader.gif",this.showOrderCount=!1,this.showCustomerCount=!1,this.showLowStockCount=!1,this.showReturnRequestCount=!1,this.getOrders(),this.getCustomers(),this.getLowStockReport(),this.getReturnRequest(),this.getBestSeller()},e.prototype.closeAlert=function(e){var t=this.alerts.indexOf(e);this.alerts.splice(t,1)},e.prototype.getOrders=function(){var e=this;this.showOrderCount=!0,this.dashboardService.getOrders().subscribe(function(t){e.showOrderCount=!1,e.orderCount=t.json().Total,e.orderList=t.json().Data,console.log(e.orderCount),console.log(e.orderList)},function(t){console.log(t),e.showOrderCount=!1,alert("Can't fetch total orders ! Please refresh or check your connnection !")})},e.prototype.getCustomers=function(){var e=this;this.showCustomerCount=!0,this.dashboardService.getCustomers().subscribe(function(t){e.showCustomerCount=!1,e.customerCount=t.json().Total,console.log(e.customerCount)},function(t){console.log(t),e.showCustomerCount=!1,alert("Can't fetch total customers ! Please refresh or check your connnection !")})},e.prototype.getLowStockReport=function(){var e=this;this.showLowStockCount=!0,this.dashboardService.getLowStockReport().subscribe(function(t){e.showLowStockCount=!1,e.lowStockCount=t.json().Total,console.log(e.lowStockCount)},function(t){console.log(t),e.showLowStockCount=!1,alert("Can't fetch low stock data ! Please refresh or check your connnection !")})},e.prototype.getReturnRequest=function(){var e=this;this.showReturnRequestCount=!0,this.dashboardService.getReturnRequest().subscribe(function(t){e.showReturnRequestCount=!1,e.returnRequestCount=t.json().Total,console.log(e.returnRequestCount)},function(t){console.log(t),e.showReturnRequestCount=!1,alert("Can't fetch return request data ! Please refresh or check your connnection !")})},e.prototype.getBestSeller=function(){var e=this;this.dashboardService.getBestSeller().subscribe(function(t){e.bestSellerList=t.json().Data,console.log(e.bestSellerList)},function(e){console.log(e),alert("Can't fetch best Seller data ! Please refresh or check your connnection !")})},e}();c=r([i.i(n.Component)({selector:"app-dashboard",template:i("9Ks4"),styles:[i("b+U9")],animations:[i.i(a.a)()]}),l("design:paramtypes",["function"==typeof(d=void 0!==s.b&&s.b)&&d||Object,"function"==typeof(u=void 0!==o.a&&o.a)&&u||Object])],c);var d,u},UlOu:function(e,t,i){"use strict";var n=i("/oeL");i.d(t,"a",function(){return o});var a=this&&this.__decorate||function(e,t,i,n){var a,s=arguments.length,o=s<3?t:null===n?n=Object.getOwnPropertyDescriptor(t,i):n;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)o=Reflect.decorate(e,t,i,n);else for(var r=e.length-1;r>=0;r--)(a=e[r])&&(o=(s<3?a(o):s>3?a(t,i,o):a(t,i))||o);return s>3&&o&&Object.defineProperty(t,i,o),o},s=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},o=function(){function e(){}return e.prototype.ngOnInit=function(){},e}();o=a([i.i(n.Component)({selector:"app-timeline",template:i("vUOt"),styles:[i("K+6V")]}),s("design:paramtypes",[])],o)},XL72:function(e,t,i){"use strict";var n=i("/oeL"),a=i("BkNc"),s=i("TKqQ");i.d(t,"a",function(){return l});var o=this&&this.__decorate||function(e,t,i,n){var a,s=arguments.length,o=s<3?t:null===n?n=Object.getOwnPropertyDescriptor(t,i):n;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)o=Reflect.decorate(e,t,i,n);else for(var r=e.length-1;r>=0;r--)(a=e[r])&&(o=(s<3?a(o):s>3?a(t,i,o):a(t,i))||o);return s>3&&o&&Object.defineProperty(t,i,o),o},r=[{path:"",component:s.a}],l=function(){function e(){}return e}();l=o([i.i(n.NgModule)({imports:[a.a.forChild(r)],exports:[a.a]})],l)},"b+U9":function(e,t,i){t=e.exports=i("rP7Y")(!1),t.push([e.i,"",""]),e.exports=e.exports.toString()},jBxV:function(e,t,i){"use strict";var n=i("/oeL"),a=i("CPp0"),s=i("wso+");i.d(t,"a",function(){return l});var o=this&&this.__decorate||function(e,t,i,n){var a,s=arguments.length,o=s<3?t:null===n?n=Object.getOwnPropertyDescriptor(t,i):n;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)o=Reflect.decorate(e,t,i,n);else for(var r=e.length-1;r>=0;r--)(a=e[r])&&(o=(s<3?a(o):s>3?a(t,i,o):a(t,i))||o);return s>3&&o&&Object.defineProperty(t,i,o),o},r=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},l=function(){function e(e,t){this.http=e,this.urlService=t,this.Token=localStorage.getItem("Token"),this.headers=new a.c({"Content-Type":"application/json",Accept:"application/json",Authorization:"Token "+this.Token})}return e.prototype.getOrders=function(){return this.http.post(this.urlService.serverUrl+"/orders/OrderList",{Page:1,PageSize:10},{headers:this.headers})},e.prototype.getCustomers=function(){return this.http.post(this.urlService.serverUrl+"/customers",{SearchCustomerRoleIds:[1,2]},{headers:this.headers})},e.prototype.getLowStockReport=function(){return this.http.post(this.urlService.serverUrl+"/Products/LowStockReport",{Page:1,PageSize:1},{headers:this.headers})},e.prototype.getReturnRequest=function(){return this.http.post(this.urlService.serverUrl+"/ReturnRequest",{Command:{Page:1,PageSize:2}},{headers:this.headers})},e.prototype.getBestSeller=function(){return this.http.post(this.urlService.serverUrl+"/Orders/BestsellersReportList",{Page:1,PageSize:10},{headers:this.headers})},e}();l=o([i.i(n.Injectable)(),r("design:paramtypes",["function"==typeof(c=void 0!==a.b&&a.b)&&c||Object,"function"==typeof(d=void 0!==s.a&&s.a)&&d||Object])],l);var c,d},kZm2:function(e,t,i){"use strict";var n=i("/oeL");i.d(t,"a",function(){return o});var a=this&&this.__decorate||function(e,t,i,n){var a,s=arguments.length,o=s<3?t:null===n?n=Object.getOwnPropertyDescriptor(t,i):n;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)o=Reflect.decorate(e,t,i,n);else for(var r=e.length-1;r>=0;r--)(a=e[r])&&(o=(s<3?a(o):s>3?a(t,i,o):a(t,i))||o);return s>3&&o&&Object.defineProperty(t,i,o),o},s=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},o=function(){function e(){}return e.prototype.ngOnInit=function(){},e}();o=a([i.i(n.Component)({selector:"app-notification",template:i("rYux"),styles:[i("nmsg")]}),s("design:paramtypes",[])],o)},nmsg:function(e,t,i){t=e.exports=i("rP7Y")(!1),t.push([e.i,"",""]),e.exports=e.exports.toString()},rYux:function(e,t){e.exports='<div class="card-block">\n    <div class="list-group">\n        <a href="#" class="list-group-item clearfix d-block">\n            <i class="fa fa-comment fa-fw"></i> New Comment\n            <span class="float-right text-muted small"><em>4 minutes ago</em></span>\n        </a>\n        <a href="#" class="list-group-item clearfix d-block">\n            <i class="fa fa-twitter fa-fw"></i> 3 New Followers\n            <span class="float-right text-muted small"><em>12 minutes ago</em></span>\n        </a>\n        <a href="#" class="list-group-item clearfix d-block">\n            <i class="fa fa-envelope fa-fw"></i> Message Sent\n            <span class="float-right text-muted small"><em>27 minutes ago</em></span>\n        </a>\n        <a href="#" class="list-group-item clearfix d-block">\n            <i class="fa fa-tasks fa-fw"></i> New Task\n            <span class="float-right text-muted small"><em>43 minutes ago</em></span>\n        </a>\n        <a href="#" class="list-group-item clearfix d-block">\n            <i class="fa fa-upload fa-fw"></i> Server Rebooted\n            <span class="float-right text-muted small"><em>11:32 AM</em></span>\n        </a>\n        <a href="#" class="list-group-item clearfix d-block">\n            <i class="fa fa-bolt fa-fw"></i> Server Crashed!\n            <span class="float-right text-muted small"><em>11:13 AM</em></span>\n        </a>\n        <a href="#" class="list-group-item clearfix d-block">\n            <i class="fa fa-warning fa-fw"></i> Server Not Responding\n            <span class="float-right text-muted small"><em>10:57 AM</em></span>\n        </a>\n        <a href="#" class="list-group-item clearfix d-block">\n            <i class="fa fa-shopping-cart fa-fw"></i> New Order Placed\n            <span class="float-right text-muted small"><em>9:49 AM</em></span>\n        </a>\n        <a href="#" class="list-group-item clearfix d-block">\n            <i class="fa fa-money fa-fw"></i> Payment Received\n            <span class="float-right text-muted small"><em>Yesterday</em></span>\n        </a>\n    </div>\n    \x3c!-- /.list-group --\x3e\n    <a href="#" class="btn btn-default btn-block">View All Alerts</a>\n</div>\n'},vUOt:function(e,t){e.exports='<div class="card-block">\n    <ul class="timeline">\n        <li>\n            <div class="timeline-badge"><i class="fa fa-check"></i>\n            </div>\n            <div class="timeline-panel">\n                <div class="timeline-heading">\n                    <h4 class="timeline-title">Lorem ipsum dolor</h4>\n                    <p><small class="text-muted"><i class="fa fa-clock-o"></i> 11 hours ago via Twitter</small>\n                    </p>\n                </div>\n                <div class="timeline-body">\n                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Libero laboriosam dolor perspiciatis omnis exercitationem. Beatae, officia pariatur? Est cum veniam excepturi. Maiores praesentium, porro voluptas suscipit facere rem dicta, debitis.</p>\n                </div>\n            </div>\n        </li>\n        <li class="timeline-inverted">\n            <div class="timeline-badge warning"><i class="fa fa-credit-card"></i>\n            </div>\n            <div class="timeline-panel">\n                <div class="timeline-heading">\n                    <h4 class="timeline-title">Lorem ipsum dolor</h4>\n                </div>\n                <div class="timeline-body">\n                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Autem dolorem quibusdam, tenetur commodi provident cumque magni voluptatem libero, quis rerum. Fugiat esse debitis optio, tempore. Animi officiis alias, officia repellendus.</p>\n                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Laudantium maiores odit qui est tempora eos, nostrum provident explicabo dignissimos debitis vel! Adipisci eius voluptates, ad aut recusandae minus eaque facere.</p>\n                </div>\n            </div>\n        </li>\n        <li>\n            <div class="timeline-badge danger"><i class="fa fa-bomb"></i>\n            </div>\n            <div class="timeline-panel">\n                <div class="timeline-heading">\n                    <h4 class="timeline-title">Lorem ipsum dolor</h4>\n                </div>\n                <div class="timeline-body">\n                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repellendus numquam facilis enim eaque, tenetur nam id qui vel velit similique nihil iure molestias aliquam, voluptatem totam quaerat, magni commodi quisquam.</p>\n                </div>\n            </div>\n        </li>\n        <li class="timeline-inverted">\n            <div class="timeline-panel">\n                <div class="timeline-heading">\n                    <h4 class="timeline-title">Lorem ipsum dolor</h4>\n                </div>\n                <div class="timeline-body">\n                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates est quaerat asperiores sapiente, eligendi, nihil. Itaque quos, alias sapiente rerum quas odit! Aperiam officiis quidem delectus libero, omnis ut debitis!</p>\n                </div>\n            </div>\n        </li>\n        <li>\n            <div class="timeline-badge info"><i class="fa fa-save"></i>\n            </div>\n            <div class="timeline-panel">\n                <div class="timeline-heading">\n                    <h4 class="timeline-title">Lorem ipsum dolor</h4>\n                </div>\n                <div class="timeline-body">\n                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nobis minus modi quam ipsum alias at est molestiae excepturi delectus nesciunt, quibusdam debitis amet, beatae consequuntur impedit nulla qui! Laborum, atque.</p>\n                    <hr>\n                    <div class="btn-group">\n                        <button type="button" class="btn btn-primary btn-sm dropdown-toggle" data-toggle="dropdown">\n                            <i class="fa fa-gear"></i>  <span class="caret"></span>\n                        </button>\n                        <ul class="dropdown-menu" role="menu">\n                            <li><a href="#">Action</a>\n                            </li>\n                            <li><a href="#">Another action</a>\n                            </li>\n                            <li><a href="#">Something else here</a>\n                            </li>\n                            <li class="divider"></li>\n                            <li><a href="#">Separated link</a>\n                            </li>\n                        </ul>\n                    </div>\n                </div>\n            </div>\n        </li>\n        <li>\n            <div class="timeline-panel">\n                <div class="timeline-heading">\n                    <h4 class="timeline-title">Lorem ipsum dolor</h4>\n                </div>\n                <div class="timeline-body">\n                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sequi fuga odio quibusdam. Iure expedita, incidunt unde quis nam! Quod, quisquam. Officia quam qui adipisci quas consequuntur nostrum sequi. Consequuntur, commodi.</p>\n                </div>\n            </div>\n        </li>\n        <li class="timeline-inverted">\n            <div class="timeline-badge success"><i class="fa fa-graduation-cap"></i>\n            </div>\n            <div class="timeline-panel">\n                <div class="timeline-heading">\n                    <h4 class="timeline-title">Lorem ipsum dolor</h4>\n                </div>\n                <div class="timeline-body">\n                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Deserunt obcaecati, quaerat tempore officia voluptas debitis consectetur culpa amet, accusamus dolorum fugiat, animi dicta aperiam, enim incidunt quisquam maxime neque eaque.</p>\n                </div>\n            </div>\n        </li>\n    </ul>\n</div>\n'},y5XU:function(e,t,i){"use strict";var n=i("UlOu");i.d(t,"a",function(){return n.a});var a=i("kZm2");i.d(t,"b",function(){return a.a});var s=i("F8yw");i.d(t,"c",function(){return s.a})},zm24:function(e,t){e.exports='<div class="chat-panel card card-default">\n    <div class="card-header">\n        <i class="fa fa-comments fa-fw"></i>\n        Chat\n        <div class=" pull-right" ngbDropdown>\n            <button class="btn btn-secondary btn-sm" ngbDropdownToggle>\n                <span class="caret"></span>\n            </button>\n            <ul class="dropdown-menu dropdown-menu-right">\n                <li role="menuitem"><a class="dropdown-item" href="#">\n                    <i class="fa fa-refresh fa-fw"></i> Refresh</a>\n                </li>\n                <li role="menuitem"><a class="dropdown-item" href="#">\n                    <i class="fa fa-check-circle fa-fw"></i> Available</a>\n                </li>\n                <li role="menuitem"><a class="dropdown-item" href="#">\n                    <i class="fa fa-times fa-fw"></i> Busy</a>\n                </li>\n                <li class="divider dropdown-divider"></li>\n                <li role="menuitem">\n                    <a href="#" class="dropdown-item">\n                        <i class="fa fa-times fa-fw"></i> Busy\n                    </a>\n                </li>\n                <li>\n                    <a href="#" class="dropdown-item">\n                        <i class="fa fa-clock-o fa-fw"></i> Away\n                    </a>\n                </li>\n                <li class="divider"></li>\n                <li>\n                  <a href="#" class="dropdown-item">\n                    <i class="fa fa-sign-out fa-fw"></i> Sign Out\n                  </a>\n                </li>\n            </ul>\n        </div>\n    </div>\n    \x3c!-- /.panel-heading --\x3e\n    <div class="card-block">\n        <ul class="chat">\n            <li class="left clearfix">\n                <span class="chat-img pull-left">\n                    <img src="http://placehold.it/50/55C1E7/fff" alt="User Avatar" class="img-circle">\n                </span>\n                <div class="chat-body clearfix">\n                    <div class="header">\n                        <strong class="primary-font">Jack Sparrow</strong>\n                        <small class="pull-right text-muted">\n                            <i class="fa fa-clock-o fa-fw"></i> 12 mins ago\n                        </small>\n                    </div>\n                    <p>\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare dolor, quis ullamcorper ligula sodales.\n                    </p>\n                </div>\n            </li>\n            <li class="right clearfix">\n                <span class="chat-img pull-right">\n                    <img src="http://placehold.it/50/FA6F57/fff" alt="User Avatar" class="img-circle">\n                </span>\n                <div class="chat-body clearfix">\n                    <div class="header">\n                        <small class=" text-muted">\n                            <i class="fa fa-clock-o fa-fw"></i> 13 mins ago\n                        </small>\n                        <strong class="pull-right primary-font">Bhaumik Patel</strong>\n                    </div>\n                    <p>\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare dolor, quis ullamcorper ligula sodales.\n                    </p>\n                </div>\n            </li>\n            <li class="left clearfix">\n                <span class="chat-img pull-left">\n                    <img src="http://placehold.it/50/55C1E7/fff" alt="User Avatar" class="img-circle">\n                </span>\n                <div class="chat-body clearfix">\n                    <div class="header">\n                        <strong class="primary-font">Jack Sparrow</strong>\n                        <small class="pull-right text-muted">\n                            <i class="fa fa-clock-o fa-fw"></i> 14 mins ago\n                        </small>\n                    </div>\n                    <p>\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare dolor, quis ullamcorper ligula sodales.\n                    </p>\n                </div>\n            </li>\n            <li class="right clearfix">\n                <span class="chat-img pull-right">\n                    <img src="http://placehold.it/50/FA6F57/fff" alt="User Avatar" class="img-circle">\n                </span>\n                <div class="chat-body clearfix">\n                    <div class="header">\n                        <small class=" text-muted">\n                            <i class="fa fa-clock-o fa-fw"></i> 15 mins ago\n                        </small>\n                        <strong class="pull-right primary-font">Bhaumik Patel</strong>\n                    </div>\n                    <p>\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare dolor, quis ullamcorper ligula sodales.\n                    </p>\n                </div>\n            </li>\n        </ul>\n    </div>\n    \x3c!-- /.card-body --\x3e\n    <div class="card-footer">\n        <div class="input-group">\n            <input id="btn-input" type="text" class="form-control input-sm" placeholder="Type your message here...">\n            <span class="input-group-btn">\n                <button class="btn btn-warning btn-sm" id="btn-chat">\n                    Send\n                </button>\n            </span>\n        </div>\n    </div>\n    \x3c!-- /.card-footer --\x3e\n</div>\n'}});