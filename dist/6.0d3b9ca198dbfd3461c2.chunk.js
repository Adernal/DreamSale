webpackJsonp([6],{"/jnK":function(t,e,i){"use strict";var r=i("/oeL");i.d(e,"a",function(){return o});var n=this&&this.__decorate||function(t,e,i,r){var n,o=arguments.length,a=o<3?e:null===r?r=Object.getOwnPropertyDescriptor(e,i):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(t,e,i,r);else for(var s=t.length-1;s>=0;s--)(n=t[s])&&(a=(o<3?n(a):o>3?n(e,i,a):n(e,i))||a);return o>3&&a&&Object.defineProperty(e,i,a),a},o=function(){function t(){}return t.prototype.transform=function(t,e,i){if(0===t.length||""===e)return t;for(var r=[],n=0,o=t;n<o.length;n++){var a=o[n];a[i].toLowerCase().indexOf(e.toLowerCase())>=0&&r.push(a)}return r},t}();o=n([i.i(r.Pipe)({name:"filter",pure:!1})],o)},"1pZ8":function(t,e,i){e=t.exports=i("rP7Y")(!1),e.push([t.i,"",""]),t.exports=t.exports.toString()},"4/a/":function(t,e,i){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var r=i("/oeL"),n=i("qbdv"),o=i("HS/N"),a=i("90qs"),s=i("gOac"),c=i("bm2B"),l=i("/jnK"),u=i("yRSJ"),d=i("iz04"),f=i("wso+");i.d(e,"SpecificationAttributesModule",function(){return b});var p=this&&this.__decorate||function(t,e,i,r){var n,o=arguments.length,a=o<3?e:null===r?r=Object.getOwnPropertyDescriptor(e,i):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(t,e,i,r);else for(var s=t.length-1;s>=0;s--)(n=t[s])&&(a=(o<3?n(a):o>3?n(e,i,a):n(e,i))||a);return o>3&&a&&Object.defineProperty(e,i,a),a},b=function(){function t(){}return t}();b=p([i.i(r.NgModule)({imports:[n.k,o.a,s.b,c.a,d.a],providers:[u.a,f.a],declarations:[a.a,l.a]})],b)},"90qs":function(t,e,i){"use strict";var r=i("/oeL"),n=i("bm2B"),o=i("yRSJ");i.d(e,"a",function(){return c});var a=this&&this.__decorate||function(t,e,i,r){var n,o=arguments.length,a=o<3?e:null===r?r=Object.getOwnPropertyDescriptor(e,i):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(t,e,i,r);else for(var s=t.length-1;s>=0;s--)(n=t[s])&&(a=(o<3?n(a):o>3?n(e,i,a):n(e,i))||a);return o>3&&a&&Object.defineProperty(e,i,a),a},s=this&&this.__metadata||function(t,e){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(t,e)},c=function(){function t(t){this.specificationAttributeService=t,this.submitted=!1,this.attribute=[],this.editMode=!1,this.filteredSpecificationAttribute=""}return t.prototype.ngOnInit=function(){this.currentPageNumber=1,this.showAddForm=!0,this.DisplayOrder=0,this.Name="",this.getAttributes()},t.prototype.addAttribute=function(){var t=this;this.submitted=!0,this.Name=this.attributeForm.value.Name,this.DisplayOrder=this.attributeForm.value.DisplayOrder,this.attribute.push({Id:0,CustomProperties:{"sample string 1":{},"sample string 3":{}},Name:this.Name,DisplayOrder:this.DisplayOrder}),this.specificationAttributeService.storeAttributes(this.attribute).subscribe(function(e){console.log(e),alert("Added !"),t.getAttributes()},function(t){console.log(t),alert("Can't add Specification Attribute ! Please refresh or check your connnection !")}),this.attributeForm.reset()},t.prototype.editAttribute=function(){var t=this;this.editMode=!1,this.Name=this.attributeForm.value.Name,this.attribute[+this.Id].Name=this.Name,this.specificationAttributeService.updateAttributes(this.attribute,this.Id).subscribe(function(e){console.log(e),t.getAttributes(),alert("Edited !")},function(t){console.log(t),alert("Can't fetch data ! Please refresh or check your connnection !")})},t.prototype.editAttributeMode=function(t){this.editMode=!0,this.Id=+t.name,console.log(this.Id),this.Name=this.attribute[+this.Id].Name},t.prototype.getAttributes=function(){var t=this;this.specificationAttributeService.getAttributes().subscribe(function(e){t.attributes=e.json(),t.attribute=t.attributes.Data,console.log(t.attribute)},function(t){console.log(t),alert("Can't fetch data ! Please refresh or check your connnection !")})},t.prototype.deleteAttribute=function(t){var e=this;confirm("Are you sure you want to delete ?")&&(this.Id=+this.attribute[+t.name].Id,this.attribute.splice(+t.name,1),this.specificationAttributeService.deleteAttributes(this.attribute,this.Id).subscribe(function(t){console.log(t),e.getAttributes(),alert("Deleted")},function(t){console.log(t),alert("Can't fetch data ! Please refresh or check your connnection !")})),this.editMode&&(this.editMode=!1)},t}();a([i.i(r.ViewChild)("f"),s("design:type","function"==typeof(l=void 0!==n.b&&n.b)&&l||Object)],c.prototype,"attributeForm",void 0),c=a([i.i(r.Component)({selector:"app-specification-attributes",template:i("N3Q7"),styles:[i("1pZ8")]}),s("design:paramtypes",["function"==typeof(u=void 0!==o.a&&o.a)&&u||Object])],c);var l,u},"HS/N":function(t,e,i){"use strict";var r=i("/oeL"),n=i("BkNc"),o=i("90qs");i.d(e,"a",function(){return c});var a=this&&this.__decorate||function(t,e,i,r){var n,o=arguments.length,a=o<3?e:null===r?r=Object.getOwnPropertyDescriptor(e,i):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(t,e,i,r);else for(var s=t.length-1;s>=0;s--)(n=t[s])&&(a=(o<3?n(a):o>3?n(e,i,a):n(e,i))||a);return o>3&&a&&Object.defineProperty(e,i,a),a},s=[{path:"",component:o.a}],c=function(){function t(){}return t}();c=a([i.i(r.NgModule)({imports:[n.a.forChild(s)],exports:[n.a]})],c)},N3Q7:function(t,e){t.exports='<div class="container">\n<h1>  Specification Attributes Management </h1>\n     <hr>\n    \n\n        <div class="row">\n            <div class="col-lg-6" *ngIf="!editMode">\n      <form role="form" (ngSubmit)="addAttribute(f)" #f="ngForm" class="container">\n       \n            <fieldset class="form-group card mb-3">\n                <label class="card-header">Specification Attribute</label>\n                <input class="form-control" ngModel name="Name" required placeholder="Enter Attribute Name" >\n               \n                \x3c!-- <p class="help-block">Example block-level help text here.</p> --\x3e\n            </fieldset>\n            <fieldset class="form-group card mb-3">\n                <label class="card-header">Display Order</label>\n                <input class="form-control" ngModel name="DisplayOrder" required placeholder="Enter Display Order" >\n               \n                \x3c!-- <p class="help-block">Example block-level help text here.</p> --\x3e\n            </fieldset>\n\n            <button type="submit" class="btn btn-primary" [disabled]="!f.valid" >Add Attribute</button>\n          \n            \n\n    \n</form>\n</div>\n<div class="col-lg-6" *ngIf="editMode">\n    <form role="form" (ngSubmit)="editAttribute(s)" #s="ngForm" class="container">\n     \n          <fieldset class="form-group card mb-3">\n              <label class="card-header">Specification Attribute</label>\n              \n              <input class="form-control" [(ngModel)]="Name" name="Name" required value="{{Name}}">\n              <input class="form-control" [(ngModel)]="DisplayOrder" name="DisplayOrder" required value ="{{DisplayOrder}}">\n              \x3c!-- <p class="help-block">Example block-level help text here.</p> --\x3e\n          </fieldset>\n\n        \n          <button type="submit" class="btn btn-primary" [disabled]="!s.valid">Edit Attribute</button>\n          \n\n  \n</form>\n</div>\n\n\n  </div>\n  \n <hr>\n    <div class="row">\n\n\n            <div class="card mb-3">\n                <div class="card-header">\n                    <h2>Attribute List</h2>\n                    <input type="text" placeholder="Search Attribute" [(ngModel)]="filteredSpecificationAttribute" style="width:100%;">\n                </div>\n                <div class="card-block table-responsive" style="width:100%;">\n                    <table class="table table-bordered" style="table-layout:fixed">\n                        <thead class="thead-inverse">\n                        <tr>\n                            <th>Attribute Id</th>\n                            <th>Attribute Name</th>\n                            <th>Action</th>\n\n\n                        </tr>\n                        </thead>\n                        <tbody>\n                          <tr *ngFor =" let i = index;let attribute of attribute | filter: filteredSpecificationAttribute : \'Name\' ">\n\n                          <td>{{attribute.Id}}</td>\n                          <td>{{attribute.Name}}</td>\n\n                          <td><button type="button" name="{{i}}" class="btn btn-primary" (click)="editAttributeMode(c)" #c><i class="fa fa-edit"></i></button>\n                          <button type="button" name="{{i}}" class="btn btn-danger" (click)="deleteAttribute(d)" #d><i class="fa fa-times"></i></button></td>\n                        </tr>\n\n\n                        </tbody>\n                    </table>\n                    \x3c!-- <pagination-controls (pageChange)="currentPageNumber = $event"></pagination-controls> --\x3e\n                </div>\n            </div>\n\n\n\n    </div>\n</div>\n'},yRSJ:function(t,e,i){"use strict";var r=i("/oeL"),n=i("CPp0"),o=i("wso+");i.d(e,"a",function(){return c});var a=this&&this.__decorate||function(t,e,i,r){var n,o=arguments.length,a=o<3?e:null===r?r=Object.getOwnPropertyDescriptor(e,i):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)a=Reflect.decorate(t,e,i,r);else for(var s=t.length-1;s>=0;s--)(n=t[s])&&(a=(o<3?n(a):o>3?n(e,i,a):n(e,i))||a);return o>3&&a&&Object.defineProperty(e,i,a),a},s=this&&this.__metadata||function(t,e){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(t,e)},c=function(){function t(t,e){this.http=t,this.urlService=e,this.headers=new n.c({"Content-Type":"application/json",Authorization:"Token "+localStorage.getItem("Token").toUpperCase()})}return t.prototype.storeAttributes=function(t){return this.temp=t[t.length-1],this.http.post(this.urlService.serverUrl+"/SpecificationAttribute/Create",this.temp,{headers:this.headers})},t.prototype.getAttributes=function(){return this.http.post(this.urlService.serverUrl+"/SpecificationAttribute",{Page:1,PageSize:300},{headers:this.headers})},t.prototype.updateAttributes=function(t,e){return console.log(t),this.http.post(this.urlService.serverUrl+"/SpecificationAttribute/Update",t[e],{headers:this.headers})},t.prototype.deleteAttributes=function(t,e){return console.log("Id = "+e),this.http.post(this.urlService.serverUrl+"/SpecificationAttribute/Delete?id="+e,null,{headers:this.headers})},t}();c=a([i.i(r.Injectable)(),s("design:paramtypes",["function"==typeof(l=void 0!==n.b&&n.b)&&l||Object,"function"==typeof(u=void 0!==o.a&&o.a)&&u||Object])],c);var l,u}});