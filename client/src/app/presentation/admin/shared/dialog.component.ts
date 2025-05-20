import { Component, Input, Output, EventEmitter, ContentChild, TemplateRef } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dialog',
  standalone: true,
  imports: [CommonModule, DialogModule],
  template: `
    <p-dialog
      [(visible)]="internalVisible"
      (onHide)="onHide()"
      [modal]="true"
      [style]="{ width: width }"
      [breakpoints]="breakpoints"
      [closable]="closable"
      [dismissableMask]="dismissableMask"
      [header]="header"
    >
      <ng-content></ng-content>

      <ng-container *ngIf="footer">
        <ng-template pTemplate="footer">
          <ng-container *ngTemplateOutlet="footer"></ng-container>
        </ng-template>
      </ng-container>

    <ng-container *ngIf="second">
        <ng-template pTemplate="second">
          <ng-container *ngTemplateOutlet="second"></ng-container>
        </ng-template>
      </ng-container>
    </p-dialog>
  `
})
export class AppDialogComponent {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();

  @Input() header: string = '';
  @Input() width: string = '30vw';
  @Input() closable: boolean = true;
  @Input() dismissableMask: boolean = false;

  @Input() breakpoints: { [key: string]: string } = {
    '1920px': '30vw',
    '1366px': '40vw',
    '1024px': '60vw',
    '768px': '75vw',
    '480px': '90vw'
  };

  @ContentChild('footer') footer!: TemplateRef<any>;

  get internalVisible() {
    return this.visible;
  }

  set internalVisible(val: boolean) {
    this.visible = val;
    this.visibleChange.emit(val);
  }

  onHide() {
    this.visibleChange.emit(false);
  }
}
