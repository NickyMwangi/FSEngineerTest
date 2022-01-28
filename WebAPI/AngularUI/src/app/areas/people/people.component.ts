import {Component, Input, OnInit} from '@angular/core';
import {ICategories} from '../../iModels/icategories';
import {CrudereService} from '../../utility/crudere.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {EventService} from '../../services/event.service';
import {BaseCrudComponent} from '../../shared/base-crud/base-crud.component';
import {IPeople} from '../../iModels/ipeople';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.scss']
})
export class PeopleComponent  extends BaseCrudComponent<IPeople> implements OnInit  {
  ModelName = IPeople;
  @Input() Editable = true;
  constructor(public crud: CrudereService, public modalService: NgbModal, public router: Router, private toatr: ToastrService,
              public activatedRoute: ActivatedRoute, private url: EventService) {
    super(crud, modalService, router, activatedRoute, toatr);
  }

  ngOnInit(): void {
    this.SOURCE_URL = this.url.PeopleURL;
    super.ngOnInit();
  }

  index() {
    this.defaultIndex();
  }

  initForm() {
  }

}
