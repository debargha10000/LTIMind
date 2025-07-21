import { Routes } from '@angular/router';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { DashboardPageComponent } from './pages/dashboard-page/dashboard-page.component';
import { AdminsPageComponent } from './pages/admins-page/admins-page.component';
import { CartPageComponent } from './pages/cart-page/cart-page.component';
import { DeliverPersonnelPageComponent } from './pages/deliver-personnel-page/deliver-personnel-page.component';
import { MenuPageComponent } from './pages/menu-page/menu-page.component';
import { OrderDetailsPageComponent } from './pages/order-details-page/order-details-page.component';
import { OrderHistoryPageComponent } from './pages/order-history-page/order-history-page.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { LoginFormComponent } from './forms/login-form/login-form.component';
import { RegisterFormComponent } from './forms/register-form/register-form.component';
import { LogoutPageComponent } from './pages/logout-page/logout-page.component';

export const routes: Routes = [
  {
    path: '',
    component: LandingPageComponent,
    children: [
      { path: 'login', component: LoginFormComponent },
      { path: 'register', component: RegisterFormComponent },
    ],
  },
  { path: 'dashboard', component: DashboardPageComponent },
  { path: 'admins', component: AdminsPageComponent },
  { path: 'cart', component: CartPageComponent },
  { path: 'personnel', component: DeliverPersonnelPageComponent },
  { path: 'menu', component: MenuPageComponent },
  { path: 'profile', component: ProfilePageComponent },
  {
    path: 'order',
    children: [
      { path: '', component: OrderHistoryPageComponent },
      { path: ':id', component: OrderDetailsPageComponent },
    ],
  },
  { path: 'logout', component: LogoutPageComponent },
];
