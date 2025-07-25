import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import {
  ActivatedRoute,
  NavigationEnd,
  Route,
  Router,
  RouterOutlet,
} from '@angular/router';

type PageRouteType = 'home' | 'login' | 'register';
@Component({
  selector: 'app-landing-page',
  imports: [CommonModule, RouterOutlet],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss',
})
export class LandingPageComponent implements OnInit {
  public pageRoute: PageRouteType = 'home';
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.route.url.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        const currentRoute = this.router.url;
        if (currentRoute.includes('/login')) {
          this.pageRoute = 'login';
        } else if (currentRoute.includes('/register')) {
          this.pageRoute = 'register';
        } else {
          this.pageRoute = 'home';
        }
        this.cdr.detectChanges();
      }
    });
  }
}
