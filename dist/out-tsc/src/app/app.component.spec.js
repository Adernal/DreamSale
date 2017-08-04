"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var testing_2 = require("@angular/router/testing");
var core_1 = require("@ngx-translate/core");
var app_component_1 = require("./app.component");
describe('AppComponent', function () {
    var fixture;
    var component;
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [
                testing_2.RouterTestingModule,
                core_1.TranslateModule.forRoot()
            ],
            declarations: [
                app_component_1.AppComponent
            ],
        }).compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(app_component_1.AppComponent);
        component = fixture.componentInstance;
    });
    it('should create the app', testing_1.async(function () {
        expect(component).toBeTruthy();
    }));
});
//# sourceMappingURL=app.component.spec.js.map