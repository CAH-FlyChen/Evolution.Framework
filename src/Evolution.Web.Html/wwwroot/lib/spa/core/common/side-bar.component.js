/// <reference path="../../../../typings/globals/es6-shim/index.d.ts" />
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
require('rxjs/add/operator/map');
var http_client_1 = require('./http-client');
var client_data_service_1 = require('../services/client-data.service');
var client_data_1 = require('../domain/client-data');
/**
 * 暂时没有使用
 */
var SideBarComponent = (function () {
    function SideBarComponent(service) {
        this.service = service;
        this.clientData = new client_data_1.ClientData();
    }
    SideBarComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.service.getClientData().then(function (response) {
            var datax = response.json();
            _this.clientData.dataItems = datax.dataItems;
            _this.clientData.organize = datax.organize;
            _this.clientData.role = datax.role;
            _this.clientData.duty = datax.duty;
            _this.clientData.authorizeMenu = eval(datax.authorizeMenu);
            _this.clientData.authorizeButton = datax.authorizeButton;
        });
    };
    SideBarComponent = __decorate([
        core_1.Component({
            selector: 'side-bar',
            templateUrl: './side-bar.component.html',
            providers: [http_client_1.HttpClient, client_data_service_1.ClientDataService, client_data_1.ClientData]
        }), 
        __metadata('design:paramtypes', [client_data_service_1.ClientDataService])
    ], SideBarComponent);
    return SideBarComponent;
}());
exports.SideBarComponent = SideBarComponent;
