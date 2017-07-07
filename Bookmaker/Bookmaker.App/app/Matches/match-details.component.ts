import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { IMatch } from './match';
import { MatchService } from './match.service';
import { LoginService } from '../Login/login.service';
import { UserService } from '../Services/user.service';
import { LoginType } from '../Models/loginType';
import { LoginTypeDecorator } from '../Models/loginType.decorator';
import { PlaceBetComponent } from './place-bet-component';

@Component({
    templateUrl: 'app/Matches/match-details.component.html',
    styleUrls: [ 'app/Matches/match-details.component.css' ]
})

@LoginTypeDecorator    
export class MatchDetailsComponent implements OnInit {
    match: IMatch;
    loginType: LoginType = LoginType.None;
    notFoundMessage = '';    
    errorMessage: string;
    userChecked = false;
    authorized = false;

    constructor(private _activatedRoute: ActivatedRoute,
        private _matchService: MatchService,
        private _loginService: LoginService,
        private _userService: UserService) { }

    ngOnInit(): void {
        let id = +this._activatedRoute.snapshot.params['id'];
        this._matchService.getMatch(id).subscribe(value => this.showMatchDetails(value),
            error => this.errorMessage = error);   

        this._loginService.getAuth().then(x => {
            this.authorized = x;
            this.userChecked = true;

            if (this.authorized) {
                this._userService.get(localStorage.getItem('userEmail')).subscribe(user => {
                    if (user.role.localeCompare('user') === 0) {
                        this.loginType = LoginType.User;
                    }
                    else if (user.role.localeCompare('admin') === 0) {
                        this.loginType = LoginType.Admin;
                    }
                });
            }
        });
    }

    private showMatchDetails(m: IMatch): void {
        this.match = m;
        this.match.startTime = new Date(this.match.startTime);

        if (this.match === null) {
            this.notFoundMessage = `Match with id "${+this._activatedRoute.snapshot.params['id']}" does not exist.`;
        }
        else {
            this.notFoundMessage = '';            
        }            
    }
}