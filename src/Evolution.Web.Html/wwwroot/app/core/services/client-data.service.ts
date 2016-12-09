import { Injectable } from '@angular/core';
import { HttpClient } from '../common/http-client';
import 'rxjs/add/operator/toPromise';
//全局serverurl变量
declare var server_url;

@Injectable()
export class ClientDataService {
    private url: string;
    //构造函数
    constructor(private httpClient: HttpClient) {
        this.url = server_url +"/ClientsData/GetClientsDataJson"
    }
    //获取客户端数据
    getClientData(): Promise<any>{
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
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}