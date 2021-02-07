import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
  HttpRequest,
} from '@angular/common/http';
import { EventEmitter } from '@angular/core';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { TodoListModel } from '../models/todolist.model';
import { TodoListEntryModel } from '../models/todolistentry.model';
import { UserModel } from '../models/user.model';
import { IAPIService } from './api.interface';

const API_BASE = environment.production ? '/api' : 'https://localhost:5001/api';

@Injectable({
  providedIn: 'root',
})
export default class RestAPIService implements IAPIService {
  error = new EventEmitter<HttpErrorResponse>();
  authError = new EventEmitter<HttpErrorResponse>();

  constructor(private client: HttpClient) {}

  register(login_name: string, password: string): Observable<UserModel> {
    return this.post<UserModel>('/auth/register', {
      login_name,
      password,
    });
  }

  login(login_name: string, password: string): Observable<UserModel> {
    return this.post<UserModel>(
      '/auth/login',
      {
        login_name,
        password,
      },
      undefined,
      false
    );
  }

  logout(): Observable<any> {
    return this.post<any>('/auth/logout');
  }

  getLists(): Observable<TodoListModel[]> {
    return this.get<TodoListModel[]>('/lists');
  }

  createList(name: string): Observable<TodoListModel> {
    return this.post<TodoListModel>('/lists', { name });
  }

  getList(id: string): Observable<TodoListModel> {
    return this.get<TodoListModel>(`/lists/${id}`);
  }

  updateList(list: TodoListModel): Observable<TodoListModel> {
    return this.post<TodoListModel>(`/lists/${list.id}`, list);
  }

  deleteList(id: string): Observable<any> {
    return this.delete<any>(`/lists/${id}`);
  }

  getListEntries(id: string): Observable<TodoListEntryModel[]> {
    return this.get<TodoListEntryModel[]>(`/lists/${id}/entries`);
  }

  createListEntry(
    id: string,
    content: string,
    checked: boolean = false
  ): Observable<TodoListEntryModel> {
    return this.post<TodoListEntryModel>(`/lists/${id}/entries`, {
      content,
      checked,
    });
  }

  getListEntry(id: string, entry_id: string): Observable<TodoListEntryModel> {
    return this.get<TodoListEntryModel>(`/lists/${id}/entries/${entry_id}`);
  }

  updateListEntry(
    id: string,
    entry: TodoListEntryModel
  ): Observable<TodoListEntryModel> {
    return this.post<TodoListEntryModel>(
      `/lists/${id}/entries/${entry.id}`,
      entry
    );
  }

  deleteListEntry(id: string, entry_id: string): Observable<any> {
    return this.delete<any>(`/lists/${id}/entries/${entry_id}`);
  }

  getMe(): Observable<UserModel> {
    return this.get<UserModel>('/users/me');
  }

  resetMyPassword(
    current_password: string,
    new_password: string
  ): Observable<any> {
    return this.post<TodoListEntryModel>('/users/me/resetpassword', {
      current_password,
      new_password,
    });
  }

  // --- INTERNALS -----------------------

  private get<T>(path: string, params?: HttpParams): Observable<T> {
    return this.client
      .get<T>(`${API_BASE}${path}`, {
        withCredentials: true,
        params,
      })
      .pipe(catchError(this.errorHandler.bind(this)));
  }

  private post<T>(
    path: string,
    body?: any,
    params?: HttpParams,
    catchAuthErr = true
  ): Observable<T> {
    return this.client
      .post<T>(`${API_BASE}${path}`, body, {
        withCredentials: true,
        params,
      })
      .pipe(catchError((e, c) => this.errorHandler(e, c, catchAuthErr)));
  }

  private delete<T>(
    path: string,
    body?: any,
    params?: HttpParams
  ): Observable<T> {
    return this.client
      .delete<T>(`${API_BASE}${path}`, {
        withCredentials: true,
        params,
      })
      .pipe(catchError(this.errorHandler.bind(this)));
  }

  private errorHandler<T>(
    err: HttpErrorResponse,
    caught: Observable<T>,
    catchAuthErr = true
  ): Observable<any> {
    if (err.status === 401) {
      this.authError.emit(err);
      if (catchAuthErr) {
        return of({});
      }
      throw err;
    }

    this.error?.emit(err);
    throw err;
  }
}
