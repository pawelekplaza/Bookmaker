import { Component, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';

import { LoginService } from './login.service';
import { IJwt } from '../Models/jwt';

@Component({    
    templateUrl: 'app/Login/login.component.html',
    styleUrls: [ 'app/Login/login.component.css' ]
})

export class LoginComponent implements AfterViewInit {
    inputEmail: string;
    inputPassword: string;

    constructor(private _loginService: LoginService,
        private _router: Router) { }

    private login(email: string, password: string): void {
        this._loginService.login(email, password);
    }

    ngAfterViewInit(): void {
        document.getElementById('userEmail').focus();
    }
}