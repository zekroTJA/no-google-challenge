import { Component, Inject, OnInit } from '@angular/core';
import {
  SnackBarService,
  SnackBarType,
} from 'src/app/components/snack-bar/snack-bar.service';
import { IAPIService } from 'src/app/services/api.interface';
import { ErrorService } from 'src/app/services/error.service';

@Component({
  selector: 'app-user-route',
  templateUrl: './user-route.component.html',
  styleUrls: ['./user-route.component.scss'],
})
export class UserRouteComponent implements OnInit {
  currentPassword = '';
  newPassword = '';

  constructor(
    @Inject('APIService') private api: IAPIService,
    private errors: ErrorService,
    private snackBar: SnackBarService
  ) {}

  ngOnInit() {}

  async onSaveChanges() {
    if (this.newPassword) {
      if (!this.currentPassword) {
        this.snackBar.show(
          'Current password must be provided to change the password.',
          SnackBarType.ERROR
        );
        return;
      }

      this.errors.wrapRequest(
        async () => {
          await this.api
            .resetMyPassword(this.currentPassword, this.newPassword)
            .toPromise();
          this.snackBar.show('Password changed.', SnackBarType.SUCCESS);
          this.newPassword = '';
          this.currentPassword = '';
        },
        (err) => {
          this.currentPassword = '';
          this.errors.defaultRequestErrorHandler(err);
        }
      );
    }
  }
}
