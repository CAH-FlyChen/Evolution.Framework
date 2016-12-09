/// <reference path="../../typings/globals/es6-shim/index.d.ts" />
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
var core_2 = require('@angular/core');
var http_client_1 = require('./core/common/http-client');
var client_data_service_1 = require('./core/services/client-data.service');
var client_data_1 = require('./core/domain/client-data');
var router_1 = require('@angular/router');
core_2.enableProdMode();
var AppComponent = (function () {
    function AppComponent(service, router) {
        var _this = this;
        this.service = service;
        var __this = this;
        this.clientData = new client_data_1.ClientData();
        router.events.subscribe(function (event) {
            if (!event.url)
                return;
            if (!_this.clientData.authorizeMenu)
                return;
            if (event instanceof router_1.NavigationEnd) {
                _this.clientData.authorizeMenu.forEach(function (data0, index0, array0) {
                    data0.ChildNodes.forEach(function (data, index, array) {
                        if (data.UrlAddress == event.url) {
                            __this.currentUrlObj = data;
                        }
                    });
                });
            }
        });
    }
    AppComponent.prototype.ngOnInit = function () {
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
    AppComponent.prototype.getUserName = function () {
        return "admin";
    };
    AppComponent.prototype.getClientData = function () {
    };
    AppComponent.prototype.ngAfterContentInit = function () {
        this.setDefaultSking();
        this.changeSking();
        this.adjustWindow();
        this.configSideBar();
        this.doOthers();
    };
    AppComponent.prototype.setDefaultSking = function () {
        if (storage) {
            var usedSkin = localStorage.getItem('config-skin');
            if (usedSkin != '' && usedSkin != null) {
                document.body.className = usedSkin;
            }
            else {
                document.body.className = 'theme-blue-gradient';
                localStorage.setItem('config-skin', "theme-blue-gradient");
            }
        }
    };
    AppComponent.prototype.changeSking = function () {
        if (storage) {
            try {
                var usedSkin = localStorage.getItem('config-skin');
                if (usedSkin != '') {
                    jQuery('#skin-colors .skin-changer').removeClass('active');
                    jQuery('#skin-colors .skin-changer[data-skin="' + usedSkin + '"]').addClass('active');
                }
            }
            catch (e) {
                console.log(e);
            }
        }
        else {
            document.body.className = 'theme-blue';
        }
        //jQuery('#config-tool-cog').on('click', function () {
        //    jQuery('#config-tool').toggleClass('closed');
        //});
        //jQuery('#config-fixed-header').on('change', function () {
        //    var fixedHeader = '';
        //    if (jQuery(this).is(':checked')) {
        //        jQuery('body').addClass('fixed-header'); fixedHeader = 'fixed-header';
        //    }
        //    else {
        //        jQuery('body').removeClass('fixed-header');
        //        if (jQuery('#config-fixed-sidebar').is(':checked')) {
        //            jQuery('#config-fixed-sidebar').prop('checked', false);
        //            jQuery('#config-fixed-sidebar').trigger('change'); location.reload();
        //        }
        //    }
        //});
        //jQuery('#skin-colors .skin-changer').on('click', function () {
        //    jQuery('body').removeClassPrefix('theme-');
        //    jQuery('body').addClass(jQuery(this).data('skin'));
        //    jQuery('#skin-colors .skin-changer').removeClass('active');
        //    jQuery(this).addClass('active');
        //    if (storage) {
        //        try {
        //            localStorage.setItem('config-skin', jQuery(this).data('skin'));
        //        }
        //        catch (e) { console.log(e); }
        //    }
        //});
    };
    AppComponent.prototype.adjustWindow = function () {
        $("#content-wrapper").find('.mainContent').height($(window).height() - 100);
        $(window).resize(function (e) {
            $("#content-wrapper").find('.mainContent').height($(window).height() - 100);
        });
    };
    AppComponent.prototype.configSideBar = function () {
        $('#sidebar-nav,#nav-col-submenu').on('click', '.dropdown-toggle', function (e) {
            e.preventDefault();
            var $item = $(this).parent();
            if (!$item.hasClass('open')) {
                $item.parent().find('.open .submenu').slideUp('fast');
                $item.parent().find('.open').toggleClass('open');
            }
            $item.toggleClass('open');
            if ($item.hasClass('open')) {
                $item.children('.submenu').slideDown('fast', function () {
                    var _height1 = $(window).height() - 92 - $item.position().top;
                    var _height2 = $item.find('ul.submenu').height() + 10;
                    var _height3 = _height2 > _height1 ? _height1 : _height2;
                    $item.find('ul.submenu').css({
                        overflow: "auto",
                        height: _height3
                    });
                });
            }
            else {
                $item.children('.submenu').slideUp('fast');
            }
        });
    };
    AppComponent.prototype.doOthers = function () {
        $('body').on('mouseenter', '#page-wrapper.nav-small #sidebar-nav .dropdown-toggle', function (e) {
            if ($(document).width() >= 992) {
                var $item = $(this).parent();
                if ($('body').hasClass('fixed-leftmenu')) {
                    var topPosition = $item.position().top;
                    if ((topPosition + 4 * $(this).outerHeight()) >= $(window).height()) {
                        topPosition -= 6 * $(this).outerHeight();
                    }
                    $('#nav-col-submenu').html($item.children('.submenu').clone());
                    $('#nav-col-submenu > .submenu').css({ 'top': topPosition });
                }
                $item.addClass('open');
                $item.children('.submenu').slideDown('fast');
            }
        });
        $('body').on('mouseleave', '#page-wrapper.nav-small #sidebar-nav > .nav-pills > li', function (e) {
            if ($(document).width() >= 992) {
                var $item = $(this);
                if ($item.hasClass('open')) {
                    $item.find('.open .submenu').slideUp('fast');
                    $item.find('.open').removeClass('open');
                    $item.children('.submenu').slideUp('fast');
                }
                $item.removeClass('open');
            }
        });
        $('body').on('mouseenter', '#page-wrapper.nav-small #sidebar-nav a:not(.dropdown-toggle)', function (e) {
            if ($('body').hasClass('fixed-leftmenu')) {
                $('#nav-col-submenu').html('');
            }
        });
        $('body').on('mouseleave', '#page-wrapper.nav-small #nav-col', function (e) {
            if ($('body').hasClass('fixed-leftmenu')) {
                $('#nav-col-submenu').html('');
            }
        });
        $('.mobile-search').click(function (e) {
            e.preventDefault();
            $('.mobile-search').addClass('active');
            $('.mobile-search form input.form-control').focus();
        });
        $(document).mouseup(function (e) {
            var container = $('.mobile-search');
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                container.removeClass('active');
            }
        });
        $(window).load(function () {
            window.setTimeout(function () {
                $('#ajax-loader').fadeOut();
            }, 300);
        });
        document.body.className = localStorage.getItem('config-skin');
        jQuery("[data-toggle='tooltip']").tooltip();
    };
    //模板左上角点击可以缩小左侧导航
    AppComponent.prototype.onSmallNavClick = function () {
        $('#page-wrapper').toggleClass('nav-small');
    };
    AppComponent.prototype.onNavLinkClicked = function (name) {
        //添加一个tab
        //设置tab为活动
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            templateUrl: './app/app.component.html',
            providers: [http_client_1.HttpClient, client_data_service_1.ClientDataService, client_data_1.ClientData]
        }), 
        __metadata('design:paramtypes', [client_data_service_1.ClientDataService, router_1.Router])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map