import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../Shared/shared.module';
import { MatchesListComponent } from './matches-list.component';
import { MatchDetailsComponent } from './match-details.component';
import { MatchService } from './match.service';

@NgModule({
    imports: [
        SharedModule,        
        RouterModule.forChild([
            { path: 'matches', component: MatchesListComponent },
            { path: 'matches/:id', component: MatchDetailsComponent }
        ])
    ],
    declarations: [
        MatchesListComponent,
        MatchDetailsComponent
    ],
    providers: [
        MatchService
    ],
    exports: [
        MatchesListComponent,
        MatchDetailsComponent
    ]
})

export class MatchModule { }