"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var product_attributes_service_1 = require("./product-attributes.service");
var ProductAttributesComponent = (function () {
    function ProductAttributesComponent(attributeService) {
        this.attributeService = attributeService;
        this.submitted = false;
        this.attribute = [];
        this.Name = '';
        this.temp = [];
        this.editMode = false;
        this.filteredAttribute = '';
    }
    ProductAttributesComponent.prototype.ngOnInit = function () {
        // localStorage.removeItem("attributes");
        //this.attribute = JSON.parse(localStorage.getItem("attributes"));
        this.getAttributes();
    };
    ProductAttributesComponent.prototype.addAttribute = function () {
        this.submitted = true;
        if (this.editMode) {
            this.editAttribute();
        }
        else {
            if (this.attribute.length == 0) {
                this.Id = 1;
            }
            else {
                this.Id = +this.attribute[this.attribute.length - 1].Id + 1;
            }
            this.Name = this.attributeForm.value.Name;
            this.attribute.push({
                "Id": this.Id,
                "CustomProperties": {
                    "sample string 1": {},
                    "sample string 3": {}
                },
                "Name": this.Name,
                "Description": "sample string 3",
                "Locales": [
                    {
                        "LanguageId": 1,
                        "Name": "sample string 2",
                        "Description": "sample string 3"
                    },
                    {
                        "LanguageId": 1,
                        "Name": "sample string 2",
                        "Description": "sample string 3"
                    }
                ]
            });
            this.attributeService.storeAttributes(this.attribute)
                .subscribe(function (data) {
                console.log(data);
                alert("Added !");
            }, function (error) {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
            });
            this.attributeForm.reset();
        }
    };
    ProductAttributesComponent.prototype.editAttribute = function () {
        var _this = this;
        this.editMode = false;
        this.Name = this.attributeForm.value.Name;
        this.attribute[+this.Id].Name = this.Name;
        //  localStorage.setItem("attributes", JSON.stringify(this.attribute));
        this.attributeService.updateAttributes(this.attribute, this.Id)
            .subscribe(function (data) {
            console.log(data);
            _this.getAttributes();
            alert("Edited !");
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductAttributesComponent.prototype.editAttributeMode = function (id) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        // console.log(this.attribute[1].Name);
        this.Name = this.attribute[+this.Id].Name;
    };
    ProductAttributesComponent.prototype.getAttributes = function () {
        var _this = this;
        this.attributeService.getAttributes()
            .subscribe(function (response) {
            _this.attributes = (response.json());
            _this.attribute = _this.attributes.Data;
            console.log((_this.attribute));
            //  this.attribute =[this.attributes];
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    ProductAttributesComponent.prototype.deleteAttribute = function (id) {
        var _this = this;
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.Id = +this.attribute[+id.name].Id;
            this.attribute.splice(+id.name, 1);
            //localStorage.setItem("attributes",JSON.stringify(this.attribute));
            this.attributeService.deleteAttributes(this.attribute, this.Id)
                .subscribe(function (data) {
                console.log(data);
                _this.getAttributes();
                alert("Deleted !");
            }, function (error) {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
            });
        }
        if (this.editMode) {
            this.editMode = false;
        }
    };
    return ProductAttributesComponent;
}());
__decorate([
    core_1.ViewChild('f'),
    __metadata("design:type", forms_1.NgForm)
], ProductAttributesComponent.prototype, "attributeForm", void 0);
ProductAttributesComponent = __decorate([
    core_1.Component({
        selector: 'app-product-attributes',
        templateUrl: './product-attributes.component.html',
        styleUrls: ['./product-attributes.component.scss']
    }),
    __metadata("design:paramtypes", [product_attributes_service_1.ProductAttributesService])
], ProductAttributesComponent);
exports.ProductAttributesComponent = ProductAttributesComponent;
//# sourceMappingURL=product-attributes.component.js.map