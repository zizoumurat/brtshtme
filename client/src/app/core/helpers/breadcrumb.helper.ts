import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class BreadcrumbHelper {
    private _customLabel = new BehaviorSubject<string | null>(null);
    customLabel$ = this._customLabel.asObservable();

    set(label: string) {
        this._customLabel.next(label);
    }

    clear() {
        this._customLabel.next(null);
    }
}
