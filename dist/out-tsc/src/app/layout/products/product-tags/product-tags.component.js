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
var ProductTagsComponent = (function () {
    function ProductTagsComponent() {
        this.submitted = false;
        this.tag = [];
        this.tag_name = '';
        this.editMode = false;
        this.filteredTag = '';
    }
    ProductTagsComponent.prototype.ngOnInit = function () {
        if (localStorage.getItem("tags") != null) {
            this.tag = JSON.parse(localStorage.getItem("tags"));
        }
    };
    ProductTagsComponent.prototype.addTag = function () {
        this.submitted = true;
        if (this.editMode) {
            this.editTag();
        }
        else {
            if (this.tag.length == 0) {
                this.tag_id = 1;
            }
            else {
                this.tag_id = +this.tag[this.tag.length - 1].tag_id + 1;
            }
            this.tag_name = this.tagForm.value.tag_name;
            this.tag_description = this.tagForm.value.tag_description;
            this.tag.push({
                'tag_id': this.tag_id,
                'tag_name': this.tag_name,
                'tag_description': this.tag_description
            });
            localStorage.setItem("tags", JSON.stringify(this.tag));
            alert("Added !");
            //  this.tags.tag=tag;
            console.log(this.tag);
            this.tagForm.reset();
        }
    };
    ProductTagsComponent.prototype.editTag = function () {
        this.editMode = false;
        this.tag_name = this.tagForm.value.tag_name;
        this.tag[+this.tag_id].tag_name = this.tag_name;
        this.tag[+this.tag_id].tag_description = this.tag_description;
        localStorage.setItem("tags", JSON.stringify(this.tag));
        alert("Edited !");
    };
    ProductTagsComponent.prototype.editTagMode = function (id) {
        this.editMode = true;
        this.tag_id = +id.name;
        console.log(this.tag_id);
        // console.log(this.tag[1].tag_name);
        this.tag_name = this.tag[+this.tag_id].tag_name;
        this.tag_description = this.tag[+this.tag_id].tag_description;
    };
    ProductTagsComponent.prototype.deleteTag = function (id) {
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.tag_id = +id.name;
            this.tag = JSON.parse(localStorage.getItem("tags"));
            this.tag.splice(+this.tag_id, 1);
            localStorage.setItem("tags", JSON.stringify(this.tag));
            this.tagForm.reset();
            if (this.editMode) {
                this.editMode = false;
            }
            alert("Deleted !");
        }
    };
    return ProductTagsComponent;
}());
__decorate([
    core_1.ViewChild('f'),
    __metadata("design:type", forms_1.NgForm)
], ProductTagsComponent.prototype, "tagForm", void 0);
ProductTagsComponent = __decorate([
    core_1.Component({
        selector: 'app-product-tags',
        templateUrl: './product-tags.component.html',
        styleUrls: ['./product-tags.component.scss']
    }),
    __metadata("design:paramtypes", [])
], ProductTagsComponent);
exports.ProductTagsComponent = ProductTagsComponent;
//# sourceMappingURL=product-tags.component.js.map