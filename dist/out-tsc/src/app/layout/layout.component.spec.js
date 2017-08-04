"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var testing_2 = require("@angular/router/testing");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var core_1 = require("@ngx-translate/core");
var shared_1 = require("../shared");
var layout_component_1 = require("./layout.component");
describe('LayoutComponent', function () {
    var component;
    var fixture;
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [
                testing_2.RouterTestingModule,
                ng_bootstrap_1.NgbDropdownModule.forRoot(),
                core_1.TranslateModule.forRoot(),
            ],
            declarations: [
                layout_component_1.LayoutComponent,
                shared_1.HeaderComponent,
                shared_1.SidebarComponent,
            ]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(layout_component_1.LayoutComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=layout.component.spec.js.map