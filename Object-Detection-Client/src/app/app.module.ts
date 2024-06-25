import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { HomeTableComponent } from './components/home-table/home-table.component';
import { ErrorComponent } from './components/error/error.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { MenubarModule } from 'primeng/menubar';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MenuComponent } from './components/home-table/menu/menu.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LogDetailDialogComponent } from './components/log-detail-dialog/log-detail-dialog.component';
import { DialogModule } from 'primeng/dialog';
import { AlertDialogComponent } from './components/alert-dialog/alert-dialog.component';
import { LogDetailLinkComponent } from './components/log-detail-link/log-detail-link.component';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { TokenInterceptor } from './services/token.interceptor';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { TagModule } from 'primeng/tag';
import { CommonModule } from '@angular/common';
import { UserRolePipe } from './pipes/user-role.pipe';
import { AddUserDialogComponent } from './components/add-user-dialog/add-user-dialog.component';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { RadioButtonModule } from 'primeng/radiobutton';
import { UserPipe } from './pipes/user.pipe';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    HomeTableComponent,
    ErrorComponent,
    AdminPanelComponent,
    MenuComponent,
    LogDetailDialogComponent,
    AlertDialogComponent,
    LogDetailLinkComponent,
    UserRolePipe,
    AddUserDialogComponent,
    UserPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ButtonModule,
    TableModule,
    FormsModule,
    MenubarModule,
    RouterModule,
    HttpClientModule,
    DialogModule, 
    DynamicDialogModule,
    InputTextModule,
    DropdownModule,
    TagModule,
    CommonModule,
    InputGroupModule,
    InputGroupAddonModule,
    RadioButtonModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('token')
      }
    }),
  ],
  providers: [
    JwtHelperService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
