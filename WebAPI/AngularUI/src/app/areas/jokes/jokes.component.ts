import {Component, Input, OnInit} from '@angular/core';
import {BaseCrudComponent} from '../../shared/base-crud/base-crud.component';
import {CrudereService} from '../../utility/crudere.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {EventService} from '../../services/event.service';
import {IJokes} from '../../iModels/ijokes';

@Component({
  selector: 'app-jokes',
  templateUrl: './jokes.component.html',
  styleUrls: ['./jokes.component.scss']
})
export class JokesComponent extends BaseCrudComponent<IJokes> implements OnInit  {
  ModelName = IJokes;
  category: string;
  @Input() Editable = true;
  constructor(public crud: CrudereService, public modalService: NgbModal, public router: Router, private toatr: ToastrService,
              public activatedRoute: ActivatedRoute, private url: EventService) {
    super(crud, modalService, router, activatedRoute, toatr);
  }

  ngOnInit(): void {
    this.SOURCE_URL = this.url.JokesURL;
    super.ngOnInit();
  }

  index() {
    this.category = this.activeRoute.snapshot.params.category;
    if (this.category) {
      this.crud.whereObs('CategoryId="' + this.category + '"', this.url.JokesURL)
        .subscribe((n: IJokes[]) => {
        this.listData = n;
      });
    } else {
      this.defaultIndex();
    }
  }

  initForm() {
  }

}
