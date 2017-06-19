import { Injectable } from '@angular/core';
import { ITeam } from './team';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';


@Injectable()
export class TeamService {
    private _url: string = "http://localhost:5000/api/teams";    

    constructor(private _http: Http) { }

    getTeams(): Observable<ITeam[]> {
        return this._http.get(this._url)
            .map((response: Response) => <ITeam[]>response.json())
            .catch(this.handleError);
    }

    getTeam(id: number): Observable<ITeam> {
        const url = `${this._url}/${id}`;
        return this._http.get(url)
            .map((response: Response) => <ITeam>response.json())
            .catch(this.handleError);        
    }

    private handleError(error: Response) {
        console.log(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}