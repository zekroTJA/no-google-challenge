import { EntityModel } from './entity.model';
import { TodoListModel } from './todolist.model';

export interface TodoListEntryModel extends EntityModel {
  content: string;
  checked: boolean;
  contained_in?: TodoListModel;
}
