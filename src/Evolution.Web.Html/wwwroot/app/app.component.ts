/// <reference path="../../typings/globals/es6-shim/index.d.ts" />

import { Component, OnInit, AfterContentInit, AfterViewInit } from '@angular/core';
import { Location } from '@angular/common';
import 'rxjs/add/operator/map';
import { enableProdMode } from '@angular/core';
import { HttpClient } from './core/common/http-client';
import { ClientDataService } from './core/services/client-data.service';
import { ClientData } from './core/domain/client-data';
import { ActivatedRoute, Params, Router, NavigationEnd } from '@angular/router';

enableProdMode();
//import { MembershipService } from './core/services/membership.service';
//import { User } from './core/domain/user';

declare var jQuery;
declare var storage;
declare var $;

@Component({
    selector: 'my-app',
    templateUrl: './app/app.component.html',
    providers: [HttpClient, ClientDataService, ClientData]
})
export class AppComponent implements OnInit,AfterContentInit {
    username: string;
    color: string;
    clientData: ClientData;
    //当前访问的urlObj
    currentUrlObj: any;

    constructor(private service: ClientDataService, private router: Router) {
        var __this = this;
        this.clientData = new ClientData();
        router.events.subscribe(event => {
            if (!event.url) return;
            if (!this.clientData.authorizeMenu) return;
            if (event instanceof NavigationEnd) {
                this.clientData.authorizeMenu.forEach(function (data0, index0, array0) {
                    data0.ChildNodes.forEach(function (data, index, array) {
                        if (data.UrlAddress == event.url) {
                            __this.currentUrlObj = data;
                        }
                    });
                });
            }
        });
    }

    ngOnInit() {
        this.service.getClientData().then(response => {
            var datax = response.json();

            this.clientData.dataItems = datax.dataItems;
            this.clientData.organize = datax.organize;
            this.clientData.role = datax.role;
            this.clientData.duty = datax.duty;
            this.clientData.authorizeMenu = eval(datax.authorizeMenu);
            this.clientData.authorizeButton = datax.authorizeButton;


        });
    }


    getUserName(): string {
        return "admin"
    }

    getClientData() {

    }

    ngAfterContentInit() {

        this.setDefaultSking();
        this.changeSking();
        this.adjustWindow();
        this.configSideBar();
        this.doOthers();
    }
    setDefaultSking() {
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
    }

    changeSking() {
        if (storage) {
            try {
                var usedSkin = localStorage.getItem('config-skin');
                if (usedSkin != '') {
                    jQuery('#skin-colors .skin-changer').removeClass('active');
                    jQuery('#skin-colors .skin-changer[data-skin="' + usedSkin + '"]').addClass('active');
                }
            }
            catch (e) { console.log(e); }
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
    }

    adjustWindow() {
        $("#content-wrapper").find('.mainContent').height($(window).height() - 50);
        $(window).resize(function (e) {
            $("#content-wrapper").find('.mainContent').height($(window).height() - 50);
        });
    }

    configSideBar() {
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
                    //var _height1 = $(window).height() - 92 - $item.position().top;
                    //var _height2 = $item.find('ul.submenu').height() + 10;
                    //var _height3 = _height2 > _height1 ? _height1 : _height2;
                    //$item.find('ul.submenu').css({
                    //    overflow: "none",
                    //    height: _height3
                    //})
                });
            }
            else {
                $item.children('.submenu').slideUp('fast');
            }
        });
    }
    
    doOthers() {
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
            if (!container.is(e.target) && container.has(e.target).length === 0) // ... nor a descendant of the container
            {
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
    }
    //模板左上角点击可以缩小左侧导航
    onSmallNavClick() {
        $('#page-wrapper').toggleClass('nav-small');
    }
    onNavLinkClicked(name: string) {

        //添加一个tab

        //设置tab为活动

    }
}
