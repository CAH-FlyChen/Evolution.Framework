/// <reference path="../../../typings/globals/jquery/index.d.ts" />

import { Component, AfterContentInit, Input } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { OrganizeAddComponent } from './organize-add.component'

declare var $;
declare var JQueryStatic;



@Component({
    selector: 'organize',
    templateUrl: './app/components/organize.component.html'
})
export class OrganizeComponent implements AfterContentInit{

    constructor(private modalService: NgbModal) { }

    ngAfterContentInit() {
        this.gridList();
    }
    open() {
        debugger;
        const modalRef = this.modalService.open(OrganizeAddComponent);
        modalRef.componentInstance.name = 'World';
    }
    gridList() {
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
                    } else if (cellvalue == "Company") {
                        return "公司";
                    } else if (cellvalue == "Department") {
                        return "部门";
                    } else if (cellvalue == "WorkGroup") {
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
    }


    newItem() {
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
    }
    //function btn_edit() {
    //    var keyValue = $("#gridList").jqGridRowValue().Id;
    //    $.modalOpen({
    //        id: "Form",
    //        title: "修改机构",
    //        url: "/SystemManage/Organize/Form?keyValue=" + keyValue,
    //        width: "700px",
    //        height: "520px",
    //        callBack: function (iframeId) {
    //            top.frames[iframeId].submitForm();
    //        }
    //    });
    //}
    //function btn_delete() {
    //    $.deleteForm({
    //        url: "/SystemManage/Organize/DeleteForm",
    //        param: { keyValue: $("#gridList").jqGridRowValue().Id },
    //        success: function () {
    //            $.currentWindow().$("#gridList").resetSelection();
    //            $.currentWindow().$("#gridList").trigger("reloadGrid");
    //        }
    //    })
    //}
    //function btn_details() {
    //    var keyValue = $("#gridList").jqGridRowValue().Id;
    //    $.modalOpen({
    //        id: "Details",
    //        title: "查看机构",
    //        url: "/SystemManage/Organize/Details?keyValue=" + keyValue,
    //        width: "700px",
    //        height: "560px",
    //        btn: null,
    //    });
    //}

}
