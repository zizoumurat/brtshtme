import { Component, Input } from '@angular/core';

@Component({
    selector: 'auth-logo-widget',
    standalone: true,
    template: `<img src="images/logo.png" />`,
})
export class AuthLogoWidget {
    @Input() className: string = '';
}
