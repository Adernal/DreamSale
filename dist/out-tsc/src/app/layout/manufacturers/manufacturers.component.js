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
var manufacturers_service_1 = require("./manufacturers.service");
var ManufacturersComponent = (function () {
    function ManufacturersComponent(manufacturersService) {
        this.manufacturersService = manufacturersService;
        this.submitted = false;
        this.manufacturer = [];
        this.Name = '';
        this.manufacturer_description = '';
        this.editMode = false;
        this.filteredManufacturer = '';
    }
    ManufacturersComponent.prototype.ngOnInit = function () {
        //  localStorage.removeItem("manufacturer");
        //localStorage.removeItem("manufacturers");
        if (localStorage.getItem("manufacturer") != null) {
            this.manufacturer = JSON.parse(localStorage.getItem("manufacturer"));
        }
        //this.getManufacturers();
    };
    ManufacturersComponent.prototype.addManufacturer = function () {
        this.submitted = true;
        if (this.editMode) {
            this.editManufacturer();
        }
        else {
            if (this.manufacturer.length == 0) {
                this.Id = 1;
            }
            else {
                this.Id = +this.manufacturer[this.manufacturer.length - 1].Id + 1;
            }
            this.Name = this.manufacturerForm.value.Name;
            this.DisplayOrder = this.manufacturerForm.value.DisplayOrder;
            this.manufacturer.push({
                "Id": this.Id,
                "CustomProperties": {
                    "sample string 1": {},
                    "sample string 3": {}
                },
                "Name": this.Name,
                "Description": "Test Description",
                "ManufacturerTemplateId": 4,
                "AvailableManufacturerTemplates": [
                    {
                        "Disabled": true,
                        "Group": {
                            "Disabled": true,
                            "Name": "sample string 2"
                        },
                        "Selected": true,
                        "Text": "sample string 3",
                        "Value": "sample string 4"
                    },
                    {
                        "Disabled": true,
                        "Group": {
                            "Disabled": true,
                            "Name": "sample string 2"
                        },
                        "Selected": true,
                        "Text": "sample string 3",
                        "Value": "sample string 4"
                    }
                ],
                "MetaKeywords": "sample string 5",
                "MetaDescription": "sample string 6",
                "MetaTitle": "sample string 7",
                "SeName": "sample string 8",
                "PictureId": 9,
                "PageSize": 10,
                "AllowCustomersToSelectPageSize": true,
                "PageSizeOptions": "sample string 12",
                "PriceRanges": "sample string 13",
                "Published": true,
                "Deleted": true,
                "DisplayOrder": this.DisplayOrder,
                "Locales": [
                    {
                        "Id": 1,
                        "LanguageId": 2,
                        "Name": "sample string 3",
                        "Description": "sample string 4",
                        "MetaKeywords": "sample string 5",
                        "MetaDescription": "sample string 6",
                        "MetaTitle": "sample string 7",
                        "SeName": "sample string 8"
                    },
                    {
                        "Id": 1,
                        "LanguageId": 2,
                        "Name": "sample string 3",
                        "Description": "sample string 4",
                        "MetaKeywords": "sample string 5",
                        "MetaDescription": "sample string 6",
                        "MetaTitle": "sample string 7",
                        "SeName": "sample string 8"
                    }
                ],
                "SelectedCustomerRoleIds": [
                    1,
                    2
                ],
                "AvailableCustomerRoles": [
                    {
                        "Disabled": true,
                        "Group": {
                            "Disabled": true,
                            "Name": "sample string 2"
                        },
                        "Selected": true,
                        "Text": "sample string 3",
                        "Value": "sample string 4"
                    },
                    {
                        "Disabled": true,
                        "Group": {
                            "Disabled": true,
                            "Name": "sample string 2"
                        },
                        "Selected": true,
                        "Text": "sample string 3",
                        "Value": "sample string 4"
                    }
                ],
                "SelectedStoreIds": [
                    1,
                    2
                ],
                "AvailableStores": [
                    {
                        "Disabled": true,
                        "Group": {
                            "Disabled": true,
                            "Name": "sample string 2"
                        },
                        "Selected": true,
                        "Text": "sample string 3",
                        "Value": "sample string 4"
                    },
                    {
                        "Disabled": true,
                        "Group": {
                            "Disabled": true,
                            "Name": "sample string 2"
                        },
                        "Selected": true,
                        "Text": "sample string 3",
                        "Value": "sample string 4"
                    }
                ],
                "SelectedDiscountIds": [
                    1,
                    2
                ],
                "AvailableDiscounts": [
                    {
                        "Disabled": true,
                        "Group": {
                            "Disabled": true,
                            "Name": "sample string 2"
                        },
                        "Selected": true,
                        "Text": "sample string 3",
                        "Value": "sample string 4"
                    },
                    {
                        "Disabled": true,
                        "Group": {
                            "Disabled": true,
                            "Name": "sample string 2"
                        },
                        "Selected": true,
                        "Text": "sample string 3",
                        "Value": "sample string 4"
                    }
                ]
            });
            localStorage.setItem("manufacturers", JSON.stringify(this.manufacturer));
            alert("Added");
            // this.manufacturersService.storeManufacturers(this.manufacturer)
            // .subscribe(
            //   (response)=>console.log(response),
            //   (error)=>console.log(error)
            // );
            //  this.manufacturers.manufacturer=manufacturer;
            this.manufacturerForm.reset();
        }
    };
    ManufacturersComponent.prototype.editManufacturerMode = function (id) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        this.Name = this.manufacturer[+this.Id].Name;
        this.DisplayOrder = this.manufacturer[+this.Id].DisplayOrder;
    };
    ManufacturersComponent.prototype.editManufacturer = function () {
        this.editMode = false;
        this.Name = this.manufacturerForm.value.Name;
        this.DisplayOrder = this.manufacturerForm.value.DisplayOrder;
        this.manufacturer[+this.Id].Name = this.Name;
        this.manufacturer[+this.Id].DisplayOrder = this.DisplayOrder;
        localStorage.setItem("manufacturers", JSON.stringify(this.manufacturer));
        alert("Edited");
        // this.manufacturersService.updateManufacturers(this.manufacturer,this.Id)
        // .subscribe(
        //   (response)=>{
        //     console.log(response);
        //     alert("Edited !");
        //     this.getManufacturers();
        //   },
        //   (error)=>console.log(error)
        // );
    };
    ManufacturersComponent.prototype.deleteManufacturer = function (id) {
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.Id = +this.manufacturer[+id.name].Id;
            this.manufacturer = JSON.parse(localStorage.getItem("manufacturers"));
            this.manufacturer.splice(+id.name, 1);
            localStorage.setItem("manufacturers", JSON.stringify(this.manufacturer));
            alert("Deleted !");
            // this.manufacturersService.deleteAttributes(this.manufacturer,this.Id)
            // .subscribe(
            //   (response)=>{
            //     console.log(response);
            //     alert("Deleted !");
            //     this.getManufacturers();
            //   },
            //   (error)=>console.log(error)
            // );
        }
    };
    return ManufacturersComponent;
}());
__decorate([
    core_1.ViewChild('f'),
    __metadata("design:type", forms_1.NgForm)
], ManufacturersComponent.prototype, "manufacturerForm", void 0);
ManufacturersComponent = __decorate([
    core_1.Component({
        selector: 'app-manufacturers',
        templateUrl: './manufacturers.component.html',
        styleUrls: ['./manufacturers.component.scss']
    }),
    __metadata("design:paramtypes", [manufacturers_service_1.ManufacturersService])
], ManufacturersComponent);
exports.ManufacturersComponent = ManufacturersComponent;
//# sourceMappingURL=manufacturers.component.js.map