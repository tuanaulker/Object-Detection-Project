import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeTableComponent, LoginComponent, AdminPanelComponent, ErrorComponent } from './components';
import { canActivateGuard, isAdminGuard, isLoggedIn, loginCheck } from './services/guard';
import { LogDetailLinkComponent } from './components/log-detail-link/log-detail-link.component';

export const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
    canActivate: [loginCheck]
  },
  {
    path: "home",
    component: HomeTableComponent,
    canActivate: [canActivateGuard, isLoggedIn],
  },
  {
    path: "admin-panel",
    component: AdminPanelComponent,
    canActivate: [canActivateGuard, isAdminGuard],
  },
  {
    path: "log-detail/:id",
    component: LogDetailLinkComponent,
  },
  {
    path: "**",
    component: ErrorComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
