"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var testing_2 = require("@angular/router/testing");
var ng2_charts_1 = require("ng2-charts");
var shared_1 = require("./../../shared");
var charts_component_1 = require("./charts.component");
describe('ChartsComponent', function () {
    var component;
    var fixture;
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [
                testing_2.RouterTestingModule,
                ng2_charts_1.ChartsModule,
                shared_1.PageHeaderModule
            ],
            declarations: [charts_component_1.ChartsComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(charts_component_1.ChartsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=charts.component.spec.js.map