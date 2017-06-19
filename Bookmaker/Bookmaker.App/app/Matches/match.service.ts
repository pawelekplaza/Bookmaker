import { Injectable } from '@angular/core';
import { IMatch } from './match';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';


@Injectable()
export class MatchService {
    private _url: string = "http://localhost:5000/api/matches";

    constructor(private _http: Http) { }

    getMatches(): Observable<IMatch[]> {
        return this._http.get(this._url)
            .map((response: Response) => <IMatch[]>response.json())
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.log(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}