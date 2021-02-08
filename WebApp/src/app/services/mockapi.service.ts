import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { delay } from 'rxjs/operators';
import { TodoListModel } from '../models/todolist.model';
import { TodoListEntryModel } from '../models/todolistentry.model';
import { IAPIService } from './api.interface';
import RestAPIService from './restapi.service';

@Injectable({
  providedIn: 'root',
})
export class MockAPIService extends RestAPIService implements IAPIService {
  constructor(client: HttpClient) {
    super(client);
  }

  // getLists(): Observable<TodoListModel[]> {
  //   return super.getLists().pipe(delay(3000));
  // }

  // getList(id: string): Observable<TodoListModel> {
  //   return super.getList(id).pipe(delay(3000));
  // }

  // getListEntries(id: string): Observable<TodoListEntryModel[]> {
  //   return super.getListEntries(id).pipe(delay(3000));
  // }
}
