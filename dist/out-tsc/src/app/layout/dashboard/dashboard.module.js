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
var dashboard_routing_module_1 = require("./dashboard-routing.module");
var dashboard_component_1 = require("./dashboard.component");
var components_1 = require("./components");
var shared_1 = require("../../shared");
var DashboardModule = (function () {
    function DashboardModule() {
    }
    return DashboardModule;
}());
DashboardModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            ng_bootstrap_1.NgbCarouselModule.forRoot(),
            ng_bootstrap_1.NgbAlertModule.forRoot(),
            dashboard_routing_module_1.DashboardRoutingModule,
            shared_1.StatModule,
        ],
        declarations: [
            dashboard_component_1.DashboardComponent,
            components_1.TimelineComponent,
            components_1.NotificationComponent,
            components_1.ChatComponent
        ]
    })
], DashboardModule);
exports.DashboardModule = DashboardModule;
//# sourceMappingURL=dashboard.module.js.map