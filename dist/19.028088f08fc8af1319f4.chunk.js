webpackJsonp([19],{"9vVq":function(e,t){e.exports='<div class="container">\n<h1> Customer Role Management </h1>\n     <hr>\n    <div class="row">\n\n\n      <form role="form" (ngSubmit)="addCustomerRole(f)" #f="ngForm" class="container">\n        <div class="row">\n          <div class="col-lg-6">\n            <fieldset class="form-group card mb-3">\n                <label class="card-header">Customer Role</label>\n                <input class="form-control" ngModel name="Name" required placeholder="Enter Customer Role Name" *ngIf="!editMode">\n                <input class="form-control" [(ngModel)]="Name" name="Name" required value="{{Name}}" *ngIf="editMode">\n                \x3c!-- <p class="help-block">Example block-level help text here.</p> --\x3e\n            </fieldset>\n            <fieldset class="form-group card mb-3">\n            <label class="card-header">Free Shipping</label>\n              <div *ngIf="!editMode" class="checkbox" style="float:left; margin:5px;">\n\n                <input type="checkbox" class="form-control" ngModel name="FreeShipping">\n              </div>\n\n              <div *ngIf="editMode"  class="checkbox" style="float:left; margin:5px;">\n                <input type="checkbox" class="form-control" [(ngModel)]="FreeShipping" name="Active">\n              </div>\n\n            </fieldset>\n            <fieldset class="form-group card mb-3">\n            <label class="card-header">Tax Exempt</label>\n              <div *ngIf="!editMode" class="checkbox" style="float:left; margin:5px;">\n\n                <input type="checkbox" class="form-control" ngModel name="TaxExempt">\n              </div>\n\n              <div *ngIf="editMode"  class="checkbox" style="float:left; margin:5px;">\n                <input type="checkbox" class="form-control" [(ngModel)]="TaxExempt" name="Active">\n              </div>\n\n            </fieldset>\n\n\n            <button type="submit" class="btn btn-primary" [disabled]="!f.valid" *ngIf="!editMode">Add Customer Role</button>\n            <button type="submit" class="btn btn-primary" [disabled]="!f.valid" *ngIf="editMode">Edit Customer Role</button>\n            \n\n        </div>\n        <div class="col-lg-6">\n\n            <fieldset class="form-group card mb-3">\n            <label class="card-header">Active</label>\n              <div *ngIf="!editMode" class="checkbox" style="float:left; margin:5px;">\n\n                <input type="checkbox" class="form-control" ngModel name="Active">\n              </div>\n\n              <div *ngIf="editMode"  class="checkbox" style="float:left; margin:5px;">\n                <input type="checkbox" class="form-control" [(ngModel)]="Active" name="Active">\n              </div>\n\n            </fieldset>\n            <fieldset class="form-group card mb-3">\n            <label class="card-header">Is System Role</label>\n              <div *ngIf="!editMode" class="checkbox" style="float:left; margin:5px;">\n\n                <input type="checkbox" class="form-control" ngModel name="IsSystemRole">\n              </div>\n\n              <div *ngIf="editMode"  class="checkbox" style="float:left; margin:5px;">\n                <input type="checkbox" class="form-control" [(ngModel)]="IsSystemRole" name="Active">\n              </div>\n\n            </fieldset>\n\n\n\n        </div>\n\n\n  </div>\n\n\n</form>\n  </div>\n  </div>\n <hr>\n    <div class="row">\n\n\n            <div class="card mb-3">\n                <div class="card-header">\n                  <h2>Customer Role List</h2>\n                  \x3c!-- <input type="text" placeholder="Search By Name or Email" [(ngModel)]="filteredCustomer Role" style="width:100%;"> --\x3e\n                </div>\n                <div class="card-block" style="width:100%;">\n                    <table class="table table-bordered" style="table-layout:fixed">\n                        <thead class="thead-inverse">\n                        <tr>\n\n                            <th>Customer Role</th>\n                            <th>Free Shipping</th>\n                            <th>Tax Exempt</th>\n                            <th>Active</th>\n                            <th>Is System Role</th>\n                            <th>Action</th>\n\n\n                        </tr>\n                        </thead>\n                        <tbody>\n                          <tr *ngFor ="let customer_role of customer_role " >\n\n\n                          <td>{{customer_role.Name}}</td>\n                          <td>{{customer_role.FreeShipping}}</td>\n                          <td>{{customer_role.TaxExempt}}</td>\n                          <td>{{customer_role.Active}}</td>\n                          <td>{{customer_role.IsSystemRole}}</td>\n\n\n\n\n                          <td><button type="button" name="{{customer_role.Id}}" class="btn btn-primary" (click)="editCustomerRoleMode(c)" #c><i class="fa fa-edit"></i></button>\n                          <button type="button" name="{{customer_role.Id}}" class="btn btn-danger" (click)="deleteCustomerRole(d)" #d><i class="fa fa-times"></i></button></td>\n                        </tr>\n\n                        </tbody>\n                    </table>\n                  \n                </div>\n            </div>\n\n\n\n    </div>\n'},CDez:function(e,t,o){"use strict";var r=o("/oeL"),n=o("bm2B"),s=o("ksX+");o.d(t,"a",function(){return c});var i=this&&this.__decorate||function(e,t,o,r){var n,s=arguments.length,i=s<3?t:null===r?r=Object.getOwnPropertyDescriptor(t,o):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)i=Reflect.decorate(e,t,o,r);else for(var l=e.length-1;l>=0;l--)(n=e[l])&&(i=(s<3?n(i):s>3?n(t,o,i):n(t,o))||i);return s>3&&i&&Object.defineProperty(t,o,i),i},l=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},c=function(){function e(e){this.customer_roleService=e,this.currentPageNumber=1,this.editMode=!1,this.customer_role=[],this.currentCustomerRole=[]}return e.prototype.ngOnInit=function(){this.getCustomerRoles()},e.prototype.addCustomerRole=function(){this.submitted=!0,this.editMode?this.editCustomerRole():(0==this.customer_role.length?this.Id=1:this.Id=+this.customer_role[this.customer_role.length-1].Id+1,this.Name=this.customer_roleForm.value.Name,this.FreeShipping=this.customer_roleForm.value.FreeShipping,this.FreeShipping||(this.FreeShipping=!1),this.TaxExempt=this.customer_roleForm.value.TaxExempt,this.TaxExempt||(this.TaxExempt=!1),this.Active=this.customer_roleForm.value.Active,this.Active||(this.Active=!1),this.IsSystemRole=this.customer_roleForm.value.IsSystemRole,this.IsSystemRole||(this.IsSystemRole=!1),this.customer_role.push({Id:this.Id,Name:this.Name,FreeShipping:this.FreeShipping,TaxExempt:this.TaxExempt,Active:this.Active,IsSystemRole:this.IsSystemRole,SystemName:"sample string 7",EnablePasswordLifetime:!0,PurchasedWithProductId:9,PurchasedWithProductName:"sample string 10",CustomProperties:{"sample string 1":{},"sample string 3":{}}}),this.customer_roleService.storeCustomerRole(this.customer_role).subscribe(function(e){alert("Added !")},function(e){alert("Failed to add !"),console.log(e)}),console.log(this.customer_role),this.customer_roleForm.reset())},e.prototype.getCustomerRoles=function(){var e=this;this.customer_roleService.getCustomerRole().subscribe(function(t){e.customer_roles=t.json(),e.customer_role=e.customer_roles.Data},function(e){console.log(e),alert("Can't fetch data ! Please refresh or check your connnection !")})},e.prototype.editCustomerRoleMode=function(e){this.editMode=!0,this.Id=+e.name,this.currentCustomerRole=this.getCustomerRoleIndex(this.Id)[0],this.Name=this.currentCustomerRole.Name,this.FreeShipping=this.currentCustomerRole.FreeShipping,this.TaxExempt=this.currentCustomerRole.TaxExempt,this.Active=this.currentCustomerRole.Active,this.IsSystemRole=this.currentCustomerRole.IsSystemRole},e.prototype.editCustomerRole=function(){var e=this;this.editMode=!1,this.Name=this.customer_roleForm.value.Name,this.FreeShipping=this.customer_roleForm.value.FreeShipping,this.FreeShipping||(this.FreeShipping=!1),this.TaxExempt=this.customer_roleForm.value.TaxExempt,this.TaxExempt||(this.TaxExempt=!1),this.Active=this.customer_roleForm.value.Active,this.Active||(this.Active=!1),this.IsSystemRole=this.customer_roleForm.value.IsSystemRole,this.IsSystemRole||(this.IsSystemRole=!1),this.currentCustomerRole.Name=this.Name,this.currentCustomerRole.FreeShipping=this.FreeShipping,this.currentCustomerRole.TaxExempt=this.TaxExempt,this.currentCustomerRole.Active=this.Active,this.currentCustomerRole.IsSystemRole=this.IsSystemRole,this.customer_roleService.updateCustomerRole(this.currentCustomerRole).subscribe(function(t){console.log(t.json()),e.getCustomerRoles(),alert("Edited !")},function(e){console.log(e),alert("Can't fetch data ! Please refresh or check your connnection !")})},e.prototype.deleteCustomerRole=function(e){var t=this;confirm("Are you sure you want to delete ?")&&this.customer_roleService.deleteCustomerRole(+e.name).subscribe(function(e){console.log(e),t.getCustomerRoles(),alert("Deleted !")},function(e){console.log(e),alert("Can't fetch data ! Please refresh or check your connnection !")}),this.editMode&&(this.editMode=!1)},e.prototype.getCustomerRoleIndex=function(e){return this.customer_role.filter(function(t){return t.Id==e})},e}();i([o.i(r.ViewChild)("f"),l("design:type","function"==typeof(a=void 0!==n.b&&n.b)&&a||Object)],c.prototype,"customer_roleForm",void 0),c=i([o.i(r.Component)({selector:"app-customer-roles",template:o("9vVq"),styles:[o("K5nz")]}),l("design:paramtypes",["function"==typeof(u=void 0!==s.a&&s.a)&&u||Object])],c);var a,u},K5nz:function(e,t,o){t=e.exports=o("rP7Y")(!1),t.push([e.i,"",""]),e.exports=e.exports.toString()},cfh5:function(e,t,o){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=o("/oeL"),n=o("qbdv"),s=o("oQ6t"),i=o("CDez"),l=o("gOac"),c=o("bm2B"),a=o("ksX+"),u=o("iz04"),d=o("wso+");o.d(t,"CustomerRolesModule",function(){return h});var m=this&&this.__decorate||function(e,t,o,r){var n,s=arguments.length,i=s<3?t:null===r?r=Object.getOwnPropertyDescriptor(t,o):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)i=Reflect.decorate(e,t,o,r);else for(var l=e.length-1;l>=0;l--)(n=e[l])&&(i=(s<3?n(i):s>3?n(t,o,i):n(t,o))||i);return s>3&&i&&Object.defineProperty(t,o,i),i},h=function(){function e(){}return e}();h=m([o.i(r.NgModule)({imports:[n.k,s.a,l.b,c.a,u.a],providers:[a.a,d.a],declarations:[i.a]})],h)},"ksX+":function(e,t,o){"use strict";var r=o("/oeL"),n=o("CPp0"),s=o("wso+");o.d(t,"a",function(){return c});var i=this&&this.__decorate||function(e,t,o,r){var n,s=arguments.length,i=s<3?t:null===r?r=Object.getOwnPropertyDescriptor(t,o):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)i=Reflect.decorate(e,t,o,r);else for(var l=e.length-1;l>=0;l--)(n=e[l])&&(i=(s<3?n(i):s>3?n(t,o,i):n(t,o))||i);return s>3&&i&&Object.defineProperty(t,o,i),i},l=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},c=function(){function e(e,t){this.http=e,this.urlService=t}return e.prototype.storeCustomerRole=function(e){var t=new n.c({"Content-Type":"application/json"});return this.temp=e[e.length-1],this.http.post(this.urlService.serverUrl+"/customers/CreateCustomerRole",this.temp,{headers:t})},e.prototype.getCustomerRole=function(){return this.http.get(this.urlService.serverUrl+"/customers/Roles")},e.prototype.updateCustomerRole=function(e){var t=new n.c({"Content-Type":"application/json"});return console.log(e),this.http.post(this.urlService.serverUrl+"/customers/UpdateCustomerRole",e,{headers:t})},e.prototype.deleteCustomerRole=function(e){var t=new n.c({"Content-Type":"application/json"});return this.http.post(this.urlService.serverUrl+"/customers/DeleteCustomerRole?id="+e,null,{headers:t})},e}();c=i([o.i(r.Injectable)(),l("design:paramtypes",["function"==typeof(a=void 0!==n.b&&n.b)&&a||Object,"function"==typeof(u=void 0!==s.a&&s.a)&&u||Object])],c);var a,u},oQ6t:function(e,t,o){"use strict";var r=o("/oeL"),n=o("BkNc"),s=o("CDez");o.d(t,"a",function(){return c});var i=this&&this.__decorate||function(e,t,o,r){var n,s=arguments.length,i=s<3?t:null===r?r=Object.getOwnPropertyDescriptor(t,o):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)i=Reflect.decorate(e,t,o,r);else for(var l=e.length-1;l>=0;l--)(n=e[l])&&(i=(s<3?n(i):s>3?n(t,o,i):n(t,o))||i);return s>3&&i&&Object.defineProperty(t,o,i),i},l=[{path:"",component:s.a}],c=function(){function e(){}return e}();c=i([o.i(r.NgModule)({imports:[n.a.forChild(l)],exports:[n.a]})],c)}});