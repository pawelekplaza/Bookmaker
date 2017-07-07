import { Injectable, EventEmitter, Output } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Router } from '@angular/router';

import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';

import { IJwt } from '../Models/jwt';

@Injectable()
export class LoginService {
    private _urlToken: string = 'http://localhost:5000/api/account/token';
    private _urlAuth: string = 'http://localhost:5000/api/account/auth';
    @Output() credentialsChanged: EventEmitter<any> = new EventEmitter();

    constructor(private _http: Http,
        private _router: Router) { }

    getToken(email: string, password: string): Promise<IJwt> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let data = new Object({ email: email, password: password });

        return this._http.post(this._urlToken, JSON.stringify(data), options)
            .toPromise()
            .then((res: Response) => {
                if (!res.text()) {
                    return {};
                }

                this.setStorageEmail(email);
                return res.json() as IJwt;
            })
            .catch(err => console.log(err));
    }

    getAuth(): Promise<boolean> {
        let headers = new Headers({'Content-Type': 'application/json', 'Authorization': `Bearer ${localStorage.getItem('userToken')}` });
        let options = new RequestOptions({ headers: headers });

        return this._http.post(this._urlAuth, { email: localStorage.getItem('userEmail') }, options)
            .toPromise()
            .then((res: Response) => {                
                if (!res.text()) {
                    this.setStorageToken('_');
                    return false;
                }

                let jwt = res.json() as IJwt;
                if (jwt === null) {
                    this.setStorageToken('_');
                    return false;
                }

                if (jwt.token.length < 50) {
                    this.setStorageToken('_');
                    return false;
                }

                this.setStorageEmail(jwt.email);
                this.setStorageToken(jwt.token);
                return true;
            })
            .catch(err => console.log(err));
    }

    login(email: string, password: string): void {
        this.getToken(email, password)
            .then(x => {
                if (x as IJwt) {
                    localStorage.setItem('userToken', x.token);
                    localStorage.setItem('incorrectCredentials', null);
                    this._router.navigate(['/home']);
                }
                else {
                    localStorage.setItem('incorrectCredentials', 'true');
                }
                this.credentialsChanged.emit();
            });
    }

    private setStorageEmail(email: string) {
        localStorage.setItem('userEmail', email);
    }

    private setStorageToken(token: string) {
        localStorage.setItem('userToken', token);
    }
}
