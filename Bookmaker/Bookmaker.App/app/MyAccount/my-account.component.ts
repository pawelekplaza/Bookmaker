import { Component, OnInit } from '@angular/core';

import { LoginService } from '../Login/login.service';
import { UserService } from '../Services/user.service';
import { IUser } from '../Models/user';

@Component({
    templateUrl: 'app/MyAccount/my-account.component.html'
})

export class MyAccountComponent implements OnInit {
    authorized: boolean = false;
    viewPrepared: boolean = false;
    user: IUser;

    constructor(private _loginService: LoginService,
        private _userService: UserService) { }

    ngOnInit(): void {
        this._loginService.getAuth().then(x => {
            this.authorized = x;
            this.viewPrepared = true;

            if (this.authorized) {
                this._userService.get(localStorage.getItem('userEmail')).subscribe(x => this.user = x);
            }
        });
    }
}