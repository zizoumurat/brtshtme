import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app.config';
import { AppComponent } from './app.component';

import '@angular/common/locales/global/tr';

bootstrapApplication(AppComponent, appConfig).catch((err) => console.error(err));

