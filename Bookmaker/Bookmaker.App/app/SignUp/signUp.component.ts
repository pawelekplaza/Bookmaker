import { Component, AfterViewInit } from '@angular/core';
import { Response } from '@angular/http';
import { Router } from '@angular/router';

import { UserService } from '../Services/user.service';
import { ErrorMessage } from '../Models/error-message';
import { LoginService } from '../Login/login.service';
import { IUserForCreation } from '../Models/userForCreation';

@Component({
    templateUrl: 'app/SignUp/signUp.component.html',
    styleUrls: [ 'app/SignUp/signUp.component.css' ]
})

export class SignUpComponent implements AfterViewInit {
    errorMessage: string = '';
    username: string = '';
    userEmail: string = '';
    userEmailConfirm: string = '';
    userPassword: string = '';
    userPasswordConfirm: string = '';
    registerButtonEnabled: boolean = false;
    count: number = 0;

    constructor(private _userService: UserService,
        private _router: Router,
        private _loginService: LoginService) { }

    register(): void {
        let user: IUserForCreation = {
            email: this.userEmail,
            username: this.username,
            password: this.userPassword
        };

        this._userService.add(user)
            .then(value => {
                if (value) {
                    let jsonMessage = (JSON.parse(value) as ErrorMessage).message;
                    this.errorMessage = jsonMessage;
                    if (!jsonMessage) {
                        this._router.navigate(['/loading']);
                        this._loginService.login(this.userEmail, this.userPassword);
                    }
                }
                else {
                    this.clearUserData();
                    this.errorMessage = '';
                }

            })
            .catch((res: Response) => {
                console.log(res);
                this.errorMessage = res.toString();
            });
    }

    ngAfterViewInit(): void {
        document.getElementById('username').focus();
    }

    private registerOnEnter(): void {
        if (this.registerButtonEnabled) {
            this.register();
        }
    }

    private clearUserData(): void {
        this.username = '';
        this.userEmail = '';
        this.userEmailConfirm = '';
        this.userPassword = '';
        this.userPasswordConfirm = '';
    }

    private validateData(): void {
        if (this.userEmail.localeCompare(this.userEmailConfirm)) {
            this.registerButtonEnabled = false;
            return;
        }

        if (this.userPassword.localeCompare(this.userPasswordConfirm)) {
            this.registerButtonEnabled = false;
            return;
        }

        if (this.username.length === 0 ||
            this.userPassword.length === 0 ||
            this.userPasswordConfirm.length === 0 ||
            this.userEmail.length === 0 ||
            this.userEmailConfirm.length === 0) {
            this.registerButtonEnabled = false;
            return;
        }

        this.registerButtonEnabled = true;
    }    

    private clearUsername(): void {
        this.username = '';
    }
}