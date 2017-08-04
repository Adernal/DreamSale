"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var forms_1 = require("@angular/forms");
// relative import
var bs_component_routing_module_1 = require("./bs-component-routing.module");
var bs_component_component_1 = require("./bs-component.component");
var components_1 = require("./components");
var shared_1 = require("../../shared");
var BsComponentModule = (function () {
    function BsComponentModule() {
    }
    return BsComponentModule;
}());
BsComponentModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            forms_1.FormsModule,
            forms_1.ReactiveFormsModule,
            ng_bootstrap_1.NgbModule.forRoot(),
            shared_1.PageHeaderModule,
            bs_component_routing_module_1.BsComponentRoutingModule,
        ],
        declarations: [
            bs_component_component_1.BsComponentComponent,
            components_1.ButtonsComponent,
            components_1.AlertComponent,
            components_1.ModalComponent,
            components_1.CollapseComponent,
            components_1.DatePickerComponent,
            components_1.DropdownComponent,
            components_1.PaginationComponent,
            components_1.PopOverComponent,
            components_1.ProgressbarComponent,
            components_1.TabsComponent,
            components_1.TooltipComponent,
            components_1.TimepickerComponent
        ]
    })
], BsComponentModule);
exports.BsComponentModule = BsComponentModule;
//# sourceMappingURL=bs-component.module.js.map