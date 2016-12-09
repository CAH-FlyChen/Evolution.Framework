//自定义Http
import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

@Injectable()
export class HttpClient {

    constructor(private http: Http) { }

    createAuthorizationHeader(headers: Headers) {
        headers.append('Authorization', 'Bearer ' + sessionStorage.getItem("token"));
        headers.append('X-Requested-With','XMLHttpRequest');
    }

    get(url) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.get(url, {
            headers: headers
        }).catch(this.onCatch);
    }

    post(url, data) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.post(url, data, {
            headers: headers
        });
    }

    private onCatch(error: any, caught: Observable<any>): Observable<any> {
        if (error.status == 401)
            window.location.href = '/login.html';
        return Observable.throw(error);
    }
}