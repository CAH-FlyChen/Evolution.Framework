/// <reference path="../../../../typings/globals/es6-shim/index.d.ts" />

import { Component, OnInit, AfterContentInit } from '@angular/core';
import { Location } from '@angular/common';
import 'rxjs/add/operator/map';
import { enableProdMode } from '@angular/core';
import { HttpClient } from './http-client';
import { ClientDataService } from '../services/client-data.service';
import { ClientData } from '../domain/client-data';


declare var jQuery;
declare var storage;
declare var $;
/**
 * 暂时没有使用
 */
@Component({
    selector: 'side-bar',
    templateUrl: './side-bar.component.html',
    providers: [HttpClient, ClientDataService, ClientData]
})
export class SideBarComponent implements OnInit {
    clientData: ClientData;

    constructor(private service: ClientDataService) {
        this.clientData = new ClientData();
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
}
