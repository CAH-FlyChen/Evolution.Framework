/// <reference path="../../../typings/globals/jquery/index.d.ts" />
"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var ng_bootstrap_1 = require('@ng-bootstrap/ng-bootstrap');
var OrganizeAddComponent = (function () {
    function OrganizeAddComponent(activeModal) {
        this.activeModal = activeModal;
    }
    OrganizeAddComponent.prototype.ngAfterContentInit = function () {
    };
    __decorate([
        core_1.Input(), 
        __metadata('design:type', Object)
    ], OrganizeAddComponent.prototype, "name", void 0);
    OrganizeAddComponent = __decorate([
        core_1.Component({
            selector: 'organize-add',
            template: "\n    <div class=\"modal-header\">\n      <button type=\"button\" class=\"close\" aria-label=\"Close\" (click)=\"activeModal.dismiss('Cross click')\">\n        <span aria-hidden=\"true\">&times;</span>\n      </button>\n      <h4 class=\"modal-title\">Hi there!</h4>\n    </div>\n    <div class=\"modal-body\">\n      <p>Hello, {{name}}!</p>\n    </div>\n    <div class=\"modal-footer\">\n      <button type=\"button\" class=\"btn btn-secondary\" (click)=\"activeModal.close('Close click')\">Close</button>\n    </div>\n  "
        }), 
        __metadata('design:paramtypes', [ng_bootstrap_1.NgbActiveModal])
    ], OrganizeAddComponent);
    return OrganizeAddComponent;
}());
exports.OrganizeAddComponent = OrganizeAddComponent;
