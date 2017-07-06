import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';

@Injectable()
export class LoginService {
    private _urlToken: string = 'http://localhost:5000/api/account/token';
    private _urlAuth: string = 'http://localhost:5000/api/account/auth';

    constructor(private _http: Http) { }

    getToken(email: string, password: string): Promise<any> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let data = new Object({ email: email, password: password });

        return this._http.post(this._urlToken, JSON.stringify(data), options)
            .toPromise()
            .then((res: Response) => res.text() ? res.json().token : {})
            .catch(err => console.log(err));
    }

    getAuth(): Promise<boolean> {
        let headers = new Headers({'Content-Type': 'application/json', 'Authorization': `Bearer ${localStorage.getItem('userToken')}` });
        let options = new RequestOptions({ headers: headers });

        return this._http.get(this._urlAuth, options)
            .toPromise()
            .then((res: Response) => res.text() ? res.text() === "Auth" : false)
            .catch(err => console.log(err));
    }
}
