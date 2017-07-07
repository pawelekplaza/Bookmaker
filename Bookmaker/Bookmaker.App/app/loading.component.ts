import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { LoginService } from './Login/login.service';

@Component({
    selector: 'loading',
    templateUrl: 'app/loading.component.html',
    styleUrls: [ 'app/loading.component.css' ]
})

export class LoadingComponent implements OnInit {
    incorrectCredentials: boolean = false;

    constructor(private _router: Router,
        private _loginService: LoginService) { }

    goBack(): void {
        this._router.navigate(['/login']);
    }

    ngOnInit(): void {            
        this._loginService.credentialsChanged.subscribe(() => {
            this.incorrectCredentials = localStorage.getItem('incorrectCredentials') ? true : false;
        });
    }
}