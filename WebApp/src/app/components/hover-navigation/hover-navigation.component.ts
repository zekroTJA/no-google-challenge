import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-hover-navigation',
  templateUrl: './hover-navigation.component.html',
  styleUrls: ['./hover-navigation.component.scss'],
})
export class HoverNavigationComponent implements OnInit {
  @Output() activate = new EventEmitter<string>();

  constructor() {}

  ngOnInit() {}

  onActivate(key: string) {
    this.activate.emit(key);
  }
}
