import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  SnackBarService,
  SnackBarType,
} from 'src/app/components/snack-bar/snack-bar.service';
import { IsNew } from 'src/app/models/isnew.model';
import { TodoListModel } from 'src/app/models/todolist.model';
import { TodoListEntryModel } from 'src/app/models/todolistentry.model';
import { IAPIService } from 'src/app/services/api.interface';
import { ErrorService } from 'src/app/services/error.service';
import ListUtil from 'src/app/util/lists';

interface TodoListEntryModelExt extends TodoListEntryModel, IsNew {}

@Component({
  selector: 'app-list-route',
  templateUrl: './list-route.component.html',
  styleUrls: ['./list-route.component.scss'],
})
export class ListRouteComponent implements OnInit {
  list = {} as TodoListModel;
  entries?: TodoListEntryModelExt[];

  constructor(
    @Inject('APIService') private api: IAPIService,
    private errors: ErrorService,
    private snackBar: SnackBarService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.fetchList(params.id);
    });
  }

  async fetchList(id: string) {
    this.list = await this.errors.wrapRequest(() =>
      this.api.getList(id).toPromise()
    );
    this.entries = (await this.errors.wrapRequest(() =>
      this.api.getListEntries(id).toPromise()
    )) as TodoListEntryModelExt[];
  }

  async onRename(entry: TodoListEntryModelExt) {
    if (entry.isNew) {
      this.errors.wrapRequest(
        async () => {
          const newEntry = await this.api
            .createListEntry(this.list.id, entry.content, false)
            .toPromise();
          entry.id = newEntry.id;
          entry.created = newEntry.created;
          entry.contained_in = newEntry.contained_in;
          entry.isNew = false;
          this.snackBar.show('List entry created.', SnackBarType.SUCCESS);
        },
        (err) => {
          ListUtil.removeElement(this.entries!, (e) => e.id === entry.id);
          this.errors.defaultRequestErrorHandler(err);
        }
      );
    } else {
      this.errors.wrapRequest(async () => {
        await this.api.updateListEntry(this.list.id, entry).toPromise();
        this.snackBar.show('List entry updated.', SnackBarType.SUCCESS);
      });
    }
  }

  onEntryClick(entry: TodoListEntryModelExt) {
    entry.checked = !entry.checked;
    return this.errors.wrapRequest(
      () => this.api.updateListEntry(this.list.id, entry).toPromise(),
      (err) => {
        entry.checked = !entry.checked;
        this.errors.defaultRequestErrorHandler(err);
      }
    );
  }

  async onDelete(entry: TodoListEntryModelExt) {
    if (entry.isNew) {
      ListUtil.removeElement(this.entries!, (e) => e.id === entry.id);
    } else {
      await this.errors.wrapRequest(async () => {
        await this.api.deleteListEntry(this.list.id, entry.id).toPromise();
        ListUtil.removeElement(this.entries!, (e) => e.id === entry.id);
      });
    }
  }

  onNew() {
    if (!this.entries) this.entries = [];
    this.entries.push({
      id: 'TMP_NEW_ID',
      content: 'New entry',
      checked: false,
      isNew: true,
    } as TodoListEntryModelExt);
  }
}
