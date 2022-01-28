import {Component, Input, OnInit} from '@angular/core';
import {IJokes} from '../../iModels/ijokes';
import {IPeople} from '../../iModels/ipeople';
import {CrudereService} from '../../utility/crudere.service';
import {Router} from '@angular/router';
import {EventService} from '../../services/event.service';
import {map} from 'rxjs/operators';
import {Observable, of} from 'rxjs';

@Component({
  selector: 'app-menus',
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.scss']
})
export class MenusComponent  implements OnInit {
  Jokes$: Observable<IJokes[]> = null;
  People$: Observable<IPeople[]> = null;
  @Input() Editable = true;
  constructor(public crud: CrudereService, public router: Router, private url: EventService) {
  }

  ngOnInit(): void {
    this.index();
  }

  index(): void {
    this.crud.readObs('', this.url.PeopleURL)
      .subscribe((n: IPeople[]) => {
      this.People$ = of(n);
    });
    this.crud.readObs('', this.url.JokesURL).
      subscribe((i: IJokes[]) => {
      this.Jokes$ = of(i);
    });
  }
  OnSearch(event: Event): void {
    const input = event.target as HTMLInputElement;
    const stringdt = input.value;
    this.People$ = this.crud.readObs<IPeople[]>('', this.url.PeopleURL)
      .pipe(map(r => {
        return r.filter(x => x.Name.toLowerCase().includes(stringdt));
      }));
    this.Jokes$ = this.crud.readObs<IJokes[]>( this.url.JokesURL)
      .pipe(map(r => {
        return r.filter(x => x.Description.toLowerCase().includes(stringdt));
      }));
  }

}
