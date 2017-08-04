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
var specification_attributes_service_1 = require("./specification-attributes.service");
var SpecificationAttributesComponent = (function () {
    function SpecificationAttributesComponent(specificationAttributeService) {
        this.specificationAttributeService = specificationAttributeService;
        this.submitted = false;
        this.attribute = [];
        this.Name = '';
        this.editMode = false;
        this.filteredSpecificationAttribute = '';
    }
    SpecificationAttributesComponent.prototype.ngOnInit = function () {
        //localStorage.removeItem("spec-attributes");
        this.getAttributes();
    };
    SpecificationAttributesComponent.prototype.addAttribute = function () {
        var _this = this;
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
                "DisplayOrder": 3,
                "Locales": [
                    {
                        "LanguageId": 1,
                        "Name": "sample string 2"
                    },
                    {
                        "LanguageId": 1,
                        "Name": "sample string 2"
                    }
                ]
            });
            //localStorage.setItem("spec-attributes", JSON.stringify(this.attribute));
            //  this.attributes.attribute=attribute;
            this.specificationAttributeService.storeAttributes(this.attribute)
                .subscribe(function (data) {
                console.log(data);
                alert("Added !");
                _this.getAttributes();
            }, function (error) {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
            });
            this.attributeForm.reset();
        }
    };
    SpecificationAttributesComponent.prototype.editAttribute = function () {
        var _this = this;
        this.editMode = false;
        this.Name = this.attributeForm.value.Name;
        this.attribute[+this.Id].Name = this.Name;
        // localStorage.setItem("spec-attributes", JSON.stringify(this.attribute));
        this.specificationAttributeService.updateAttributes(this.attribute, this.Id)
            .subscribe(function (data) {
            console.log(data);
            _this.getAttributes();
            alert("Edited !");
        }, function (error) {
            console.log(error);
            alert("Can't fetch data ! Please refresh or check your connnection !");
        });
    };
    SpecificationAttributesComponent.prototype.editAttributeMode = function (id) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        // console.log(this.attribute[1].Name);
        this.Name = this.attribute[+this.Id].Name;
    };
    SpecificationAttributesComponent.prototype.getAttributes = function () {
        var _this = this;
        this.specificationAttributeService.getAttributes()
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
    SpecificationAttributesComponent.prototype.deleteAttribute = function (id) {
        var _this = this;
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.Id = +this.attribute[+id.name].Id;
            this.attribute.splice(+id.name, 1);
            //localStorage.setItem("attributes",JSON.stringify(this.attribute));
            this.specificationAttributeService.deleteAttributes(this.attribute, this.Id)
                .subscribe(function (data) {
                console.log(data);
                _this.getAttributes();
                alert("Deleted");
            }, function (error) {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
            });
        }
        if (this.editMode) {
            this.editMode = false;
        }
    };
    return SpecificationAttributesComponent;
}());
__decorate([
    core_1.ViewChild('f'),
    __metadata("design:type", forms_1.NgForm)
], SpecificationAttributesComponent.prototype, "attributeForm", void 0);
SpecificationAttributesComponent = __decorate([
    core_1.Component({
        selector: 'app-specification-attributes',
        templateUrl: './specification-attributes.component.html',
        styleUrls: ['./specification-attributes.component.scss']
    }),
    __metadata("design:paramtypes", [specification_attributes_service_1.SpecificationAttributesService])
], SpecificationAttributesComponent);
exports.SpecificationAttributesComponent = SpecificationAttributesComponent;
//# sourceMappingURL=specification-attributes.component.js.map