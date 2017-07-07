import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../Login/login.service';

@Component({
    templateUrl: 'app/Wallet/topup.component.html',
    styleUrls: [ 'app/Wallet/topup.component.css' ]
})

export class TopupComponent implements OnInit {
    authorized: boolean = false;

    constructor(private _loginService: LoginService,
        private _router: Router) { }

    ngOnInit(): void {
        this._loginService.getAuth()
            .then(x => {
                this.authorized = x;
                if (!x) {
                    this._router.navigate(['/home']);
                }
            });
    }
}