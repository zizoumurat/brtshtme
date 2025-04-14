import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { TranslateModule } from '@ngx-translate/core';
import { PrimeNG } from 'primeng/config'; 

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterModule, TranslateModule],  // TranslateModule'ü burada import ediyoruz
    template: `<router-outlet></router-outlet>`
})
export class AppComponent implements OnInit {

    constructor(private config: PrimeNG, private translateService: TranslateService) {
    }

    ngOnInit() {
        this.translateService.setDefaultLang('tr');

        // 'tr' dilini kullanmaya başlıyoruz.
        this.translateService.use('tr').subscribe(() => {
            // PrimeNG çevirisini 'primeng' anahtarına göre alıyoruz ve PrimeNGConfig'e set ediyoruz.
            this.translateService.get('primeng').subscribe((res: any) => {
                this.config.setTranslation(res); // PrimeNG çevirisi burada ayarlanıyor.
            });
        });
    }
}
