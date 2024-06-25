import { Token } from '@angular/compiler';
import { Injectable, inject } from '@angular/core';
import { CanActivateFn,Router} from '@angular/router';
import { TokenService } from '../services/token.service';
import { AuthService } from './auth.service';


export const canActivateGuard: CanActivateFn = () => {
  const router = inject(Router);
  if (localStorage.getItem('token')) {
    return true;
  } else {
    router.navigate(['']);
    return false;
  }
};

export const isLoggedIn: CanActivateFn = () => {
  const tokenService = inject(TokenService);
  const authService = inject(AuthService)
  if (tokenService.loggedIn()) {
    authService.logOut()
    alert('Your token has been expired. You are getting redirect to the login page.')
    return false;
  } 
  else {
    return true;
  }
};

export const loginCheck: CanActivateFn = () => {
  const router = inject(Router);
  if (localStorage.getItem('token')) {
    router.navigate(['home']);
    return false;
  } else {
    return true;
  }
};


export const isAdminGuard: CanActivateFn = () => {
  const router = inject(Router);
  const tokenService = inject(TokenService);

  if (localStorage.getItem('token') && (tokenService.hasRole('Admin') || tokenService.hasRole('SuperAdmin'))  ) {
    return true;
  } else {
    router.navigate(['']);
    return false;
  }
};