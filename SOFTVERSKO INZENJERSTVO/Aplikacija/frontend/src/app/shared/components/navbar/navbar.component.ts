import {
  Component,
  HostListener,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { SidebarModule } from 'primeng/sidebar';
import { AuthService } from '../../services/auth.service';
import { Observable, take } from 'rxjs';
import { RouterModule, Router } from '@angular/router';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { NavbarService } from '../../services/navbar.service';
import { environment } from '../../../../environments/environment';
import { AppState } from '../../../app.state';
import { Store } from '@ngrx/store';
import { saveUser } from '../../../features/auth/store/auth.actions';
import { MessageService } from 'primeng/api';
import { getUser } from '../../../features/auth/store/auth.selectors';

@Component({
  imports: [
    CommonModule,
    ButtonModule,
    SidebarModule,
    RouterModule,
    AvatarGroupModule,
    AvatarModule,
  ],
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  encapsulation: ViewEncapsulation.None,
  standalone: true,
})
export class NavBarComponent implements OnInit {
  sidebarVisible: boolean = false;
  dropdownVisible: boolean = false;
  isAuthenticated$: Observable<boolean>;
  profileDropdownVisible = false;
  avatarUrl = '';
  firstName = '';
  lastName = '';
  role = 'user';
  isExpert = false;

  constructor(
    private authService: AuthService,
    private readonly _messageService: MessageService,
    private _navbarService: NavbarService,
    private readonly _store: Store<AppState>
  ) {
    this.isAuthenticated$ = this.authService.isAuthenticated();
  }

  ngOnInit() {
    this.authService.isAuthenticated().subscribe((response) => {
      if (response) {
        this._navbarService.getProfileInfo().subscribe((response) => {
          this.avatarUrl = `${environment.API_URL}/uploads/${response.data.avatar}`;
          this.firstName = response.data.firstName;
          this.lastName = response.data.lastName;
        });
      }
    });

    this._store.select(getUser).subscribe({
      next: (data) => {
        console.log(data);
        this.role = data.role;
        this.isExpert = data.isExpert;
      },
    });
  }

  toggleSidebar() {
    this.sidebarVisible = !this.sidebarVisible;
  }

  toggleDropdown() {
    this.dropdownVisible = !this.dropdownVisible;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.sidebarVisible = false;
    this.dropdownVisible = false;
  }

  logout() {
    this.authService.logout().subscribe({
      next: (response) => {
        if (response.success) {
          this._store.dispatch(
            saveUser({
              user: {
                id: '',
                isExpert: false,
                role: 'user',
                loggedIn: false,
                fetched: true,
              },
            })
          );
        }
      },
      error: () => {
        this._messageService.add({
          severity: 'error',
          summary: 'Greska',
          detail: 'Greska prilikom odjavljivanja.',
        });
      },
    });
    this.dropdownVisible = false;
  }

  toggleProfileDropdown() {
    this.profileDropdownVisible = !this.profileDropdownVisible;
  }
}
