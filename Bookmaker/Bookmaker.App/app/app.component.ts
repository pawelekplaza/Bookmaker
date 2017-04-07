import { Component } from '@angular/core';
import { Http } from '@angular/http';

import { UserService } from './Services/user.service';

@Component({
    selector: 'my-app',
    templateUrl: 'Templates/app.component.html',
    styleUrls: [
        'Styles/app.component.css',
        '../bootstrap.css'
    ],
    moduleId: module.id
})
export class AppComponent {
    name = 'Angular 2';

    constructor(private userService: UserService) { }

    sendPost(email: string, username: string, password: string): void {
        this.userService.add(email, username, password)
            .catch(error => console.log(error));
    }
}
