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
var organize_add_component_1 = require('./organize-add.component');
var OrganizeComponent = (function () {
    function OrganizeComponent(modalService) {
        this.modalService = modalService;
    }
    OrganizeComponent.prototype.ngAfterContentInit = function () {
        this.gridList();
    };
    OrganizeComponent.prototype.open = function () {
        debugger;
        var modalRef = this.modalService.open(organize_add_component_1.OrganizeAddComponent);
        modalRef.componentInstance.name = 'World';
    };
    OrganizeComponent.prototype.gridList = function () {
        var $gridList = $("#gridList");
        $gridList.dataGrid({
            treeGrid: true,
            treeGridModel: "adjacency",
            ExpandColumn: "EnCode",
            url: "/SystemManage/Organize/GetTreeGridJson",
            height: $(window).height() - 96,
            colModel: [
                { label: "主键", name: "Id", hidden: true, key: true },
                { label: '名称', name: 'FullName', width: 200, align: 'left' },
                { label: '编号', name: 'EnCode', width: 150, align: 'left' },
                {
                    label: '分类', name: 'CategoryId', width: 80, align: 'left',
                    formatter: function (cellvalue) {
                        if (cellvalue == "Group") {
                            return "集团";
                        }
                        else if (cellvalue == "Company") {
                            return "公司";
                        }
                        else if (cellvalue == "Department") {
                            return "部门";
                        }
                        else if (cellvalue == "WorkGroup") {
                            return "小组";
                        }
                    }
                },
                {
                    label: '创建时间', name: 'CreateTime', width: 80, align: 'left',
                    formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
                },
                {
                    label: "有效", name: "EnabledMark", width: 60, align: "center",
                    formatter: function (cellvalue) {
                        return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                    }
                },
                { label: '备注', name: 'Description', width: 300, align: 'left' }
            ]
        });
        $("#btn_search").click(function () {
            $gridList.jqGrid('setGridParam', {
                postData: { keyword: $("#txt_keyword").val() },
            }).trigger('reloadGrid');
        });
    };
    OrganizeComponent.prototype.newItem = function () {
        $.modalOpen({
            id: "Form",
            title: "新增机构",
            url: "/SystemManage/Organize/Form",
            width: "700px",
            height: "520px",
            callBack: function (iframeId) {
                //top.frames[iframeId].submitForm();
            }
        });
    };
    OrganizeComponent = __decorate([
        core_1.Component({
            selector: 'organize',
            templateUrl: './app/components/organize.component.html'
        }), 
        __metadata('design:paramtypes', [ng_bootstrap_1.NgbModal])
    ], OrganizeComponent);
    return OrganizeComponent;
}());
exports.OrganizeComponent = OrganizeComponent;
