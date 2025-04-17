import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { TextareaModule } from 'primeng/textarea';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { DialogModule } from 'primeng/dialog';
import { SelectModule } from 'primeng/select';
import { MultiSelectModule } from 'primeng/multiselect';
import { DatePickerModule } from 'primeng/datepicker';
import { FullCalendarModule } from '@fullcalendar/angular';
import { InputMaskModule } from 'primeng/inputmask';
import { CheckboxModule } from 'primeng/checkbox';
import { InputGroupModule } from 'primeng/inputgroup';
import { FluidModule } from 'primeng/fluid';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { CardModule } from 'primeng/card';
import { TagModule } from 'primeng/tag';
import { TabsModule } from 'primeng/tabs';
import { MenuModule } from 'primeng/menu';
import { ValidationMessageComponent } from './validation-message.component';
import { TranslateModule } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    TableModule,
    InputTextModule,
    InputNumberModule,
    TextareaModule,
    InputGroupModule,
    ToolbarModule,
    RatingModule,
    TooltipModule,
    ConfirmDialogModule,
    ToastModule,
    DialogModule,
    SelectModule,
    MultiSelectModule,
    DatePickerModule,
    FullCalendarModule,
    InputMaskModule,
    CheckboxModule,
    FluidModule,
    IconFieldModule,
    InputIconModule,
    CardModule,
    TagModule,
    TabsModule,
    MenuModule,
    ValidationMessageComponent,
    TranslateModule,
    RouterModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    TableModule,
    InputTextModule,
    InputNumberModule,
    TextareaModule,
    InputGroupModule,
    ToolbarModule,
    RatingModule,
    TooltipModule,
    ConfirmDialogModule,
    ToastModule,
    DialogModule,
    SelectModule,
    MultiSelectModule,
    DatePickerModule,
    FullCalendarModule,
    InputMaskModule,
    CheckboxModule,
    FluidModule,
    IconFieldModule,
    InputIconModule,
    CardModule,
    TagModule,
    TabsModule,
    MenuModule,
    TranslateModule,
    ValidationMessageComponent,
    RouterModule
  ]
})
export class SharedComponentModule { }
