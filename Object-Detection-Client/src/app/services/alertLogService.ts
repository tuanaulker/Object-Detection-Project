import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AlertLogService {
  private alertReceivedSubject = new Subject<void>();

  alertReceived$ = this.alertReceivedSubject.asObservable();

  constructor() { }

  triggerGetLog() {
    this.alertReceivedSubject.next();
  }
}