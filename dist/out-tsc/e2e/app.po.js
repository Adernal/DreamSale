"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var protractor_1 = require("protractor");
var SbAdminCliUpdatePage = (function () {
    function SbAdminCliUpdatePage() {
    }
    SbAdminCliUpdatePage.prototype.navigateTo = function () {
        return protractor_1.browser.get('/');
    };
    SbAdminCliUpdatePage.prototype.getParagraphText = function () {
        return protractor_1.element(protractor_1.by.css('app-root h1')).getText();
    };
    return SbAdminCliUpdatePage;
}());
exports.SbAdminCliUpdatePage = SbAdminCliUpdatePage;
//# sourceMappingURL=app.po.js.map