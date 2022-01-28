import {Component, Input, OnInit} from '@angular/core';
import {BaseCrudComponent} from '../../shared/base-crud/base-crud.component';
import {ICategories} from '../../iModels/icategories';
import {CrudereService} from '../../utility/crudere.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {EventService} from '../../services/event.service';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent extends BaseCrudComponent<ICategories> implements OnInit {
  ModelName = ICategories;
  @Input() Editable = true;
  constructor(public crud: CrudereService, public modalService: NgbModal, public router: Router, private toatr: ToastrService,
              public activatedRoute: ActivatedRoute, private url: EventService) {
    super(crud, modalService, router, activatedRoute, toatr);
  }

  ngOnInit(): void {
    this.SOURCE_URL = this.url.CategoryURL;
    super.ngOnInit();
  }

  index() {
    this.defaultIndex();
  }

  initForm() {
  }


}
