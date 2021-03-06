﻿import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthService {    
    constructor(private _http: Http) { }

    sendGet(url: string): Observable<Response> {
        let headers = new Headers({ 'Authorization': `Bearer ${localStorage.getItem('userToken')}` });
        let options = new RequestOptions({ headers: headers });

        return this._http.get(url, options);
    }

    sendPost(url: string, body: any): Observable<Response> {
        let headers = new Headers({ 'Authorization': `Bearer ${localStorage.getItem('userToken')}` });
        let options = new RequestOptions({ headers: headers });

        return this._http.post(url, body, options);
    }

    sendPut(url: string, body: any, contentType: string = null): Observable<Response> {
        let headers = new Headers({ 'Authorization': `Bearer ${localStorage.getItem('userToken')}` });
        if (contentType) {
            headers.append('Content-Type', contentType);
        }
        let options = new RequestOptions({ headers: headers });

        return this._http.put(url, body, options);
    }
}