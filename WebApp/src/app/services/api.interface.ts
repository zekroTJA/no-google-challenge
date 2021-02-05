import { HttpErrorResponse } from '@angular/common/http';
import { EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { TodoListModel } from '../models/todolist.model';
import { TodoListEntryModel } from '../models/todolistentry.model';
import { UserModel } from '../models/user.model';

export interface IAPIService {
  error: EventEmitter<HttpErrorResponse>;
  authError: EventEmitter<HttpErrorResponse>;

  register(login_name: string, password: string): Observable<UserModel>;
  login(login_name: string, password: string): Observable<UserModel>;
  logout(): Observable<any>;

  getLists(): Observable<TodoListModel[]>;
  createList(name: string): Observable<TodoListModel>;
  getList(id: string): Observable<TodoListModel>;
  updateList(list: TodoListModel): Observable<TodoListModel>;
  deleteList(id: string): Observable<any>;
  getListEntries(id: string): Observable<TodoListEntryModel[]>;
  createListEntry(
    id: string,
    content: string,
    checked?: boolean
  ): Observable<TodoListEntryModel>;
  getListEntry(id: string, entry_id: string): Observable<TodoListEntryModel>;
  updateListEntry(
    id: string,
    entry: TodoListEntryModel
  ): Observable<TodoListEntryModel>;
  deleteListEntry(id: string, entry_id: string): Observable<any>;

  getMe(): Observable<UserModel>;
  resetMyPassword(
    current_password: string,
    new_password: string
  ): Observable<any>;
}
