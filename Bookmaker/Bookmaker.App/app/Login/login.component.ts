import { Component, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';

import { LoginService } from './login.service';

@Component({    
    templateUrl: 'app/Login/login.component.html',
    styleUrls: [ 'app/Login/login.component.css' ]
})

export class LoginComponent implements AfterViewInit {
    inputEmail: string;
    inputPassword: string;

    authValid: boolean = false;


    constructor(private _loginService: LoginService,
        private _router: Router) { }

    private login(email: string, password: string): void {
        this._loginService.getToken(email, password)
            .then(x => {
                localStorage.setItem('userToken', x);
                this._router.navigate(['/home']);
            });
    }

    private checkAuth(): void {
        this._loginService.getAuth()
            .then(x => this.authValid = x);
    }

    ngAfterViewInit(): void {
        document.getElementById('userEmail').focus();
    }
}