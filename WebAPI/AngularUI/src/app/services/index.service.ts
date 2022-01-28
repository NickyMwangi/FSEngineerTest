import {AuthService} from '../utility/guards/auth.service';
import {CookieService} from '../utility/guards/cookie.service';
import {UrlService} from './url.service';
import {CrudereService} from '../utility/crudere.service';
import {ThemeService} from './theme.service';

export const IndexServices: any[] = [AuthService, CookieService, UrlService, CrudereService, ThemeService];
export * from '../utility/guards/auth.service';
export *  from '../utility/guards/cookie.service';
export * from './url.service';
export * from './theme.service';
