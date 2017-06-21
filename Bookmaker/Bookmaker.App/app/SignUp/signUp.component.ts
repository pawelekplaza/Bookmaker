import { Component } from '@angular/core';

import { UserService } from '../Services/user.service';
import { IUser } from '../Models/user';
import { ErrorMessage } from '../Models/error-message';

@Component({
    templateUrl: 'app/SignUp/signUp.component.html',
    styleUrls: [ 'app/SignUp/signUp.component.css' ]
})

export class SignUpComponent {
    errorMessage: string;
    username: string = '';
    userEmail: string = '';
    userEmailConfirm: string = '';
    userPassword: string = '';
    userPasswordConfirm: string = '';

    constructor(private _userService: UserService) { }

    sendPost(): void {
        this._userService.add(this.userEmail, this.username, this.userPassword)
            .then(value => {
                if (value) {
                    let jsonMessage = (JSON.parse(value) as ErrorMessage).message;
                    this.errorMessage = jsonMessage;
                }
                else {
                    this.clearUserData();
                    this.errorMessage = '-';
                }

            })
            .catch(error => { console.log(error); this.errorMessage = 'catch'; });
    }

    private clearUserData(): void {
        this.username = '';
        this.userEmail = '';
        this.userEmailConfirm = '';
        this.userPassword = '';
        this.userPasswordConfirm = '';
    }
}