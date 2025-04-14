import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';

@Component({
    selector: 'app-personels',
    standalone: true,
    imports: [
      DefaultTableOptionsDirective,
    ],
    templateUrl: './personel.component.html'
  })
export class PersonelsComponent {

    constructor(private fb: FormBuilder) {
    }
}

