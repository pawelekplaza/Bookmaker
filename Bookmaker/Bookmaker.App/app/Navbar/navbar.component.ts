import { Component } from '@angular/core';

@Component({
    selector: 'navbar-cmp',
    templateUrl: 'app/Navbar/navbar.component.html',
    styleUrls: [ 'app/Navbar/navbar.component.css' ]
})

export class NavbarComponent {    
    name: string = "The Bookmaker";
}