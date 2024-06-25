import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { TokenService } from './token.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserLogin } from '../interfaces/user-login';
import { map } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = `${environment.baseUrl}/User`;
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(
    private http: HttpClient,
    public tokenService: TokenService,
    private router: Router) { }

  login(user: UserLogin) {
    return this.http.post(this.baseUrl + '/LoginUser', user).pipe(
      map((response: any) => {
        const result = response;
        if (result.isSuccessful) {
          localStorage.setItem("token", result.data.accessToken)
        }
        else if (!result.isSuccessful) {
          alert(result.message);
        }
      })
    )
  }

  logOut() {
    Swal.fire({
      title: "Are you sure?",
      text: "You are about to log out. Do you want to proceed?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, log out"
    }).then((result) => {
      if (result.isConfirmed) {
        localStorage.removeItem("token");
        localStorage.clear();
        this.router.navigate(['']);
      }
    });


  }

  loggedIn() {
    const token: any = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);
  }
}
