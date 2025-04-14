import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SharedComponentModule } from '../admin/shared/shared-components.module';

@Component({
    selector: 'app-crm-layout',
    standalone: true,
    imports: [RouterModule, SharedComponentModule],
    templateUrl: './crm.component.html'
})
export class CrmComponent {
    constructor(private router: Router) { }

    tabs = [
        { route: '/crm/tasks', label: 'crm.tabs.myTasks', icon: 'pi pi-check-square' },
        { route: '/crm/my-data', label: 'crm.tabs.myData', icon: 'pi pi-database' },
        { route: '/crm/transactions', label: 'crm.tabs.myTransactions', icon: 'pi pi-briefcase' },
        { route: '/crm/field-special', label: 'crm.tabs.fieldSpecial', icon: 'pi pi-map-marker' },
        { route: '/crm/reports', label: 'crm.tabs.reports', icon: 'pi pi-chart-bar' },
        { route: '/crm/settings', label: 'crm.tabs.settings', icon: 'pi pi-cog' }
    ];

    activeTab: string = '';

    ngOnInit(): void {
        this.router.events.subscribe(() => {
            this.setActiveTab();
        });

        this.setActiveTab();
    }

    setActiveTab(): void {
        const currentRoute = this.router.url;
        this.activeTab = this.tabs.find(tab => currentRoute.startsWith(tab.route))?.route || '';
    }

    get currentRoute(): string {
        return location.pathname;
    }

    navigate(path: string) {
        this.router.navigate(['/crm', path]);
    }

    isActive(path: string): boolean {
        return this.router.url.includes(path);
    }
}
