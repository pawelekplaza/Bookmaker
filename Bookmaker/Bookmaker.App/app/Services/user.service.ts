import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/toPromise';

import { IUser } from '../Models/user';
import { IUserForCreation } from '../Models/userForCreation';
import { ErrorMessage } from '../Models/error-message';

@Injectable()
export class UserService {
    private _userUrl = 'http://localhost:5000/api/users';
    private _headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private _http: Http) { }

    //_getAll(): Promise<IUser[]> {
    //    return this._http.get(this._userUrl)
    //        .toPromise()
    //        .then(response => response.json().data as IUser[])
    //        .catch(this.handleError);
    //}

    getAll(): Observable<IUser[]> {
        let options = new RequestOptions({ headers: this._headers });
        return this._http.get(this._userUrl, options)
            .map((res: Response) => res.text() ? res.json() : {})
            .catch(this.handleError);
    }

    //_get(email: string): Promise<IUser> {
    //    const url = `${this._userUrl}/${email}`;
    //    return this._http.get(url)
    //        .toPromise()
    //        .then(response => response.json().data as IUser)
    //        .catch(this.handleError);
    //}

    get(email: string): Observable<IUser> {
        let options = new RequestOptions({ headers: this._headers });
        let url = `${this._userUrl}/${email}`;
        return this._http.get(url, options)
            .map((res: Response) => res.text() ? res.json() : {})
            .catch(this.handleError);
    }

    update(user: IUser): Promise<IUser> {
        const url = `${this._userUrl}/${user.email}`;
        return this._http
            .put(url, JSON.stringify(user), { headers: this._headers })
            .toPromise()
            .then(() => user)
            .catch(this.handleError);
    }

    add(user: IUserForCreation): Promise<string> {
        let options = new RequestOptions({ headers: this._headers });
        return this._http
            .post(this._userUrl, JSON.stringify(user), options)
            .toPromise()
            .then(response => JSON.stringify(response.json()));
    }

    delete(email: string): Promise<void> {
        const url = `${this._userUrl}/${email}`;
        return this._http.delete(url, { headers: this._headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.log(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}