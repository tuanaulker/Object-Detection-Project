import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UpdateService {
  updateEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor() { }

  triggerUpdate(): void {
    this.updateEvent.emit();
  }
}
