import { Component } from '@angular/core';
import { Http } from '@angular/http';

import { UserService } from './Services/user.service';
import { IUser } from './Models/user';
import { ErrorMessage } from './Models/error-message';

@Component({
    selector: 'my-app',
    templateUrl: 'app.component.html',
    styleUrls: [
        'app.component.css'
    ],
    moduleId: module.id
})
export class AppComponent {
    name: string = "The Bookmaker";
    user: IUser =
    {
        email: '',
        password: '',
        username: ''
    };
    errorMessage: string;

    constructor(private userService: UserService) { }

    sendPost(): void {
        this.userService.add(this.user.email, this.user.username, this.user.password)
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

    clearUserData(): void {
        this.user.email = '';
        this.user.username = '';
        this.user.password = '';
    }
}
