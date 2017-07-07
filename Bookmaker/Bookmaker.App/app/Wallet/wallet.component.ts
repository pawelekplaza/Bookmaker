import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'

import { UserService } from '../Services/user.service';
import { IUser } from '../Models/user';

@Component({
    selector: 'wallet-cmp',
    templateUrl: 'app/Wallet/wallet.component.html',
    styleUrls: [ 'app/Wallet/wallet.component.css' ]
})

export class WalletComponent implements OnInit {
    walletPoints: number;

    constructor(private _userService: UserService,
        private _router: Router) { }

    ngOnInit(): void {
        this._userService.get(localStorage.getItem('userEmail'))
            .subscribe(x => this.walletPoints = x.walletPoints);            
    }

}