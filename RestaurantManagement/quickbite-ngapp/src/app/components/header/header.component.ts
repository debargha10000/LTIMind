import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean = false;

  public NAV_LINKS = [
    { text: 'login', url: '/login' },
    { text: 'register', url: '/register' },
  ];

  public NAV_LINKS_WITH_LOGIN = [
    { text: 'dashboard', url: '/dashboard' },
    { text: 'order', url: '/order' },
    { text: 'menu', url: '/menu' },
    { text: 'logout', url: '/logout' },
  ];

  public DISPLAY_LINKS = this.NAV_LINKS;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.isLoggedIn$.subscribe((status) => {
      this.isLoggedIn = status;
      if (status) {
        this.DISPLAY_LINKS = this.NAV_LINKS_WITH_LOGIN;
      } else {
        this.DISPLAY_LINKS = this.NAV_LINKS;
      }
    });
  }

  handleLogout() {
    this.authService.logoutUser();
  }
}
