import { Component } from '@angular/core';

@Component({
    selector: 'loading',
    template: `
<div class="center-block" style="display: table; height:100%">
<img class="img-responsive"
                    style="display: flex"
                    src="Resources/loading.gif" alt="Loading" />
</div>
`
})

export class LoadingComponent {

}