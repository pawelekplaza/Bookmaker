import { Component, OnInit, ViewChild } from '@angular/core';
import { Response } from '@angular/http';
import { Router } from '@angular/router';

import { LoginService } from '../Login/login.service';
import { UserService } from '../Services/user.service';
import { IUser } from '../Models/user';
import { UploadService } from '../Services/upload.service';

@Component({
    templateUrl: 'app/MyAccount/my-account.component.html',
    styleUrls: [ 'app/MyAccount/my-account.component.css' ]
})

export class MyAccountComponent implements OnInit {
    authorized: boolean = false;
    viewPrepared: boolean = false;
    user: IUser;
    avatarUrl: string;
    @ViewChild("avatarInput") avatarInput: any;

    constructor(private _loginService: LoginService,
        private _userService: UserService,
        private _uploadService: UploadService,
        private _router: Router) { }

    ngOnInit(): void {
        this._loginService.getAuth().then(x => {
            this.authorized = x;
            this.viewPrepared = true;

            if (this.authorized) {
                this._userService.get(localStorage.getItem('userEmail')).subscribe(x => {
                    this.user = x;
                    let avatarFileName = this.user.avatarFileName;
                    this.avatarUrl = `http://localhost:5000/api/upload/file/${avatarFileName}`;
                });                
            }
        });
    }

    saveChanges(): void {
        let avatarInput = this.avatarInput.nativeElement;

        if (avatarInput.files && avatarInput.files[0]) {
            let imageToUpload = avatarInput.files[0];
            this._uploadService.uploadAvatar(imageToUpload)
                .subscribe((res: Response) => {
                    //this._userService.update(
                    console.log(res);
                });
        }
    }
}