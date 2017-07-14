import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { AuthService } from './auth.service';

@Injectable()
export class UploadService {    
    private _url = 'http://localhost:5000/api/upload';
    //private _headers = new Headers({ 'Content-Type': 'multipart/form-data' });

    constructor(private _http: Http,
        private _authService: AuthService) { }

    uploadAvatar(file: File) {
        const url = `${this._url}/avatar`;
        let input = new FormData();
        input.append("file", file);

        //let options = new RequestOptions({ headers: this._headers });
        //return this._http.post(url, input);
        return this._authService.sendPost(url, input);
    }
}