import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { Router} from '@angular/router';
import {AuthService} from '../../utility/guards/auth.service';
import {IUserModel} from '../../iModels/iuser-model';
import {ThemeService} from '../../services/theme.service';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss']
})
export class TopbarComponent implements OnInit {
  @Input() showNotifMenu = false;
  @Input() showToggleMenu = false;
  @Input() darkClass  = '';
  @Output() toggleSettingDropMenuEvent = new EventEmitter();
  @Output() toggleNotificationDropMenuEvent = new EventEmitter();
  user: IUserModel;

  @Output() settingsButtonClicked = new EventEmitter();
  @Output() mobileMenuButtonClicked = new EventEmitter();

  constructor(private router: Router, public authService: AuthService, private themeService: ThemeService) {
  }

  ngOnInit() {
  }

  toggleSettingDropMenu() {
    this.toggleSettingDropMenuEvent.emit();
  }

  toggleNotificationDropMenu() {
    this.toggleNotificationDropMenuEvent.emit();
  }

  toggleSideMenu(){
    this.themeService.showHideMenu();
  }
}
