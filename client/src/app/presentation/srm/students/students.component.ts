import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { IncentiveSettingModel } from '@/core/models/crm/incentiveSetting.model';
import { IncentiveSettingService } from '@/infrastructure/api/crm/incentiveSetting-service';
import { INCENTIVESETTING_SERVICE } from '@/core/services/crm/incentiveSetting-service';
import { ParticipantType } from '@/core/enums/participantType';

@Component({
  selector: 'app-students',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './students.component.html'
})
export class StudentsComponent extends AppBaseComponent<IncentiveSettingModel, IncentiveSettingService> {
  participantType: ParticipantType = ParticipantType.DataProvider;
  
  constructor(private fb: FormBuilder) {
    super(INCENTIVESETTING_SERVICE);
    this.pageTitle = 'Data Sağlayıcı Primlendirme Ayarı';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.searchFilter['participantType'] = this.participantType;
    this.initForm();
  }

  override openModal(): void {
    super.openModal();
    this.pageForm.patchValue({'participantType': this.participantType});
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      participantType: [this.participantType],
      minAmount: [0, [Validators.required, Validators.min(0)]],
      maxAmount: [0, [Validators.required, Validators.min(0)]],
      salesCommission: [0, [Validators.required, Validators.min(0)]],
      collectionCommission: [0, [Validators.required, Validators.min(0)]],
      bonus: [0, [Validators.required, Validators.min(0)]],
      note: ['']
    });
  }
}

