import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SharedComponentModule } from '../admin/shared/shared-components.module';

@Component({
    selector: 'app-srm-layout',
    standalone: true,
    imports: [RouterModule, SharedComponentModule],
    templateUrl: './srm.component.html'
})
export class SrmComponent {

    displayModal: boolean = false;

    constructor(private router: Router) { }

    tabs = [
        { route: 'students', label: 'srm.tabs.students', icon: 'pi pi-check-square' },
        { route: 'classes', label: 'srm.tabs.classes', icon: 'pi pi-database' },
    ];

    activeTab: string = '';


    ngOnInit(): void {
        this.router.events.subscribe(() => {
            this.setActiveTab();
        });

        this.setActiveTab();
    }

    get currentRoute(): string {
        return location.pathname;
    }

    setActiveTab(): void {
        const currentRoute = this.router.url;
        this.activeTab = this.tabs.find(tab => currentRoute.endsWith(tab.route))?.route || '';
    }

    navigate(path: string) {
        this.router.navigate(['/crm', path]);
    }

    isActive(path: string): boolean {
        return this.router.url.includes(path);
    }
}
