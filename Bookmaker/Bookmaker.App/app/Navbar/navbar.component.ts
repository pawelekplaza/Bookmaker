import { Component, OnInit } from '@angular/core';

import { LoginService } from '../Login/login.service';
import { Router, NavigationStart } from '@angular/router';

@Component({
    selector: 'navbar-cmp',
    templateUrl: 'app/Navbar/navbar.component.html',
    styleUrls: [ 'app/Navbar/navbar.component.css' ]
})

export class NavbarComponent implements OnInit {    
    name: string = "The Bookmaker";
    authorized: boolean = false;

    constructor(private _loginService: LoginService,
        private _router: Router) { }

    authorize(): void {
        this._loginService.getAuth()
            .then(x => this.authorized = x)
            .catch(err => console.log(err));
    }

    logout(): void {
        localStorage.setItem('userToken', '_');
        this._router.navigate(['/home']);
    }

    ngOnInit(): void {
        this.authorize();
        this._router.events.subscribe(x => this.authorize());
    }
}