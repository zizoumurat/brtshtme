import { Directive, OnInit } from '@angular/core';
import { Select } from 'primeng/select';

@Directive({
  selector: 'p-select:not([optionLabel]):not([optionValue]), p-multiselect:not([optionLabel]):not([optionValue])',
  standalone: true
})
export class DefaultSelectOptionDirective implements OnInit {
  constructor(private select: Select) {}

  ngOnInit(): void {
    if (!this.select.optionLabel) {
      this.select.optionLabel = 'name';
    }

    if (!this.select.optionValue) {
      this.select.optionValue = 'id';
    }
  }
}