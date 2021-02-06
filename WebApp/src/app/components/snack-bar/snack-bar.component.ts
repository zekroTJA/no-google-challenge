import { Component, OnInit } from '@angular/core';
import {
  SnackBarEventPayload,
  SnackBarService,
  SnackBarType,
} from './snack-bar.service';

@Component({
  selector: 'app-snack-bar',
  templateUrl: './snack-bar.component.html',
  styleUrls: ['./snack-bar.component.scss'],
})
export class SnackBarComponent implements OnInit {
  show = false;
  content = '';
  type = SnackBarType.INFO;

  constructor(private service: SnackBarService) {
    this.service.onShow.subscribe(this.onShow.bind(this));
    this.service.onHide.subscribe(this.onHide.bind(this));
  }

  ngOnInit(): void {}

  onShow(payload: SnackBarEventPayload) {
    this.show = true;
    this.type = payload.type;
    this.content = payload.content;
  }

  onHide() {
    this.show = false;
  }
}
