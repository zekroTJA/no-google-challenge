import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IAPIService } from './services/api.interface';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'WebApp';

  constructor(
    @Inject('APIService') private api: IAPIService,
    private router: Router
  ) {}

  async ngOnInit() {
    this.api.authError.subscribe(this.onAuthError.bind(this));
    await this.api.getMe().toPromise();
  }

  private onAuthError(err: HttpErrorResponse) {
    this.router.navigate(['login']);
  }
}
