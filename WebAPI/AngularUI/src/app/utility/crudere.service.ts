import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, Subject, throwError} from 'rxjs';
import {catchError, map, shareReplay, tap} from 'rxjs/operators';
import Swal from 'sweetalert2';
import {UrlService} from '../services/url.service';
import {AngularFirestore} from '@angular/fire/firestore';
import {IUserModel} from '../iModels/iuser-model';
import {ToastrService} from 'ngx-toastr';
import {NgxSpinnerService} from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})

export abstract class CrudereService {
   protected constructor(protected httpCli: HttpClient, private n: UrlService,
                         private db: AngularFirestore, private toaster: ToastrService, private sp: NgxSpinnerService) { }

  Insert<T>(model: T | any, path?: string){
     this.sp.show();
     return this.db.collection(path).add(model).then((n: T[] | any) => {
      this.toaster.success('Saved Successfully', 'Success');
      this.sp.hide();
      return n;
    }).catch(this.n.handlePostError);
  }

  Get<T>(path?: string | any, OrderValue?: string): Observable<any> {
    // this.sp.show();
    return this.db.collection<T>(path, ref => ref.orderBy(OrderValue)).snapshotChanges().pipe(map(actions => {
        return actions.map(a => {
          const data = a.payload.doc.data() as T;
          const id = a.payload.doc.id;
          // this.sp.hide();
          return { id, ...data };
        }, catchError(this.n.handleHttpError));
      }, catchError(this.n.handleHttpError))
    );
  }

  GetByValue<T>(path?: string | any, field?: string | any, value?: string | any): Observable<any> {
    return this.db.collection<T>(path, ref => ref.where(field, '==', value)).snapshotChanges().pipe(
      map(actions => {
        return actions.map(a => {
          const data = a.payload.doc.data() as T;
          const id = a.payload.doc.id;
          return { id, ...data };
        }, catchError(this.n.handleHttpError));
      }, catchError(this.n.handleHttpError))
    );
  }

  Update<T>(model: T | any, path?: string, bol?: boolean){
    this.sp.show();
    return this.db.collection(path).doc(model.Id).update(model)
      .then((n: T[] | any) => {  if (!bol) {this.toaster.success('updated Successfully', 'Success');} this.sp.hide(); })
      .catch(this.n.handlePostError);
  }

  Delete<T>(id: string, path: string){
    Swal.fire({
      title: 'Are you sure want to Delete?',
      text: 'You will not be able to recover this data!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.value) {
        this.db.collection(path).doc(id).delete().then((n: T[] | any) => {
          Swal.fire('Deleted!', 'Delete Successfull', 'success');
        }).catch(this.n.handleHttpError);
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire('Cancelled', 'Delete failed :)', 'error');
      }
    });

  }
}
