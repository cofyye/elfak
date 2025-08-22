import { Component, ElementRef, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from '../../../../../app.state';
import { MessageService } from 'primeng/api';
import { AuthService } from '../../../../../shared/services/auth.service';
import { saveUser } from '../../../../auth/store/auth.actions';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent implements OnInit {
  isSidebarVisible: boolean = true;
  minWidthForToggle = 768;

  constructor(
    private elementRef: ElementRef,
    private _router: Router,
    private readonly _store: Store<AppState>,
    private readonly _messageService: MessageService,
    private authService: AuthService,
    private router: Router
  ) {}

  toggleSidebar() {
    if (window.innerWidth >= this.minWidthForToggle) {
      this.isSidebarVisible = !this.isSidebarVisible;
    } else {
      this._router.navigate(['/']);
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.checkScreenWidth();
  }

  ngOnInit() {
    this.checkScreenWidth();
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
          this.router.navigate(['']);
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
  }

  private checkScreenWidth() {
    if (window.innerWidth < this.minWidthForToggle) {
      this.isSidebarVisible = false;
    }
  }
}
