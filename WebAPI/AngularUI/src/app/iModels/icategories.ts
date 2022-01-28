import {IBase} from '../utility/helpers/ibase';
import {IJokes} from './ijokes';

export class ICategories extends IBase{
  Code: string;
  Description: string;
  Jokes: IJokes[];
}
