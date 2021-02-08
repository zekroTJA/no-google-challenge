import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  SnackBarService,
  SnackBarType,
} from './components/snack-bar/snack-bar.service';
import { IAPIService } from './services/api.interface';
import { ErrorService } from './services/error.service';
import LocalStorageUtil from './util/localstorage';

const LIGHT_THEME_KEY = 'i-want-to-destroy-my-eyes-theme';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'WebApp';
  lightMode = false;

  constructor(
    @Inject('APIService') private api: IAPIService,
    private router: Router,
    private errors: ErrorService
  ) {
    this.lightMode = LocalStorageUtil.get<boolean>(LIGHT_THEME_KEY, false)!;
  }

  onNavigationActivate(key: string) {
    switch (key) {
      case 'home':
        this.router.navigate(['lists']);
        break;
      case 'profile':
        this.router.navigate(['profile']);
        break;
      case 'themeswitch':
        this.lightMode = !this.lightMode;
        LocalStorageUtil.set(LIGHT_THEME_KEY, this.lightMode);
        break;
      case 'logout':
        this.errors.wrapRequest(async () => {
          await this.api.logout().toPromise();
          window.location.assign('/login');
        });
        break;
    }
  }

  async ngOnInit() {
    this.api.authError.subscribe(this.onAuthError.bind(this));
    await this.api.getMe().toPromise();
  }

  private onAuthError(err: HttpErrorResponse) {
    this.router.navigate(['login']);
  }
}
