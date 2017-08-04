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
var category_service_1 = require("./category.service");
var CategoryComponent = (function () {
    function CategoryComponent(categoryService) {
        this.categoryService = categoryService;
        this.submitted = false;
        this.category = [];
        this.Name = '';
        this.Description = '';
        this.editMode = false;
        this.filteredCategory = '';
    }
    CategoryComponent.prototype.ngOnInit = function () {
        // localStorage.removeItem("categories");
        this.getCategory();
    };
    CategoryComponent.prototype.addCategory = function () {
        this.submitted = true;
        if (this.editMode) {
            this.editCategory();
        }
        else {
            if (this.category.length == 0) {
                this.Id = 1;
            }
            else {
                this.Id = +this.category[this.category.length - 1].Id + 1;
            }
            this.Name = this.categoryForm.value.Name;
            this.Description = this.categoryForm.value.Description;
            this.DisplayOrder = this.categoryForm.value.DisplayOrder;
            this.category.push({
                'Id': this.Id,
                'CustomProperties': {
                    'sample string 1': {},
                    'sample string 3': {}
                },
                'Name': this.Name,
                'Description': this.Description,
                'CategoryTemplateId': 4,
                'AvailableCategoryTemplates': [
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    },
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    }
                ],
                'MetaKeywords': 'sample string 5',
                'MetaDescription': 'sample string 6',
                'MetaTitle': 'sample string 7',
                'SeName': 'sample string 8',
                'ParentCategoryId': 9,
                'PictureId': 10,
                'PageSize': 11,
                'AllowCustomersToSelectPageSize': true,
                'PageSizeOptions': 'sample string 13',
                'PriceRanges': 'sample string 14',
                'ShowOnHomePage': true,
                'IncludeInTopMenu': true,
                'Published': true,
                'Deleted': true,
                'DisplayOrder': this.DisplayOrder,
                'Locales': [
                    {
                        'LanguageId': 1,
                        'Name': 'sample string 2',
                        'Description': 'sample string 3',
                        'MetaKeywords': 'sample string 4',
                        'MetaDescription': 'sample string 5',
                        'MetaTitle': 'sample string 6',
                        'SeName': 'sample string 7'
                    },
                    {
                        'LanguageId': 1,
                        'Name': 'sample string 2',
                        'Description': 'sample string 3',
                        'MetaKeywords': 'sample string 4',
                        'MetaDescription': 'sample string 5',
                        'MetaTitle': 'sample string 6',
                        'SeName': 'sample string 7'
                    }
                ],
                'Breadcrumb': 'sample string 20',
                'SelectedCustomerRoleIds': [
                    1,
                    2
                ],
                'AvailableCustomerRoles': [
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    },
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    }
                ],
                'SelectedStoreIds': [
                    1,
                    2
                ],
                'AvailableStores': [
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    },
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    }
                ],
                'AvailableCategories': [
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    },
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    }
                ],
                'SelectedDiscountIds': [
                    1,
                    2
                ],
                'AvailableDiscounts': [
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    },
                    {
                        'Disabled': true,
                        'Group': {
                            'Disabled': true,
                            'Name': 'sample string 2'
                        },
                        'Selected': true,
                        'Text': 'sample string 3',
                        'Value': 'sample string 4'
                    }
                ]
            });
            //  localStorage.setItem("categories",JSON.stringify(this.category));
            this.categoryService.storeCategory(this.category)
                .subscribe(function (data) {
                console.log(data);
                alert('Added !');
            }, function (error) {
                alert('Failed to add !');
                console.log(error);
            });
            //  this.categorys.category=category;
            console.log(this.category);
            this.categoryForm.reset();
        }
    };
    CategoryComponent.prototype.editCategoryMode = function (id) {
        this.editMode = true;
        this.Id = +id.name;
        console.log(this.Id);
        // console.log(this.category[1].Name);
        this.Name = this.category[+this.Id].Name;
        this.Description = this.category[+this.Id].Description;
        this.DisplayOrder = this.category[+this.Id].DisplayOrder;
    };
    CategoryComponent.prototype.editCategory = function () {
        var _this = this;
        this.editMode = false;
        this.Name = this.categoryForm.value.Name;
        this.Description = this.categoryForm.value.Description;
        this.DisplayOrder = this.categoryForm.value.DisplayOrder;
        this.category[+this.Id].Name = this.Name;
        this.category[+this.Id].Description = this.Description;
        this.category[+this.Id].DisplayOrder = this.DisplayOrder;
        // localStorage.setItem("categories",JSON.stringify(this.category));
        this.categoryService.updateCategory(this.category, this.Id)
            .subscribe(function (data) {
            console.log(data.json());
            _this.getCategory();
            alert('Edited !');
        }, function (error) {
            console.log(error);
            alert('Can\'t fetch data ! Please refresh or check your connnection !');
        });
    };
    CategoryComponent.prototype.getCategory = function () {
        var _this = this;
        this.categoryService.getCategory()
            .subscribe(function (response) {
            _this.categories = (response.json());
            _this.category = _this.categories.Data;
            console.log((_this.category));
            //  this.attribute =[this.attributes];
        }, function (error) {
            console.log(error);
            alert('Can\'t fetch data ! Please refresh or check your connnection !');
        });
    };
    CategoryComponent.prototype.deleteCategory = function (id) {
        var _this = this;
        var confirmation = confirm('Are you sure you want to delete ?');
        if (confirmation) {
            this.Id = +this.category[+id.name].Id;
            this.category = JSON.parse(localStorage.getItem('categories'));
            this.category.splice(+id.name, 1);
            // localStorage.setItem("categories",JSON.stringify(this.category));
            this.categoryService.deleteCategory(this.category, this.Id)
                .subscribe(function (data) {
                console.log(data);
                _this.getCategory();
                alert('Deleted !');
            }, function (error) {
                console.log(error);
                alert('Can\'t fetch data ! Please refresh or check your connnection !');
            });
        }
        if (this.editMode) {
            this.editMode = false;
        }
    };
    return CategoryComponent;
}());
__decorate([
    core_1.ViewChild('f'),
    __metadata("design:type", forms_1.NgForm)
], CategoryComponent.prototype, "categoryForm", void 0);
CategoryComponent = __decorate([
    core_1.Component({
        selector: 'app-category',
        templateUrl: './category.component.html',
        styleUrls: ['./category.component.scss']
    }),
    __metadata("design:paramtypes", [category_service_1.CategoryService])
], CategoryComponent);
exports.CategoryComponent = CategoryComponent;
//# sourceMappingURL=category.component.js.map