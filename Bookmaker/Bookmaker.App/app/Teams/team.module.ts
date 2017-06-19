import { NgModule } from '@angular/core';

import { SharedModule } from '../Shared/shared.module';
import { TeamService } from './team.service';

@NgModule({
    imports: [
        SharedModule
    ],
    providers: [
        TeamService
    ]
})

export class TeamModule { }