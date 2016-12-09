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
var router_1 = require('@angular/router');
var TabBarComponent = (function () {
    function TabBarComponent(router, route) {
        this.router = router;
        this.route = route;
        this.tabItems = [];
        var __this = this;
        this.tabItems.push({ url: "/Home/Default", title: "欢迎首页", isSelected: true });
        this.tabItems.push({ url: "/Home/About", title: "平台介绍", isSelected: false });
        //this.router.events.subscribe(e => {
        //    if (e instanceof NavigationEnd) {
        //        __this.currentSelectedUrl = e.url;
        //    }
        //});
    }
    Object.defineProperty(TabBarComponent.prototype, "changeSelectedUrlObj", {
        set: function (val) {
            if (!val)
                return;
            if (!this.existUrl(val.UrlAddress)) {
                this.tabItems.push({ url: val.UrlAddress, title: val.FullName, isSelected: true });
            }
            this.tabSelectedChangeByUrl(val.UrlAddress);
        },
        enumerable: true,
        configurable: true
    });
    TabBarComponent.prototype.tabSelectedChange = function (obj) {
        this.tabSelectedChangeByUrl(obj.url);
        this.router.navigate([obj.url]);
    };
    TabBarComponent.prototype.tabSelectedChangeByUrl = function (url) {
        for (var i = 0; i < this.tabItems.length; i++) {
            this.tabItems[i].isSelected = this.tabItems[i].url == url;
        }
    };
    TabBarComponent.prototype.existUrl = function (url) {
        for (var i = 0; i < this.tabItems.length; i++) {
            if (this.tabItems[i].url == url) {
                return true;
            }
        }
        return false;
    };
    __decorate([
        core_1.Input(), 
        __metadata('design:type', Object)
    ], TabBarComponent.prototype, "selectedUrlObj", void 0);
    __decorate([
        core_1.Input('selectedUrlObj'), 
        __metadata('design:type', Object), 
        __metadata('design:paramtypes', [Object])
    ], TabBarComponent.prototype, "changeSelectedUrlObj", null);
    TabBarComponent = __decorate([
        core_1.Component({
            selector: 'tab-bar',
            templateUrl: './app/core/common/tab-bar.component.html'
        }), 
        __metadata('design:paramtypes', [router_1.Router, router_1.ActivatedRoute])
    ], TabBarComponent);
    return TabBarComponent;
}());
exports.TabBarComponent = TabBarComponent;
var TabBarItem = (function () {
    function TabBarItem() {
    }
    return TabBarItem;
}());
exports.TabBarItem = TabBarItem;
//# sourceMappingURL=tab-bar.component.js.map