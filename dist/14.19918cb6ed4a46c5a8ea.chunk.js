webpackJsonp([14],{"+oCg":function(t,e){t.exports=""},RoY6:function(t,e,n){e=t.exports=n("rP7Y")(!1),e.push([t.i,"",""]),t.exports=t.exports.toString()},VrPv:function(t,e,n){"use strict";var r=n("/oeL"),o=n("BkNc"),i=n("XRH+");n.d(e,"a",function(){return a});var c=this&&this.__decorate||function(t,e,n,r){var o,i=arguments.length,c=i<3?e:null===r?r=Object.getOwnPropertyDescriptor(e,n):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)c=Reflect.decorate(t,e,n,r);else for(var u=t.length-1;u>=0;u--)(o=t[u])&&(c=(i<3?o(c):i>3?o(e,n,c):o(e,n))||c);return i>3&&c&&Object.defineProperty(e,n,c),c},u=[{path:"",component:i.a}],a=function(){function t(){}return t}();a=c([n.i(r.NgModule)({imports:[o.a.forChild(u)],exports:[o.a]})],a)},"XRH+":function(t,e,n){"use strict";var r=n("/oeL");n.d(e,"a",function(){return c});var o=this&&this.__decorate||function(t,e,n,r){var o,i=arguments.length,c=i<3?e:null===r?r=Object.getOwnPropertyDescriptor(e,n):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)c=Reflect.decorate(t,e,n,r);else for(var u=t.length-1;u>=0;u--)(o=t[u])&&(c=(i<3?o(c):i>3?o(e,n,c):o(e,n))||c);return i>3&&c&&Object.defineProperty(e,n,c),c},i=this&&this.__metadata||function(t,e){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(t,e)},c=function(){function t(){}return t.prototype.ngOnInit=function(){},t}();c=o([n.i(r.Component)({selector:"app-return-requests",template:n("+oCg"),styles:[n("RoY6")]}),i("design:paramtypes",[])],c)},XfN7:function(t,e,n){"use strict";var r=n("/oeL"),o=n("CPp0");n.d(e,"a",function(){return u});var i=this&&this.__decorate||function(t,e,n,r){var o,i=arguments.length,c=i<3?e:null===r?r=Object.getOwnPropertyDescriptor(e,n):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)c=Reflect.decorate(t,e,n,r);else for(var u=t.length-1;u>=0;u--)(o=t[u])&&(c=(i<3?o(c):i>3?o(e,n,c):o(e,n))||c);return i>3&&c&&Object.defineProperty(e,n,c),c},c=this&&this.__metadata||function(t,e){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(t,e)},u=function(){function t(t){this.http=t}return t.prototype.storeReturnRequest=function(t){var e=new o.c({"Content-Type":"application/json"});return this.temp=t[t.length-1],this.http.post("http://denmakers-001-site1.itempurl.com/api/ReturnRequest/CreateStore?continueEditing=true",this.temp,{headers:e})},t.prototype.getReturnRequest=function(){return this.http.get("http://denmakers-001-site1.itempurl.com/api/ReturnRequest")},t.prototype.updateReturnRequest=function(t){var e=new o.c({"Content-Type":"application/json"});return console.log(t),this.http.post("http://denmakers-001-site1.itempurl.com/api/ReturnRequest/EditStore?continueEditing=true",t,{headers:e})},t.prototype.deleteReturnRequest=function(t){var e=new o.c({"Content-Type":"application/json"});return console.log("Id = "+t),this.http.post("http://denmakers-001-site1.itempurl.com/api/ReturnRequest/DeleteStore?id="+t,null,{headers:e})},t}();u=i([n.i(r.Injectable)(),c("design:paramtypes",["function"==typeof(a=void 0!==o.b&&o.b)&&a||Object])],u);var a},uSwF:function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var r=n("/oeL"),o=n("qbdv"),i=n("VrPv"),c=n("XRH+"),u=n("gOac"),a=n("bm2B"),p=n("XfN7");n.d(e,"ReturnRequestsModule",function(){return f});var s=this&&this.__decorate||function(t,e,n,r){var o,i=arguments.length,c=i<3?e:null===r?r=Object.getOwnPropertyDescriptor(e,n):r;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)c=Reflect.decorate(t,e,n,r);else for(var u=t.length-1;u>=0;u--)(o=t[u])&&(c=(i<3?o(c):i>3?o(e,n,c):o(e,n))||c);return i>3&&c&&Object.defineProperty(e,n,c),c},f=function(){function t(){}return t}();f=s([n.i(r.NgModule)({imports:[o.k,i.a,u.b,a.a],providers:[p.a],declarations:[c.a]})],f)}});