import { Component, AfterViewInit, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import {filter, map, mergeMap, takeUntil} from 'rxjs/operators';
import {pipe, Subject} from 'rxjs';
import {ThemeService} from '../services/theme.service';

@Component({
  selector: 'app-layouts',
  templateUrl: './layouts.component.html',
  styleUrls: ['./layouts.component.scss']
})
export class LayoutsComponent implements AfterViewInit, OnInit, OnDestroy  {
  public title = 'HR Appraisal';
  public isStopLoading = false;
  public showNotifMenu = false;
  public showToggleMenu = false;
  public navTab = 'menu';
  public currentActiveMenu = 'light';
  public currentActiveSubMenu;
  public themeClass = 'theme-cyan';
  public smallScreenMenu = '';
  public darkClass = '';
  private ngUnsubscribe = new Subject();

  constructor(private router: Router, private activatedRoute: ActivatedRoute,
              private themeService: ThemeService, private titleService: Title) {
    this.activatedRoute.url.pipe(takeUntil(this.ngUnsubscribe)).subscribe(url => {
      this.isStopLoading = false;
      this.getActiveRoutes();
    });

    this.themeService.themeClassChange.pipe(takeUntil(this.ngUnsubscribe)).subscribe(themeClass => {
      this.themeClass = themeClass;
    });

    this.themeService.smallScreenMenuShow.pipe(takeUntil(this.ngUnsubscribe)).subscribe(showMenuClass => {
      this.smallScreenMenu = showMenuClass;
    });

    this.themeService.darkClassChange.pipe(takeUntil(this.ngUnsubscribe)).subscribe(darkClass => {
      this.darkClass = darkClass;
    });
  }
  ngOnInit(): void {
  }

  ngOnDestroy = () => {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  };

  toggleNotificationDropMenu() {
    this.showNotifMenu = !this.showNotifMenu;
  }

  toggleSettingDropMenu() {
    this.showToggleMenu = !this.showToggleMenu;
  };

  ngAfterViewInit() {
    const that = this;
    setTimeout(() => {
      that.isStopLoading = true;
    }, 1000);
  }

  getActiveRoutes() {
    const segments: Array<string> = this.router.url.split('/');
    this.currentActiveMenu = segments[2];
    this.currentActiveSubMenu = segments[3];
  }

  activeInactiveMenu($event) {
    if ($event.item && $event.item === this.currentActiveMenu) {
      this.currentActiveMenu = '';
    } else {
      this.currentActiveMenu = $event.item;
    }
  }

}
