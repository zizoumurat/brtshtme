import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';

@Component({
    selector: 'app-tasks',
    standalone: true,
    imports: [
      DefaultTableOptionsDirective,
    ],
    templateUrl: './tasks.component.html'
  })
export class TasksComponent {

    constructor(private fb: FormBuilder) {
    }
}

