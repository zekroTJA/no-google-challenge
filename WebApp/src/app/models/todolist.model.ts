import { EntityModel } from './entity.model';
import { UserModel } from './user.model';

export interface TodoListModel extends EntityModel {
  name: string;
  owner?: UserModel;
}
