webpackJsonp([11],{"4LqJ":function(e,t,r){"use strict";var a=r("/oeL");r.d(t,"a",function(){return o});var n=this&&this.__decorate||function(e,t,r,a){var n,o=arguments.length,i=o<3?t:null===a?a=Object.getOwnPropertyDescriptor(t,r):a;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)i=Reflect.decorate(e,t,r,a);else for(var s=e.length-1;s>=0;s--)(n=e[s])&&(i=(o<3?n(i):o>3?n(t,r,i):n(t,r))||i);return o>3&&i&&Object.defineProperty(t,r,i),i},o=function(){function e(){}return e.prototype.transform=function(e,t,r){if(0===e.length||""===t)return e;for(var a=[],n=0,o=e;n<o.length;n++){var i=o[n];i[r].toLowerCase().indexOf(t.toLowerCase())>=0&&a.push(i)}return a},e}();o=n([r.i(a.Pipe)({name:"filter",pure:!1})],o)},G99K:function(e,t,r){t=e.exports=r("rP7Y")(!1),t.push([e.i,"",""]),e.exports=e.exports.toString()},HujD:function(e,t){e.exports='<div class="container">\n<h1>  Category Management </h1>\n     <hr>\n    <div class="row">\n      <div class="col-lg-6" style="float: none; margin: 0 auto;">\n\n\n      <form role="form" (ngSubmit)="addCategory(f)" #f="ngForm" >\n\n\n            <fieldset class="form-group card mb-3">\n                <label class="card-header">Category Name</label>\n                <input class="form-control" ngModel name="Name" required placeholder="Enter Category Name" *ngIf="!editMode">\n                <input class="form-control" [(ngModel)]="Name" name="Name" required value="{{Name}}" *ngIf="editMode">\n\n            </fieldset>\n\n\n            <fieldset class="form-group card mb-3">\n              <label class="card-header">Category Description</label>\n              <textarea class="form-control" rows="3"\n\n              ngModel\n               name="Description"\n\n              required\n\n              placeholder="Enter Category Description"\n                *ngIf="!editMode"\n              ></textarea>\n              <textarea class="form-control" rows="3"\n\n              [(ngModel)]="Description"\n               name="Description"\n\n              required\n\n              value="{{Description}}" *ngIf="editMode"></textarea>\n          </fieldset>\n\n\n\n\n          <fieldset class="form-group card mb-3">\n            <label class="card-header">Display Order</label>\n            <input type="number" class="form-control" ngModel name="DisplayOrder" required placeholder="Enter Display Order" min="1" step="1"\n            *ngIf="!editMode">\n            <input type="number" class="form-control" [(ngModel)]="DisplayOrder" name="DisplayOrder" required placeholder="Enter Display Order" min="1" step="1"\n            *ngIf="editMode">\n        </fieldset>\n        <fieldset class="form-group card mb-3">\n            <label class="card-header">Picture</label>\n            <image-upload [max]="1" [url]="urlService.serverUrl+\'/Pictures/upload\'" [buttonCaption]="\'Select Images!\'" [extensions]="[\'jpg\',\'png\',\'gif\']" (onFileUploadFinish)="getPictureDetails($event)"></image-upload>\n        </fieldset>\n        <fieldset class="form-group card mb-3">\n            <label class="card-header">Select Parent Category (Optional)</label>\n            <select class="form-control" *ngIf="!editMode" ngModel name="parentcategory" size="4">\n              <option *ngFor="let i= index;let parentcategory of category" value={{parentcategory.Id}}>\n                {{parentcategory.Name}}\n              <hr></option>\n                    </select>\n            <select class="form-control" [(ngModel)]="ParentCategories" name="parentcategory" *ngIf="editMode" size="4">\n              <option *ngFor="let i = index;let parentcategory of category" value="{{parentcategory.Id}}" >\n                {{parentcategory.Name}}\n              <hr></option>\n            </select>\n        </fieldset>\n\n                    <button type="submit" class="btn btn-primary" [disabled]="!f.valid" *ngIf="!editMode">Add Category</button>\n                    <button type="submit" class="btn btn-primary" [disabled]="!f.valid" *ngIf="editMode">Edit Category</button>\n\n\n\n\n\n</form>\n</div>\n  </div>\n</div>\n<img [src]="loadingImagePath" *ngIf="loadingCategory" alt="">\n <hr>\n    <div class="row">\n\n\n            <div class="card mb-3">\n                <div class="card-header">\n                    <h2>Category List</h2>\n                    <input type="text" placeholder="Search Category" [(ngModel)]="filteredCategory" style="width:100%">\n                </div>\n                <div class="card-block table-responsive" style="width:100%;">\n                    <table class="table table-bordered" style="table-layout:fixed">\n                        <thead class="thead-inverse">\n                        <tr>\n\n                            <th>Category Name</th>\n                            <th>Description</th>\n\n                            <th>Display Order</th>\n                            <th>Action</th>\n\n\n\n                        </tr>\n                        </thead>\n                        <tbody>\n                          <tr *ngFor =" let i = index;let category of category | filter: filteredCategory : \'Name\' | paginate: { itemsPerPage: 10, currentPage: currentPageNumber , totalItems : totalCategories }">\n\n\n                          <td>{{category.Name}}</td>\n                          <td>{{category.Description}}</td>\n                          <td>{{category.DisplayOrder}}</td>\n                          <td><button type="button" name="{{category.Id}}" class="btn btn-primary" (click)="editCategoryMode(c)" #c><i class="fa fa-edit"></i></button>\n                          <button type="button" name="{{category.Id}}" class="btn btn-danger" (click)="deleteCategory(d)" #d><i class="fa fa-times"></i></button></td>\n\n                        </tr>\n\n                        </tbody>\n                    </table>\n                    <pagination-controls (pageChange)="currentPageNumber = $event"></pagination-controls>\n\n                </div>\n            </div>\n\n\n\n    </div>\n'},Neet:function(e,t,r){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var a=r("/oeL"),n=r("qbdv"),o=r("QhFG"),i=r("fUBq"),s=r("bm2B"),l=r("4LqJ"),c=r("hhxv"),g=r("iz04"),d=r("bO4a"),p=r("wso+");r.d(t,"CategoryModule",function(){return y});var u=this&&this.__decorate||function(e,t,r,a){var n,o=arguments.length,i=o<3?t:null===a?a=Object.getOwnPropertyDescriptor(t,r):a;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)i=Reflect.decorate(e,t,r,a);else for(var s=e.length-1;s>=0;s--)(n=e[s])&&(i=(o<3?n(i):o>3?n(t,r,i):n(t,r))||i);return o>3&&i&&Object.defineProperty(t,r,i),i},y=function(){function e(){}return e}();y=u([r.i(a.NgModule)({imports:[n.k,o.a,s.a,g.a,d.a.forRoot()],providers:[c.a,p.a],declarations:[i.a,l.a]})],y)},QhFG:function(e,t,r){"use strict";var a=r("/oeL"),n=r("BkNc"),o=r("fUBq");r.d(t,"a",function(){return l});var i=this&&this.__decorate||function(e,t,r,a){var n,o=arguments.length,i=o<3?t:null===a?a=Object.getOwnPropertyDescriptor(t,r):a;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)i=Reflect.decorate(e,t,r,a);else for(var s=e.length-1;s>=0;s--)(n=e[s])&&(i=(o<3?n(i):o>3?n(t,r,i):n(t,r))||i);return o>3&&i&&Object.defineProperty(t,r,i),i},s=[{path:"",component:o.a}],l=function(){function e(){}return e}();l=i([r.i(a.NgModule)({imports:[n.a.forChild(s)],exports:[n.a]})],l)},fUBq:function(e,t,r){"use strict";var a=r("/oeL"),n=r("bm2B"),o=r("hhxv"),i=r("wso+");r.d(t,"a",function(){return c});var s=this&&this.__decorate||function(e,t,r,a){var n,o=arguments.length,i=o<3?t:null===a?a=Object.getOwnPropertyDescriptor(t,r):a;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)i=Reflect.decorate(e,t,r,a);else for(var s=e.length-1;s>=0;s--)(n=e[s])&&(i=(o<3?n(i):o>3?n(t,r,i):n(t,r))||i);return o>3&&i&&Object.defineProperty(t,r,i),i},l=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},c=function(){function e(e,t){this.categoryService=e,this.urlService=t,this.currentPageNumber=1,this.category=[],this.currentCategory=[]}return e.prototype.ngOnInit=function(){this.PictureId=0,this.imageUrl="",this.currentPageNumber=1,this.submitted=!1,this.Name="",this.Description="",this.DisplayOrder=1,this.ParentCategory="",this.editMode=!1,this.filteredCategory="",this.loadingCategory=!1,this.loadingImagePath="../../assets/images/ajax-loader.gif",this.ParentCategoryId=0,this.getCategory()},e.prototype.addCategory=function(){var e=this;this.submitted=!0,this.loadingCategory=!0,this.editMode?this.editCategory():(0==this.category.length?this.Id=1:this.Id=+this.category[this.category.length-1].Id+1,this.Name=this.categoryForm.value.Name,this.Description=this.categoryForm.value.Description,this.DisplayOrder=this.categoryForm.value.DisplayOrder,""===this.categoryForm.value.parentcategory||null===this.categoryForm.value.parentcategory?this.ParentCategoryId=0:(this.ParentCategoryId=this.categoryForm.value.parentcategory,this.getCategoryName()),this.category.push({Id:0,CustomProperties:{"sample string 1":{},"sample string 3":{}},Name:this.Name,Description:this.Description,CategoryTemplateId:4,AvailableCategoryTemplates:[{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"},{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"}],MetaKeywords:"sample string 5",MetaDescription:"sample string 6",MetaTitle:"sample string 7",SeName:"sample string 8",ParentCategoryId:this.ParentCategoryId,PictureId:this.PictureId,PageSize:11,AllowCustomersToSelectPageSize:!0,PageSizeOptions:"sample string 13",PriceRanges:"sample string 14",ShowOnHomePage:!0,IncludeInTopMenu:!0,Published:!0,Deleted:!0,DisplayOrder:this.DisplayOrder,Locales:[{LanguageId:1,Name:"sample string 2",Description:"sample string 3",MetaKeywords:"sample string 4",MetaDescription:"sample string 5",MetaTitle:"sample string 6",SeName:"sample string 7"},{LanguageId:1,Name:"sample string 2",Description:"sample string 3",MetaKeywords:"sample string 4",MetaDescription:"sample string 5",MetaTitle:"sample string 6",SeName:"sample string 7"}],Breadcrumb:"sample string 20",SelectedCustomerRoleIds:[1,2],AvailableCustomerRoles:[{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"},{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"}],SelectedStoreIds:[1,2],AvailableStores:[{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"},{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"}],AvailableCategories:[{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"},{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"}],SelectedDiscountIds:[1,2],AvailableDiscounts:[{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"},{Disabled:!0,Group:{Disabled:!0,Name:"sample string 2"},Selected:!0,Text:"sample string 3",Value:"sample string 4"}]}),this.categoryService.storeCategory(this.category).subscribe(function(t){console.log(t),alert("Added !"),e.getCategory()},function(e){alert("Failed to add !"),console.log(e)}),this.categoryForm.reset())},e.prototype.editCategoryMode=function(e){this.editMode=!0,this.Id=+e.name,this.currentCategory=this.getCurrentCategory(this.Id)[0],this.Name=this.currentCategory.Name,this.Description=this.currentCategory.Description,this.DisplayOrder=this.currentCategory.DisplayOrder,this.ParentCategoryId=this.currentCategory.ParentCategoryId},e.prototype.editCategory=function(){var e=this;this.loadingCategory=!0,this.editMode=!1,this.Name=this.categoryForm.value.Name,this.Description=this.categoryForm.value.Description,this.DisplayOrder=this.categoryForm.value.DisplayOrder,this.ParentCategoryId=this.categoryForm.value.parentcategory,""===this.categoryForm.value.parentcategory||null===this.categoryForm.value.parentcategory?this.ParentCategoryId=0:(this.ParentCategoryId=this.categoryForm.value.parentcategory,this.getCategoryName()),console.log(this.ParentCategoryId),this.currentCategory.Name=this.Name,this.currentCategory.Description=this.Description,this.currentCategory.DisplayOrder=this.DisplayOrder,this.currentCategory.ParentCategoryId=this.ParentCategoryId,this.categoryService.updateCategory(this.currentCategory).subscribe(function(t){console.log("Category Updated"),e.getCategory(),alert("Edited !")},function(e){console.log(e),alert("Can't fetch data ! Please refresh or check your connnection !")})},e.prototype.getCategory=function(){var e=this;this.loadingCategory=!0,this.categoryService.getCategory(this.currentPageNumber).subscribe(function(t){e.categories=t.json(),e.category=e.categories.Data,e.totalCategories=e.categories.Total,e.loadingCategory=!1,console.log("Fetched Category")},function(e){console.log(e),alert("Can't fetch data ! Please refresh or check your connnection !")})},e.prototype.deleteCategory=function(e){var t=this;confirm("Are you sure you want to delete ?")&&(this.loadingCategory=!0,this.categoryService.deleteCategory(+e.name).subscribe(function(e){t.getCategory(),alert("Deleted !")},function(e){console.log(e),alert("Can't fetch data ! Please refresh or check your connnection !")})),this.editMode&&(this.editMode=!1)},e.prototype.getCurrentCategory=function(e){return this.category.filter(function(t){return t.Id==e})},e.prototype.getCategoryName=function(){if(this.ParentCategoryId>0){var e=this.getCurrentCategory(this.ParentCategoryId);this.ParentCategory=e[0].Name,this.Name=this.ParentCategory+"---\x3e"+this.Name}},e.prototype.getPictureDetails=function(e){this.PictureId=e.serverResponse.json().pictureId,this.imageUrl=e.serverResponse.json().imageUrl},e}();s([r.i(a.ViewChild)("f"),l("design:type","function"==typeof(g=void 0!==n.b&&n.b)&&g||Object)],c.prototype,"categoryForm",void 0),c=s([r.i(a.Component)({selector:"app-category",template:r("HujD"),styles:[r("G99K")]}),l("design:paramtypes",["function"==typeof(d=void 0!==o.a&&o.a)&&d||Object,"function"==typeof(p=void 0!==i.a&&i.a)&&p||Object])],c);var g,d,p},hhxv:function(e,t,r){"use strict";var a=r("/oeL"),n=r("CPp0"),o=r("wso+");r.d(t,"a",function(){return l});var i=this&&this.__decorate||function(e,t,r,a){var n,o=arguments.length,i=o<3?t:null===a?a=Object.getOwnPropertyDescriptor(t,r):a;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)i=Reflect.decorate(e,t,r,a);else for(var s=e.length-1;s>=0;s--)(n=e[s])&&(i=(o<3?n(i):o>3?n(t,r,i):n(t,r))||i);return o>3&&i&&Object.defineProperty(t,r,i),i},s=this&&this.__metadata||function(e,t){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(e,t)},l=function(){function e(e,t){this.http=e,this.urlService=t,this.Token=localStorage.getItem("Token")}return e.prototype.storeCategory=function(e){var t=new n.c({"Content-Type":"application/json",Authorization:"Token "+localStorage.getItem("Token").toUpperCase()});return this.temp=e[e.length-1],this.http.post(this.urlService.serverUrl+"/Categories/Create",this.temp,{headers:t})},e.prototype.getCategory=function(e){return this.http.post(this.urlService.serverUrl+"/Categories",{Page:1,PageSize:5e3})},e.prototype.updateCategory=function(e){var t=new n.c({"Content-Type":"application/json",Authorization:"Token "+localStorage.getItem("Token").toUpperCase()});return this.http.post(this.urlService.serverUrl+"/Categories/Update",e,{headers:t})},e.prototype.deleteCategory=function(e){var t=new n.c({"Content-Type":"application/json",Authorization:"Token "+localStorage.getItem("Token").toUpperCase()});return this.http.post(this.urlService.serverUrl+"/Categories/Delete?id="+e,null,{headers:t})},e}();l=i([r.i(a.Injectable)(),s("design:paramtypes",["function"==typeof(c=void 0!==n.b&&n.b)&&c||Object,"function"==typeof(g=void 0!==o.a&&o.a)&&g||Object])],l);var c,g}});