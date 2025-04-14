import { Component, computed, Renderer2, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { filter, Subscription } from 'rxjs';
import { AppTopbar } from './app.topbar';
import { AppSidebar } from './app.sidebar';
import { LayoutService } from '@/presentation/layout/service/layout.service';
import { AppConfigurator } from './app.configurator';
import { AppBreadcrumb } from '@/presentation/layout/components/app.breadcrumb';
import { AppFooter } from '@/presentation/layout/components/app.footer';
import { AppSearch } from '@/presentation/layout/components/app.search';
import { AppRightMenu } from '@/presentation/layout/components/app.rightmenu';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-layout',
    standalone: true,
    imports: [CommonModule, AppTopbar, AppSidebar, RouterModule, AppConfigurator, AppBreadcrumb, AppFooter, AppSearch, AppRightMenu, ConfirmDialog, ToastModule, TranslateModule],
    template: `
        <div class="layout-wrapper" [ngClass]="containerClass()">
            <div app-sidebar></div>
            <p-confirmDialog #cdDelete key="deleteConfirm"></p-confirmDialog>
            <p-toast>
            <ng-template let-message pTemplate="message">
                <div class="p-toast-message-text">
                <div class="p-toast-summary">{{ message.summary }}</div>
                <div class="p-toast-detail">{{ message.detail | translate }}</div>
                </div>
            </ng-template>
            </p-toast>
            <div class="layout-content-wrapper">
                <div class="layout-content-wrapper-inside">
                    <div app-topbar></div>
                    <div class="layout-content">
                        <div app-breadcrumb></div>
                        <router-outlet></router-outlet>
                    </div>
                    <div app-footer></div>
                </div>
            </div>
            <app-configurator />
            <div app-search></div>
            <div app-rightmenu></div>
            <div class="layout-mask animate-fadein"></div>
        </div>
    `
})
export class AppLayout {
    overlayMenuOpenSubscription: Subscription;

    menuOutsideClickListener: any;

    menuScrollListener: any;

    @ViewChild(AppSidebar) appSidebar!: AppSidebar;

    @ViewChild(AppTopbar) appTopBar!: AppTopbar;

    constructor(
        public layoutService: LayoutService,
        public renderer: Renderer2,
        public router: Router
    ) {
        this.overlayMenuOpenSubscription = this.layoutService.overlayOpen$.subscribe(() => {
            if (!this.menuOutsideClickListener) {
                this.menuOutsideClickListener = this.renderer.listen('document', 'click', (event) => this.outsideClickListener(event));
            }
            if ((this.layoutService.isHorizontal() || this.layoutService.isSlim() || this.layoutService.isCompact()) && !this.menuScrollListener) {
                this.menuScrollListener = this.renderer.listen(this.appSidebar.menuContainer.nativeElement, 'scroll', (event) => {
                    if (this.layoutService.isDesktop()) {
                        this.hideMenu();
                    }
                });
            }
            if (this.layoutService.layoutState().staticMenuMobileActive) {
                this.blockBodyScroll();
            }
        });

        this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe(() => {
            this.hideMenu();

            const container = document.querySelector('.layout-content-wrapper');
            if (container) {
                (container as HTMLElement).scrollTop = 0;
            }
        });
    }

    outsideClickListener(event: any) {
        if (this.isOutsideClicked(event)) {
            this.hideMenu();
        }
    }

    isOutsideClicked(event: any) {
        const sidebarEl = document.querySelector('.layout-sidebar');
        const topbarButtonEl = document.querySelector('.topbar-left > a');

        return !(sidebarEl?.isSameNode(event.target) || sidebarEl?.contains(event.target) || topbarButtonEl?.isSameNode(event.target) || topbarButtonEl?.contains(event.target));
    }

    hideMenu() {
        this.layoutService.layoutState.update((prev) => ({
            ...prev,
            overlayMenuActive: false,
            staticMenuMobileActive: false,
            menuHoverActive: false
        }));
        if (this.layoutService.isHorizontal()) {
            this.layoutService.onMenuStateChange({ key: '' });
        }

        if (this.menuOutsideClickListener) {
            this.menuOutsideClickListener();
            this.menuOutsideClickListener = null;
        }

        if (this.menuScrollListener) {
            this.menuScrollListener();
            this.menuScrollListener = null;
        }

        this.unblockBodyScroll();
    }

    blockBodyScroll(): void {
        if (document.body.classList) {
            document.body.classList.add('blocked-scroll');
        } else {
            document.body.className += ' blocked-scroll';
        }
    }

    unblockBodyScroll(): void {
        if (document.body.classList) {
            document.body.classList.remove('blocked-scroll');
        } else {
            document.body.className = document.body.className.replace(new RegExp('(^|\\b)' + 'blocked-scroll'.split(' ').join('|') + '(\\b|$)', 'gi'), ' ');
        }
    }

    containerClass = computed(() => {
        const layoutConfig = this.layoutService.layoutConfig();
        const layoutState = this.layoutService.layoutState();

        return {
            'layout-overlay': layoutConfig.menuMode === 'overlay',
            'layout-static': layoutConfig.menuMode === 'static',
            'layout-slim': layoutConfig.menuMode === 'slim',
            'layout-horizontal': layoutConfig.menuMode === 'horizontal',
            'layout-compact': layoutConfig.menuMode === 'compact',
            'layout-reveal': layoutConfig.menuMode === 'reveal',
            'layout-drawer': layoutConfig.menuMode === 'drawer',
            'layout-overlay-active': layoutState.overlayMenuActive || layoutState.staticMenuMobileActive,
            'layout-mobile-active': layoutState.staticMenuMobileActive,
            'layout-static-inactive': layoutState.staticMenuDesktopInactive && layoutConfig.menuMode === 'static',
            'layout-sidebar-active': layoutState.sidebarActive,
            'layout-sidebar-anchored': layoutState.anchored
        };
    });

    ngOnDestroy() {
        if (this.overlayMenuOpenSubscription) {
            this.overlayMenuOpenSubscription.unsubscribe();
        }

        if (this.menuOutsideClickListener) {
            this.menuOutsideClickListener();
        }
    }
}
