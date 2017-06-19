import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { FooterModule } from './Footer/footer.module';
import { MatchModule } from './Matches/match.module';
import { TeamModule } from './Teams/team.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './Home/home.component';
import { UserService } from './Services/user.service';
import { NavbarComponent } from './Navbar/navbar.component';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,        
        FooterModule,
        MatchModule,
        TeamModule,
        RouterModule.forRoot([
            { path: 'home', component: HomeComponent },
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: '**', redirectTo: 'home', pathMatch: 'full' }], { useHash: true })
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        NavbarComponent
    ],
    providers: [
        UserService
    ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
