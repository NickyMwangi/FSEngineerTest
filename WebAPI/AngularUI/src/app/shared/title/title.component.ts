import {ChangeDetectorRef, Component, Input, OnDestroy, OnInit} from '@angular/core';
import {Subject} from 'rxjs';
import {ThemeService} from '../../services/theme.service';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss']
})
export class TitleComponent implements OnInit, OnDestroy {
  @Input() title: string;
  @Input() ItemName: string;
  @Input() ActiveItem: string;
  public sidebarVisible = true;
  private ngUnsubscribe = new Subject();
  constructor(private sidebarService: ThemeService,  private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
  }
  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  toggleFullWidth() {
    this.sidebarService.toggle();
    this.sidebarVisible = this.sidebarService.getStatus();
    this.cdr.detectChanges();
  }

}
