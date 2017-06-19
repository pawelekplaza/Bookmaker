import { Component, OnInit } from '@angular/core';

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

    constructor(private _matchService: MatchService, private _teamService: TeamService) { }

    ngOnInit(): void {
        this._matchService.getMatches()
            .subscribe(v => {
                this.matches = v;

                this.matches.forEach((item, index) => {
                    this._teamService.getTeam(this.matches[index].hostTeamId).subscribe(v => this.matches[index].hostTeamName = v.name);
                    this._teamService.getTeam(this.matches[index].guestTeamId).subscribe(v => this.matches[index].guestTeamName = v.name);
                });

            }, error => this.errorMessage = <any>error);        
    }    
}