import {IBase} from '../utility/helpers/ibase';

export class IUserModel extends  IBase{
  Email?: string;
  Password?: string;
  ConfirmPassword?: string;
  token?: string;
}
