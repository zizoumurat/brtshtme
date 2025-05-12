import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { RegionModel } from '@/core/models/crm/region.model';
import { IRegionService, REGION_SERVICE } from '@/core/services/crm/region-service';

@Component({
  selector: 'app-regions',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './regions.component.html'
})
export class RegionComponent extends AppBaseComponent<RegionModel, IRegionService> {

  constructor(private fb: FormBuilder) {
    super(REGION_SERVICE);
    this.pageTitle = 'Bölge Tanımları';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.initForm();
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      description: [''],
    });
  }
}

