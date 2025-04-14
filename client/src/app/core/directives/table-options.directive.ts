import { Directive, Input, OnInit } from '@angular/core';
import { Table } from 'primeng/table';

@Directive({
  selector: 'p-table',
  standalone: true,
})
export class DefaultTableOptionsDirective implements OnInit {
  @Input() defaultRows: number = 10;

  constructor(private table: Table) { }

  ngOnInit() {
    this.table.rows = this.defaultRows;
    this.table.rowsPerPageOptions = [10, 20, 50];
  }
}
