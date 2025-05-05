import { BRANCH_SERVICE } from '@/core/services/crm/branch-service';
import { Component, OnInit, Input, Output, EventEmitter, inject } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ValidationMessageComponent } from './validation-message.component';
import { SelectModule } from 'primeng/select';
import { DefaultSelectOptionDirective } from '@/core/directives/default-select-options.directive';

@Component({
  selector: 'app-select-branch',
  standalone: true,
  imports:[ValidationMessageComponent,SelectModule, DefaultSelectOptionDirective, ReactiveFormsModule],
  template: `
   <div [formGroup]="form" class="grid grid-cols-12 gap-4">
    <div class="col-span-12 md:col-span-6 mb-4">
      <label for="branchId">Şube</label>
      <p-select 
        id="branchId" 
        [options]="branchOptions" 
        class="w-full" 
        formControlName="branchId"
        placeholder="Seçiniz" appendTo="body"
        [class.ng-dirty.ng-invalid]="isFieldInvalid()"
      />
      <app-validation-message [control]="form.controls['branchId']"></app-validation-message>
    </div>
  </div>
  `
})
export class SelectBranchComponent implements OnInit {
  @Input() form!: FormGroup; // FormGroup'u input olarak alıyoruz
  @Input() branchOptions: any[] = []; // Şube seçeneklerini dışarıdan alacağız
  @Output() branchSelected = new EventEmitter<any>(); // Şube seçildiğinde dışarıya bildireceğiz

  constructor() {}

  branchService = inject(BRANCH_SERVICE);

  ngOnInit() {
    this.loadBranches();
  }

  // Şubeleri API'den çekme
  async loadBranches() {
    this.branchOptions = await this.branchService.getUserBranchList();
  }

  // Form kontrolünün geçerliliğini kontrol et
  isFieldInvalid(): boolean {
    const control = this.form.controls['branchId'];
    return control && control.invalid && (control.dirty || control.touched);
  }
}
