import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  SnackBarService,
  SnackBarType,
} from '../components/snack-bar/snack-bar.service';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  constructor(private snackBar: SnackBarService) {}

  async wrapAsync<TResult, TError>(
    func: () => Promise<TResult>,
    onError?: (err: TError) => void
  ): Promise<TResult> {
    try {
      return await func();
    } catch (err) {
      onError?.call(this, err);
      return Promise.resolve((null as any) as TResult);
    }
  }

  wrapRequest<T>(
    req: () => Promise<T>,
    onError?: (err: HttpErrorResponse) => void
  ) {
    if (!onError) onError = this.defaultRequestErrorHandler.bind(this);
    return this.wrapAsync(() => req(), onError);
  }

  defaultRequestErrorHandler(err: HttpErrorResponse) {
    let content = 'An unknown error occured.';
    if (err.error) content = `${err.error} (${err.status})`;
    this.snackBar.show(content, SnackBarType.ERROR, 3500);
  }
}
