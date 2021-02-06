import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  SnackBarService,
  SnackBarType,
} from 'src/app/components/snack-bar/snack-bar.service';
import { IAPIService } from 'src/app/services/api.interface';
import { ErrorService } from 'src/app/services/error.service';

@Component({
  selector: 'app-login-route',
  templateUrl: './login-route.component.html',
  styleUrls: ['./login-route.component.scss'],
})
export class LoginRouteComponent implements OnInit {
  public active = 0;

  constructor(
    @Inject('APIService') private api: IAPIService,
    private router: Router,
    private errors: ErrorService,
    private snackBar: SnackBarService
  ) {}

  ngOnInit() {}

  onKeyPress(event: KeyboardEvent, loginName: string, password: string) {
    if (event.key === 'Enter') {
      switch (this.active) {
        case 0:
          return this.onLogin(loginName, password);
        case 1:
          return this.onRegister(loginName, password);
      }
    }
    return null;
  }

  async onLogin(loginName: string, password: string) {
    this.errors.wrapRequest(
      async () => {
        await this.api.login(loginName, password).toPromise();
        this.router.navigate(['/']);
      },
      () =>
        this.snackBar.show('Invalid username or password.', SnackBarType.ERROR)
    );
  }

  async onRegister(loginName: string, password: string) {
    this.errors.wrapRequest(async () => {
      await this.api.register(loginName, password).toPromise();
      this.snackBar.show(
        'Successfully registered. You will now being logged in.',
        SnackBarType.SUCCESS
      );
      await this.onLogin(loginName, password);
    });
  }
}
