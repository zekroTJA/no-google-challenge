import { EventEmitter, Injectable } from '@angular/core';

export enum SnackBarType {
  INFO = 'info',
  ERROR = 'error',
  SUCCESS = 'success',
}

export interface SnackBarEventPayload {
  content: string;
  type: SnackBarType;
}

@Injectable({
  providedIn: 'root',
})
export class SnackBarService {
  onShow = new EventEmitter<SnackBarEventPayload>();
  onHide = new EventEmitter<any>();

  private timer: any;

  show(content: string, type = SnackBarType.INFO, duration = 3500) {
    this.clearTimeout();
    this.onShow.emit({ content, type });
    this.timer = setTimeout(() => this.onHide.emit(), duration);
  }

  hide() {
    this.clearTimeout();
    this.onHide.emit();
  }

  private clearTimeout() {
    if (this.timer) clearTimeout(this.timer);
  }
}
