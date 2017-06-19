import { Component } from '@angular/core';

@Component({
    selector: 'footer-cmp',
    templateUrl: 'app/Footer/footer.component.html',
    styleUrls: [ 'app/Footer/footer.component.css' ]
})

export class FooterComponent {
    footerContent: string = "Copyright © 2016-2017 The Bookmaker - Sportsbook. All Rights Reserved.";
}