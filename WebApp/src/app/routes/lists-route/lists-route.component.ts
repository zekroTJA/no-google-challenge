import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  SnackBarService,
  SnackBarType,
} from 'src/app/components/snack-bar/snack-bar.service';
import { IsNew } from 'src/app/models/isnew.model';
import { TodoListModel } from 'src/app/models/todolist.model';
import { IAPIService } from 'src/app/services/api.interface';
import { ErrorService } from 'src/app/services/error.service';
import ListUtil from 'src/app/util/lists';

interface TodoListModelExt extends TodoListModel, IsNew {}

@Component({
  selector: 'app-lists-route',
  templateUrl: './lists-route.component.html',
  styleUrls: ['./lists-route.component.scss'],
})
export class ListsRouteComponent implements OnInit {
  lists?: TodoListModelExt[];

  constructor(
    @Inject('APIService') private api: IAPIService,
    private errors: ErrorService,
    private snackBar: SnackBarService,
    private router: Router
  ) {}

  async ngOnInit() {
    this.lists = (await this.errors.wrapRequest(() =>
      this.api.getLists().toPromise()
    )) as TodoListModelExt[];
  }

  async onRename(list: TodoListModelExt) {
    if (list.isNew) {
      this.errors.wrapRequest(
        async () => {
          const newList = await this.api.createList(list.name).toPromise();
          list.id = newList.id;
          list.created = newList.created;
          list.owner = newList.owner;
          list.isNew = false;
          this.snackBar.show(
            `List '${list.name}' created.`,
            SnackBarType.SUCCESS
          );
        },
        (err) => {
          ListUtil.removeElement(this.lists!, (l) => l.id === list.id);
          this.errors.defaultRequestErrorHandler(err);
        }
      );
    } else {
      this.errors.wrapRequest(async () => {
        await this.api.updateList(list).toPromise();
        this.snackBar.show(
          `List renamed to '${list.name}'.`,
          SnackBarType.SUCCESS
        );
      });
    }
  }

  async onDelete(list: TodoListModelExt) {
    if (list.isNew) {
      ListUtil.removeElement(this.lists!, (l) => l.id === list.id);
    } else {
      this.errors.wrapRequest(async () => {
        await this.api.deleteList(list.id).toPromise();
        ListUtil.removeElement(this.lists!, (l) => l.id === list.id);
        this.snackBar.show(
          `List '${list.name}' removed.`,
          SnackBarType.SUCCESS
        );
      });
    }
  }

  onNew() {
    if (!this.lists) this.lists = [];
    this.lists.push({
      id: 'TMP_NEW_ID',
      name: 'New List',
      isNew: true,
    } as TodoListModelExt);
  }

  onListClick(list: TodoListModelExt) {
    this.router.navigate(['lists', list.id]);
  }
}
