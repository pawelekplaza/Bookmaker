import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

//import { SharedModule } from '../Shared/shared.module';
import { IMatch } from './match';
import { ITeam } from '../Teams/team';
import { MatchService } from './match.service';
import { TeamService } from '../Teams/team.service';

@Component({
    templateUrl: 'app/Matches/matches-list.component.html',
    styleUrls: [ 'app/Matches/matches-list.component.css' ]
})

export class MatchesListComponent implements OnInit {
    matches: IMatch[];
    errorMessage: string;

    constructor(private _matchService: MatchService,
        private _teamService: TeamService,
        private _router: Router) { }

    ngOnInit(): void {
        this._matchService.getMatches()
            .subscribe(v => this.matches = v,
                    error => this.errorMessage = error);        
    }

    goToDetails(id: number): void {
        this._router.navigate([`/matches/${id}`]);
    }
}