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
import { LoginComponent } from './Login/login.component';
import { SignUpComponent } from './SignUp/signUp.component';
import { LoginService } from './Login/login.service';
import { LoadingComponent } from './loading.component';
import { WalletComponent } from './Wallet/wallet.component';
import { TopupComponent } from './Wallet/topup.component';
import { MyAccountComponent } from './MyAccount/my-account.component';
import { RefreshService } from './Services/refresh.service';
import { UploadService } from './Services/upload.service';
import { AuthService } from './Services/auth.service';

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
            { path: 'login', component: LoginComponent },
            { path: 'signup', component: SignUpComponent },
            { path: 'loading', component: LoadingComponent },
            { path: 'account', component: MyAccountComponent },
            { path: 'topup', component: TopupComponent },
            { path: '', redirectTo: 'home', pathMatch: 'full' },            
            { path: '**', redirectTo: 'home', pathMatch: 'full' }], { useHash: true })
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        NavbarComponent,
        LoginComponent,
        SignUpComponent,
        LoadingComponent,
        WalletComponent,
        TopupComponent,
        MyAccountComponent
    ],
    providers: [
        UserService,
        LoginService,
        RefreshService,
        UploadService,
        AuthService
    ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
