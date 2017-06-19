import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../Shared/shared.module';
import { MatchesListComponent } from './matches-list.component';
import { MatchService } from './match.service';

@NgModule({
    imports: [
        SharedModule,        
        RouterModule.forChild([
            { path: 'matches', component: MatchesListComponent }
        ])
    ],
    declarations: [
        MatchesListComponent
    ],
    providers: [
        MatchService
    ],
    exports: [
        MatchesListComponent
    ]
})

export class MatchModule { }