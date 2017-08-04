"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var dropdown_component_1 = require("./dropdown.component");
describe('DropdownComponent', function () {
    var component;
    var fixture;
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [
                ng_bootstrap_1.NgbModule.forRoot()
            ],
            declarations: [dropdown_component_1.DropdownComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(dropdown_component_1.DropdownComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=dropdown.component.spec.js.map