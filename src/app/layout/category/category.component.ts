import {Component, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {NgModule} from '@angular/core';
import {NgForm} from '@angular/forms';
import {CategoryService} from './category.service';


@Component({
  selector: 'app-category',

  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})

export class CategoryComponent implements OnInit {
  currentPageNumber: number = 1;
  @ViewChild('f') categoryForm: NgForm;
  submitted = false;
  category = [];
  Id: Number;
  Name = '';
  Description = '';
  DisplayOrder: Number;
  ParentCategoryId:Number;
  ParentCategory='';
  currentCategory=[];
  editMode = false;
  filteredCategory = '';
  categories;



  constructor(private categoryService: CategoryService) {}

  ngOnInit() {
    // localStorage.removeItem("categories");

    this.getCategory();



  }
  addCategory() {
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
      this.ParentCategoryId = this.categoryForm.value.parentcategory;
      this.getCategoryName();
      this.category.push({
        'Id':0,
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
        'ParentCategoryId': this.ParentCategoryId,
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
        .subscribe(
        (data) => {
          console.log(data);
          alert('Added !');
        },
        (error) => {
          alert('Failed to add !');
          console.log(error)
        }
        );
      //  this.categorys.category=category;

      this.categoryForm.reset();
    }

  }
  editCategoryMode(id: HTMLFormElement) {
    this.editMode = true;
    this.Id = +id.name;
    this.currentCategory = this.getCategoryIndex(this.Id)[0];
    // console.log(this.category[1].Name);
    this.Name = this.currentCategory["Name"];
    this.Description = this.currentCategory["Description"];
    this.DisplayOrder = this.currentCategory["DisplayOrder"];
    this.ParentCategoryId = this.currentCategory["ParentCategoryId"];


  }
  editCategory() {
    this.editMode = false;
    this.Name = this.categoryForm.value.Name;
    this.Description = this.categoryForm.value.Description;
    this.DisplayOrder = this.categoryForm.value.DisplayOrder;
    this.ParentCategoryId = this.categoryForm.value.parentcategory;
    this.getCategoryName();


    this.currentCategory["Name"] = this.Name;
    this.currentCategory["Description"] = this.Description;
    this.currentCategory["DisplayOrder"] = this.DisplayOrder;
    this.currentCategory["ParentCategoryId"] = this.ParentCategoryId;
    // localStorage.setItem("categories",JSON.stringify(this.category));
    this.categoryService.updateCategory(this.currentCategory)
      .subscribe(
      (data) => {

        console.log("Category Updated");

        this.getCategory();
        alert('Edited !');
      },
      (error) => {
        console.log(error);
        alert('Can\'t fetch data ! Please refresh or check your connnection !');
      }
      );

  }
  getCategory() {
    this.categoryService.getCategory()
      .subscribe(
      (response) => {
        this.categories = (response.json());
        this.category = this.categories.Data;

        console.log(("Fetched Category"));

        //  this.attribute =[this.attributes];
      },
      (error) => {
        console.log(error)
        alert('Can\'t fetch data ! Please refresh or check your connnection !');
      }
      );

  }
  deleteCategory(id: HTMLFormElement) {
    const confirmation = confirm('Are you sure you want to delete ?');
    if (confirmation) {
      this.categoryService.deleteCategory(+id.name)
        .subscribe(
        (data) => {
          this.getCategory();
          alert('Deleted !');
        },
        (error) => {
          console.log(error)
          alert('Can\'t fetch data ! Please refresh or check your connnection !')
        }
        );
    }
    if (this.editMode) {
      this.editMode = false;

    }
  }
   getCategoryIndex(id:Number) {
    return this.category.filter(
        function(category){ return category.Id == id }
    );
  }
  getCategoryName(){
    if(this.ParentCategoryId!=null){
      var index=this.getCategoryIndex(this.ParentCategoryId);
      this.ParentCategory = index[0].Name;
      this.Name=this.ParentCategory+'--->'+this.Name;
    }

  }

}