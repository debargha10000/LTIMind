import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-logout-page',
  imports: [],
  templateUrl: './logout-page.component.html',
  styleUrl: './logout-page.component.scss',
})
export class LogoutPageComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.logoutUser().subscribe({
      next: (res) => {
        localStorage.removeItem('session');
        localStorage.setItem('loginstatus', 'false');
        alert(res.message);
        this.router.navigate(['']);
      },
      error: (err) => {
        console.log('err', err);
        alert(err.error.message);
        this.router.navigate(['/dashboard']);
      },
    });
  }
}
