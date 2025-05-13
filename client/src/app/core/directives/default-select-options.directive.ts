import { Directive, AfterViewInit } from '@angular/core';
import { Select } from 'primeng/select';

@Directive({
  selector: 'p-select:not([optionLabel]):not([optionValue]), p-multiselect:not([optionLabel]):not([optionValue])',
  standalone: true
})
export class DefaultSelectOptionDirective implements AfterViewInit {
  constructor(private select: Select) {}

  ngAfterViewInit(): void {
    if (!this.select.optionLabel) {
      this.select.optionLabel = 'name';
    }

    if (!this.select.optionValue) {
      this.select.optionValue = 'id';
    }
  }
}
