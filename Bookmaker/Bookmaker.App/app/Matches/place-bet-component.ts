import { Component, OnInit } from '@angular/core';

import { UserService } from '../Services/user.service';
import { RefreshService } from '../Services/refresh.service';

@Component({
    selector: 'place-bet-cmp',
    templateUrl: 'app/Matches/place-bet.component.html',
    styleUrls: [ 'app/Matches/place-bet.component.css' ]
})

export class PlaceBetComponent implements OnInit {
    userPoints: number;
    walletPoints: number;    
    betPlaced = false;
    information = '';

    constructor(private _userService: UserService,
        private _refreshService: RefreshService) { } 

    placeBet(): void {
        this._userService.update({ email: localStorage.getItem('userEmail'), walletPoints: (this.userPoints - this.walletPoints) })
            .then(x => {
                this.betPlaced = true;
                this._refreshService.emitRefresh();
                this.information = 'Bet placed successfully!';
                document.getElementById('walletInput').setAttribute('disabled', 'disabled');
            });
    }

    ngOnInit(): void {
        this.getUserPoints();
    }

    getUserPoints(): void {
        this._userService.get(localStorage.getItem('userEmail'))
            .subscribe(x => this.userPoints = x.walletPoints);
    }

    checkBet(): void {
        if (this.userPoints < this.walletPoints) {
            this.information = 'A value is too high!';
            document.getElementsByClassName('placeBetButton')[0].setAttribute('disabled', 'disabled');
        }
        else {
            this.information = '';
            document.getElementsByClassName('placeBetButton')[0].removeAttribute('disabled');
        }
    }
}