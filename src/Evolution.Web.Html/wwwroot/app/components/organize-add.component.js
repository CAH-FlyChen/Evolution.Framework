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
var OrganizeComponent = (function () {
    function OrganizeComponent() {
    }
    OrganizeComponent.prototype.ngAfterContentInit = function () {
        this.gridList();
    };
    OrganizeComponent.prototype.gridList = function () {
        var $gridList = $("#gridList");
        $gridList.dataGrid({
            treeGrid: true,
            treeGridModel: "adjacency",
            ExpandColumn: "EnCode",
            url: server_url + "/SystemManage/Organize/GetTreeGridJson",
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
    OrganizeComponent = __decorate([
        core_1.Component({
            selector: 'organize',
            templateUrl: './app/components/organize.component.html'
        }), 
        __metadata('design:paramtypes', [])
    ], OrganizeComponent);
    return OrganizeComponent;
}());
exports.OrganizeComponent = OrganizeComponent;
//# sourceMappingURL=organize.component.js.map