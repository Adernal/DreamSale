"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var testing_2 = require("@angular/router/testing");
var forms_1 = require("@angular/forms");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var bs_component_component_1 = require("./bs-component.component");
var components_1 = require("./components");
var shared_1 = require("../../shared");
describe('BsComponentComponent', function () {
    var component;
    var fixture;
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [
                testing_2.RouterTestingModule,
                forms_1.FormsModule,
                forms_1.ReactiveFormsModule,
                ng_bootstrap_1.NgbModule.forRoot(),
                shared_1.PageHeaderModule
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
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(bs_component_component_1.BsComponentComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=bs-component.component.spec.js.map