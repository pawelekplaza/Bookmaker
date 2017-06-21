import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { IMatch } from './match';
import { MatchService } from './match.service';
import { LoginType } from '../Models/loginType';
import { LoginTypeDecorator } from '../Models/loginType.decorator';

@Component({
    templateUrl: 'app/Matches/match-details.component.html',
    styleUrls: [ 'app/Matches/match-details.component.css' ]
})

@LoginTypeDecorator    
export class MatchDetailsComponent implements OnInit {
    match: IMatch;
    loginType: LoginType = LoginType.None;
    notFoundMessage: string = '';    
    errorMessage: string;

    constructor(private _activatedRoute: ActivatedRoute,
        private _matchService: MatchService) { }

    ngOnInit(): void {
        let id = +this._activatedRoute.snapshot.params['id'];
        this._matchService.getMatch(id).subscribe(value => this.showMatchDetails(value),
            error => this.errorMessage = error);        
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