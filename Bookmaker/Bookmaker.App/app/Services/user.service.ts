﻿import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { User } from '../Models/user';
import { ErrorMessage } from '../Models/error-message';

@Injectable()
export class UserService {
    private userUrl = 'users';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http) { }

    getAll(): Promise<User[]> {
        return this.http.get(this.userUrl)
            .toPromise()
            .then(response => response.json().data as User[])
            .catch(this.handleError);
    }

    get(id: number): Promise<User> {
        const url = `${this.userUrl}/${id}`;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json().data as User)
            .catch(this.handleError);
    }

    update(user: User): Promise<User> {
        const url = `${this.userUrl}/${user.email}`;
        return this.http
            .put(url, JSON.stringify(user), { headers: this.headers })
            .toPromise()
            .then(() => user)
            .catch(this.handleError);
    }

    add(email: string, username: string, password: string): Promise<string> {
        return this.http
            .post('http://localhost:5000/users', JSON.stringify({ email: email, password: password, username: username }), { headers: this.headers })
            .toPromise()
            .then(response => JSON.stringify(response.json()))
            .catch(this.handleError);
    }

    delete(email: string): Promise<void> {
        const url = `${this.userUrl}/${email}`;
        return this.http.delete(url, { headers: this.headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}