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
var http_client_1 = require('../common/http-client');
require('rxjs/add/operator/toPromise');
var ClientDataService = (function () {
    //构造函数
    function ClientDataService(httpClient) {
        this.httpClient = httpClient;
        this.url = server_url + "/ClientsData/GetClientsDataJson";
    }
    //获取客户端数据
    ClientDataService.prototype.getClientData = function () {
        //定义数据结构
        var dataJson = {
            dataItems: [],
            organize: [],
            role: [],
            duty: [],
            user: [],
            authorizeMenu: [],
            authorizeButton: []
        };
        return this.httpClient.get(this.url)
            .toPromise().catch(this.handleError);
        //.then(response => {
        //    debugger;
        //    response.json().data
        //})
    };
    ClientDataService.prototype.handleError = function (error) {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    };
    ClientDataService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_client_1.HttpClient])
    ], ClientDataService);
    return ClientDataService;
}());
exports.ClientDataService = ClientDataService;
//# sourceMappingURL=client-data.service.js.map