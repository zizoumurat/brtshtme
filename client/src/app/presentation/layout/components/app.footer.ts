import {Component, computed, inject} from '@angular/core';
import {LayoutService} from '@/presentation/layout/service/layout.service';

@Component({
    selector: '[app-footer]',
    standalone: true,
    template: `
        <div class="layout-footer">
            <div class="footer-logo-container">
                <img src="/layout/images/logo-{{ isDarkTheme() ? 'white' : 'dark' }}.svg" alt="poseidon-layout"/>
                <span class="footer-app-name">BRITISHTIME</span>
            </div>
            <span class="footer-copyright">&#169; BritishTime - 2025</span>
        </div>
    `
})
export class AppFooter {
    layoutService = inject(LayoutService);

    isDarkTheme = computed(() => this.layoutService.isDarkTheme());
}
