import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class EventService {
  private events = new Map<string, Subject<void>>();

  on(eventName: string) {
    if (!this.events.has(eventName)) {
      this.events.set(eventName, new Subject<void>());
    }
    return this.events.get(eventName)!.asObservable();
  }

  trigger(eventName: string) {
    this.events.get(eventName)?.next();
  }
}
