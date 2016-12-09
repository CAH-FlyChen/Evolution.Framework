"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
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
var platform_browser_1 = require('@angular/platform-browser');
var http_1 = require('@angular/http');
var forms_1 = require('@angular/forms');
var common_1 = require('@angular/common');
var http_2 = require('@angular/http');
var ng_bootstrap_1 = require('@ng-bootstrap/ng-bootstrap');
var app_component_1 = require('./app.component');
var home_component_1 = require('./components/home.component');
var organize_component_1 = require('./components/organize.component');
var tab_bar_component_1 = require('./core/common/tab-bar.component');
var about_component_1 = require('./components/about.component');
var routes_1 = require('./routes');
var highlight_directive_1 = require('./highlight.directive');
var keys_pipe_1 = require('./core/common/keys-pipe');
//import { DataService } from './core/services/data.service';
//import { MembershipService } from './core/services/membership.service';
//import { UtilityService } from './core/services/utility.service';
//import { NotificationService } from './core/services/notification.service';
var AppBaseRequestOptions = (function (_super) {
    __extends(AppBaseRequestOptions, _super);
    function AppBaseRequestOptions() {
        _super.call(this);
        this.headers = new http_2.Headers();
        this.headers.append('Content-Type', 'application/json');
        this.body = '';
    }
    return AppBaseRequestOptions;
}(http_2.BaseRequestOptions));
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                ng_bootstrap_1.NgbModule.forRoot(),
                platform_browser_1.BrowserModule,
                forms_1.FormsModule,
                http_1.HttpModule,
                routes_1.routing
            ],
            declarations: [app_component_1.AppComponent, keys_pipe_1.KeysPipe, home_component_1.HomeComponent, about_component_1.AboutComponent, highlight_directive_1.HighlightDirective, organize_component_1.OrganizeComponent, tab_bar_component_1.TabBarComponent],
            providers: [
                { provide: common_1.LocationStrategy, useClass: common_1.HashLocationStrategy },
                { provide: http_2.RequestOptions, useClass: AppBaseRequestOptions }],
            bootstrap: [app_component_1.AppComponent]
        }), 
        __metadata('design:paramtypes', [])
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map