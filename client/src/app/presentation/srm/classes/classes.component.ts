import { Component } from '@angular/core';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-classes',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './classes.component.html'
})
export class ClassesComponent {

  constructor(private router: Router) { }

  tabs = [
    { route: 'list', label: 'srm.class.list', icon: 'pi pi-check-square' },
    { route: 'reports', label: 'reports', icon: 'pi pi-check-square' },
  ];

  activeTab: string = '';

  ngOnInit(): void {
    this.setActiveTab();
  }

  setActiveTab(): void {
    const currentRoute = this.router.url;
    this.activeTab = this.tabs.find(tab => currentRoute.endsWith(tab.route))?.route || '';
  }

  get currentRoute(): string {
    return location.pathname;
  }

  navigate(path: string) {
    this.router.navigate(['/srm/classes/', path]);
    this.activeTab = path;
  }
}

