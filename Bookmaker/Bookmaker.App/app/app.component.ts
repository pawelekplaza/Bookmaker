import { Component } from '@angular/core';
import { Http } from '@angular/http';

import { UserService } from './Services/user.service';
import { User } from './Models/user';
import { ErrorMessage } from './Models/error-message';

@Component({
    selector: 'my-app',
    templateUrl: 'Templates/app.component.html',
    styleUrls: [
        'Styles/app.component.css'
    ],
    moduleId: module.id
})
export class AppComponent {
    name = 'Bookmaker';
    user = new User('', '', '');
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
                }

            })
            .catch(error => console.log(error));
    }

    clearUserData(): void {
        this.user.email = '';
        this.user.username = '';
        this.user.password = '';
    }
}
