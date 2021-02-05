import { EntityModel } from './entity.model';

export interface UserModel extends EntityModel {
  login_name: string;
}
