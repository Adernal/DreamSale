"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var testing_2 = require("@angular/router/testing");
var auth_guard_1 = require("./auth.guard");
describe('AuthGuard', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [testing_2.RouterTestingModule],
            providers: [auth_guard_1.AuthGuard]
        });
    });
    it('should ...', testing_1.inject([auth_guard_1.AuthGuard], function (guard) {
        expect(guard).toBeTruthy();
    }));
});
//# sourceMappingURL=auth.guard.spec.js.map