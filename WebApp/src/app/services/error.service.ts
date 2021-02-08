import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  SnackBarService,
  SnackBarType,
} from '../components/snack-bar/snack-bar.service';

/**
 * Service to handle generic and HTTP request
 * errors.
 */
@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  constructor(private snackBar: SnackBarService) {}

  /**
   * Wraps an async function and executes it. If the
   * function throws an error, onError is executed, if
   * provided and a resolved promise is returned.
   * If the function succeeds, the result of the promise
   * is returned.
   * @param func
   * @param onError
   */
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

  /**
   * Shorthand for wrapAsync with a HTTP request
   * promise catching a HttpErrorResponse and passing
   * the error to defaultRequestErrorHandler, if no
   * alternative error handler was provided.
   * @param req
   * @param onError
   */
  wrapRequest<T>(
    req: () => Promise<T>,
    onError?: (err: HttpErrorResponse) => void
  ) {
    if (!onError) onError = this.defaultRequestErrorHandler.bind(this);
    return this.wrapAsync(() => req(), onError);
  }

  /**
   * Default error handler for failed requests parsing the
   * request error response.
   * @param err
   */
  defaultRequestErrorHandler(err: HttpErrorResponse) {
    console.error(err);
    let content = 'An unknown error occured.';
    if (err.error) content = `${err.error} (${err.status})`;
    this.snackBar.show(content, SnackBarType.ERROR, 3500);
  }
}
