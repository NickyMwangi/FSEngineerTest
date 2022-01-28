import {IBase} from '../utility/helpers/ibase';

export class IMenuModel extends IBase{
  Code: string;
  Title: string;
  Name: string;
  Parent: string;
  Url: string;
  IsVisible: boolean;
  Level: number;
  Order: number;
}
