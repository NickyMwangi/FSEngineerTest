import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup } from '@angular/forms';
import { map } from 'rxjs/operators';
import { Subject } from 'rxjs';
import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';
import {IBase} from '../../utility/helpers/ibase';
import {CrudereService} from '../../utility/crudere.service';
import {IUserModel} from '../../iModels/iuser-model';
@Component({
  selector: 'app-base-crud',
  template: ``,
  styles: []
})

export abstract class BaseCrudComponent<T extends IBase> implements OnInit {
  ProfileId: string;
  SOURCE_URL: string;
  ModelName: any;
  listData: T[] = null;
  TForm: FormGroup;

  protected constructor(public crud: CrudereService, public modalService: NgbModal, public router: Router,
                        public activeRoute: ActivatedRoute, public toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getProfileId();
    this.index();
    this.initForm();
  }

  getProfileId() {
    const curr: IUserModel = this.crud.Identity();
    if (curr) {
      this.ProfileId = curr.Email;
    }
  }

  // tslint:disable-next-line:typedef
  defaultIndex() {
    this.crud.readObs('', this.SOURCE_URL).pipe(map((n: T[]) => n)).subscribe((i: T[]) => {
      this.listData = i;
    });
  }
  abstract index();
  abstract initForm();
  get f() {
    return this.TForm.controls;
  }

  defaultValuesCreateModal() {
  }

  CreateModal(formRef: any) {
    this.defaultValuesCreateModal();
    this.modalService.open(formRef, { size: 'lg', backdrop: 'static' });
  }

  EditModal(formRef: any, model: T, id: string) {
    this.TForm.patchValue(model);
    this.modalService.open(formRef, { size: 'lg', backdrop: 'static' });
  }

  OnCloseModal() {
    this.TForm.reset();
    this.initForm();
    this.modalService.dismissAll('On close');
  }

  onEdit(model: T) {
    this.router.navigate(['../edit/' + model.Id], { relativeTo: this.activeRoute });
  }

  async logicBeforeCreatePost(createInput: T) {
    return createInput;
  }

  async onCreatePost(model: T, isModal = true) {
    await this.logicBeforeCreatePost(model).then((rs: T) => {
      this.crud.createObs(this.ModelName, rs, this.SOURCE_URL).subscribe((res: T) => {
        this.logicAfterCreatePost(res);
        if (isModal) {
          this.index();
          this.OnCloseModal();
        } else {
          this.router.navigate(['../edit/' + res.Id], { relativeTo: this.activeRoute });
        }
      }, error => {
        if (isModal) {
          this.toastr.error(error, 'Saving Error');
        } else {
          Swal.fire('Error!', error, 'error');
        }
      });
    });
  }

  logicAfterCreatePost(model?: T) {
    return model;
  }

  async logicBeforeEditPost(editInput: T) {
    return editInput;
  }

  async onEditPost(model: T, isModal = true) {
    await this.logicBeforeEditPost(model).then((frm: T) => {
      this.crud.updateObs(this.ModelName, frm, this.SOURCE_URL + model.Id).subscribe((res: T) => {
        this.logicAfterEditPost(res);
        if (isModal) {
          this.index();
          this.OnCloseModal();
        }
      });
    });
  }

  logicAfterEditPost(model?: T) {
    return model;
  }

  onDeletePost(id: string) {
    Swal.fire({
      title: 'Are you sure want to Delete?',
      text: 'You will not be able to recover this data!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.value) {
        this.crud.Delete(this.SOURCE_URL + id).subscribe(r => {
          Swal.fire('Deleted!', 'Details deleted successfully!', 'success');
          this.index();
        },
          err => {
            Swal.fire('Error!', err, 'error');
          });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire('Cancelled', 'Delete terminated :)', 'error');
      }
    });
  }
}
