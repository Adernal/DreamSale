"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var components_1 = require("./components");
var shared_1 = require("../../shared");
var dashboard_component_1 = require("./dashboard.component");
describe('DashboardComponent', function () {
    var component;
    var fixture;
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [
                ng_bootstrap_1.NgbCarouselModule.forRoot(),
                ng_bootstrap_1.NgbAlertModule.forRoot(),
                shared_1.StatModule,
            ],
            declarations: [
                dashboard_component_1.DashboardComponent,
                components_1.TimelineComponent,
                components_1.NotificationComponent,
                components_1.ChatComponent
            ]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(dashboard_component_1.DashboardComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=dashboard.component.spec.js.map